<script lang="ts" context="module">
	/** @type {import('@sveltejs/kit').Load} */
	export async function load({ params, fetch, session, stuff }) {
		return {
			props: {
				id: /^\d*$/i.test(params.id) ? Number(params.id) : 0
			}
		};
	}
	export const prerender = true;
</script>

<script lang="ts">
	import { mutation, operationStore, query } from '@urql/svelte';
	import { onDestroy, onMount } from 'svelte';
	import UserBody from '../../components/UserBody.svelte';
	import UserHeader from '../../components/UserHeader.svelte';
	import Avatar from '../../components_ui/Avatar.svelte';
	import Icon from '../../components_ui/Icon.svelte';
	import {
		IncViewDocument,
		IncViewMutation,
		UserDocument,
		UserQuery
	} from '../../generated/graphql';
	import { profile } from '../../stores/profile';

	export let id: number;
	const userStore = operationStore<UserQuery>(
		UserDocument,
		{ userId: id },
		{ fetchOptions: { cache: 'no-cache' }, requestPolicy: 'network-only' }
	);

	$: user = !!$userStore.data?.users?.nodes?.length ? $userStore.data.users.nodes[0] : undefined;

	query(userStore);

	const incViewStore = operationStore<IncViewMutation>(IncViewDocument);

	const incView = mutation(incViewStore);

	let viewTimeout: NodeJS.Timeout;

	onMount(() => {
		viewTimeout = setTimeout(() => {
			if (!user || user?.userId === $profile.userId || $profile.userId === 0) return;
			incView({
				userId: user?.userId
			});
		}, 10000);
	});

	onDestroy(() => clearTimeout(viewTimeout));
</script>

<svelte:head>
	{#if $userStore.data && user}
		<title>{user.nickname}</title>
	{/if}
</svelte:head>

{#if $userStore.fetching}
	<div class="flex items-center justify-center w-full h-full">
		<Icon name="spinner" size={96} class="fill-gray-400 animate-spin" />
	</div>
{/if}

{#if $userStore.data && user}
	<Avatar
		src={user.banner}
		class="w-full h-screen fixed top-0 left-0 -z-10 bg-transparent brightness-25"
	/>
	<div
		class="flex flex-col lg:flex-row space-x-0 space-y-4 lg:space-y-0 lg:space-x-4 items-stretch lg:items-start"
	>
		<UserHeader {user} />
		<UserBody {user} />
	</div>
{/if}
