<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import { operationStore, query } from '@urql/svelte';
	import UserCell from '../../components/UserCell.svelte';
	import IconButton from '../../components_ui/IconButton.svelte';
	import Input from '../../components_ui/Input.svelte';
	import Paper from '../../components_ui/Paper.svelte';
	import Typography from '../../components_ui/Typography.svelte';
	import { UsersDocument, UsersQuery, UsersQueryVariables } from '../../generated/graphql';

	const elements = 24;

	const usersStore = operationStore<UsersQuery>(UsersDocument, {
		first: elements
	});

	let search = '';
	let timeout: NodeJS.Timeout;

	const refetchWithSearch = () => {
		if (!$usersStore.data && !$usersStore.error) return query(usersStore);
		if (timeout) clearTimeout(timeout);
		timeout = setTimeout(() => {
			($usersStore.variables as UsersQueryVariables).first = elements;
			($usersStore.variables as UsersQueryVariables).after = null;
			($usersStore.variables as UsersQueryVariables).before = null;
			($usersStore.variables as UsersQueryVariables).last = null;
			($usersStore.variables as UsersQueryVariables).search = search;
			$usersStore.reexecute();
		}, 1000);
	};

	const firstPage = () => {
		if (!$usersStore.data.users.pageInfo.hasPreviousPage) return;
		($usersStore.variables as UsersQueryVariables).first = elements;
		($usersStore.variables as UsersQueryVariables).after = null;
		($usersStore.variables as UsersQueryVariables).before = null;
		($usersStore.variables as UsersQueryVariables).last = null;
		$usersStore.reexecute();
	};

	const prevPage = () => {
		if (!$usersStore.data.users.pageInfo.hasPreviousPage) return;
		($usersStore.variables as UsersQueryVariables).first = null;
		($usersStore.variables as UsersQueryVariables).after = null;
		($usersStore.variables as UsersQueryVariables).before =
			$usersStore.data.users.pageInfo.startCursor;
		($usersStore.variables as UsersQueryVariables).last = elements;
		$usersStore.reexecute();
	};

	const nextPage = () => {
		if (!$usersStore.data.users.pageInfo.hasNextPage) return;
		($usersStore.variables as UsersQueryVariables).before = null;
		($usersStore.variables as UsersQueryVariables).last = null;
		($usersStore.variables as UsersQueryVariables).first = elements;
		($usersStore.variables as UsersQueryVariables).after =
			$usersStore.data.users.pageInfo.endCursor;
		$usersStore.reexecute();
	};

	const lastPage = () => {
		if (!$usersStore.data.users.pageInfo.hasNextPage) return;
		($usersStore.variables as UsersQueryVariables).before = null;
		($usersStore.variables as UsersQueryVariables).first = null;
		($usersStore.variables as UsersQueryVariables).after = null;
		($usersStore.variables as UsersQueryVariables).last = elements;
		$usersStore.reexecute();
	};

	$: search !== undefined && refetchWithSearch();
</script>

<svelte:head>
	<title>Все профили</title>
</svelte:head>

<div class="space-y-4">
	<Paper class="flex flex-col space-y-2 sm:flex-row sm:items-center sm:space-x-2 sm:space-y-0">
		<Input name="search" class="sm:max-w-xs w-full" placeholder="Поиск..." bind:value={search} />
		<!-- <div class="flex space-x-1">
			<Button
				on:click={() => (isOnlyOnline = false)}
				variant={isOnlyOnline ? 'text' : 'standart'}
				color={isOnlyOnline ? 'secondary' : 'primary'}>Все</Button
			>
			<Button
				on:click={() => (isOnlyOnline = true)}
				variant={!isOnlyOnline ? 'text' : 'standart'}
				color={!isOnlyOnline ? 'secondary' : 'primary'}>Онлайн</Button
			>
		</div> -->
		<Typography variant="body1" class="flex-1 text-center sm:text-right text-gray-400"
			>Найдено: {$usersStore.fetching
				? '...'
				: $usersStore.data?.users?.totalCount || 0}</Typography
		>
	</Paper>
	{#if !!$usersStore.data}
		<div class="grid md:grid-cols-2 xl:grid-cols-3 2xl:grid-cols-4 gap-4">
			{#each $usersStore.data.users.nodes as user (user.userId)}
				<UserCell {user} />
			{/each}
			<div class="col-span-full flex items-center justify-center space-x-2">
				<IconButton
					size="small"
					on:click={firstPage}
					disabled={!$usersStore.data.users.pageInfo.hasPreviousPage}
					name="chevrons-left"
				/>
				<IconButton
					size="small"
					on:click={prevPage}
					disabled={!$usersStore.data.users.pageInfo.hasPreviousPage}
					name="chevron-left"
				/>
				<IconButton
					size="small"
					on:click={nextPage}
					disabled={!$usersStore.data.users.pageInfo.hasNextPage}
					name="chevron-right"
				/>
				<IconButton
					size="small"
					on:click={lastPage}
					disabled={!$usersStore.data.users.pageInfo.hasNextPage}
					name="chevrons-right"
				/>
			</div>
			{#if $usersStore.fetching}
				<Typography variant="h4" class="col-span-full text-center">Загрузка...</Typography>
			{/if}
		</div>
	{/if}
</div>
