<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import { operationStore, query } from '@urql/svelte';
	import { fade } from 'svelte/transition';
	import UserCell from '../components/UserCell.svelte';
	import Button from '../components_ui/Button.svelte';
	import Paper from '../components_ui/Paper.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { TopDocument, TopQuery, TopQueryVariables, UserTopEnum } from '../generated/graphql';

	let currentTop: UserTopEnum = UserTopEnum.Views;

	const topStore = operationStore<TopQuery>(TopDocument, {
		type: currentTop
	});

	const fetchTop = () => {
		if (!$topStore.data && !$topStore.error) return query(topStore);
		($topStore.variables as TopQueryVariables).type = currentTop;
		$topStore.reexecute();
	};

	const getTitle = (top: UserTopEnum) => {
		switch (top) {
			case UserTopEnum.Friends:
				return 'По друзьям';
			case UserTopEnum.Subscribers:
				return 'По подписчикам';
			case UserTopEnum.Views:
				return 'По просмотрам';
			case UserTopEnum.Years:
				return 'По годам в городе';
			case UserTopEnum.Rating:
				return 'По рейтингу+';
			case UserTopEnum.RatingN:
				return 'По рейтингу-';
			case UserTopEnum.SocialPoints:
				return 'По соц. рейтингу+';
			case UserTopEnum.SocialPointsN:
				return 'По соц. рейтингу-';
		}
	};

	const getCustomLabel = (u: any) => {
		switch (currentTop) {
			case UserTopEnum.Friends:
				return `Друзей: ${u.totalFriends}`;
			case UserTopEnum.Subscribers:
				return `Подписчиков: ${u.totalSubscribers}`;
			case UserTopEnum.Views:
				return `Просмотров: ${u.views}`;
			case UserTopEnum.Years:
				return `Лет в городе: ${u.level}`;
			case UserTopEnum.Rating:
			case UserTopEnum.RatingN:
				return `Текущий рейтинг: ${u.rating.result}`;
			case UserTopEnum.SocialPoints:
			case UserTopEnum.SocialPointsN:
				return `Текущий соц.рейтинг: ${u.socialPoints}`;
		}
	};

	$: currentTitle = getTitle(currentTop);

	$: currentTop && fetchTop();
</script>

<svelte:head>
	<title>Топ - {currentTitle}</title>
</svelte:head>

<div class="space-y-4">
	<Paper class="grid gap-2 sm:grid-cols-2 md:grid-cols-4">
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.Views ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.Views ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.Views)}>{getTitle(UserTopEnum.Views)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.Friends ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.Friends ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.Friends)}>{getTitle(UserTopEnum.Friends)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.Subscribers ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.Subscribers ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.Subscribers)}
			>{getTitle(UserTopEnum.Subscribers)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.Years ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.Years ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.Years)}>{getTitle(UserTopEnum.Years)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.Rating ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.Rating ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.Rating)}>{getTitle(UserTopEnum.Rating)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.RatingN ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.RatingN ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.RatingN)}>{getTitle(UserTopEnum.RatingN)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.SocialPoints ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.SocialPoints ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.SocialPoints)}
			>{getTitle(UserTopEnum.SocialPoints)}</Button
		>
		<Button
			size="small"
			class="my-1 truncate"
			variant={currentTop === UserTopEnum.SocialPointsN ? 'standart' : 'text'}
			color={currentTop === UserTopEnum.SocialPointsN ? 'primary' : 'secondary'}
			on:click={() => (currentTop = UserTopEnum.SocialPointsN)}
			>{getTitle(UserTopEnum.SocialPointsN)}</Button
		>
	</Paper>

	{#if $topStore.fetching}
		<Typography variant="h4">Загрузка...</Typography>
	{/if}

	{#if !!$topStore.data && !$topStore.fetching}
		{#each $topStore.data.top as u, index (u.userId)}
			<div class="flex items-center space-x-2" transition:fade={{ duration: 150 }}>
				<Typography
					variant="h4"
					class={[
						'h-10 w-10 hidden sm:flex items-center justify-center rounded-full',
						index === 0 ? 'bg-warning-400 text-gray-900' : '',
						index === 1 ? 'bg-gray-400 text-gray-900' : '',
						index === 2 ? 'bg-warning-600 text-gray-900' : ''
					]
						.filter(Boolean)
						.join(' ')}>{index + 1}</Typography
				>
				<UserCell user={u} customLabel={getCustomLabel(u)} class="w-full" />
			</div>
		{/each}
	{/if}
</div>
