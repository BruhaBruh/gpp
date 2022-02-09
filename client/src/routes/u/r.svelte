<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { operationStore, query } from '@urql/svelte';
	import UserCell from '../../components/UserCell.svelte';
	import Icon from '../../components_ui/Icon.svelte';
	import { RatingsDocument, RatingsQuery } from '../../generated/graphql';
	import { profile } from '../../stores/profile';

	const ratingsStore = operationStore<RatingsQuery>(RatingsDocument);

	query(ratingsStore);

	const onError = () => {
		console.log($ratingsStore.error);
		goto('/');
	};

	$: ratings = !!$ratingsStore.data
		? $ratingsStore.data.me.ratingTos.sort((a, b) => Number(b.positive) - Number(a.positive))
		: [];

	$: !!$ratingsStore.data && console.log($ratingsStore.data);
	$: !!$ratingsStore.error && onError();
</script>

<svelte:head>
	<title>{$profile.nickname} - Список рейтинга</title>
</svelte:head>

{#if $ratingsStore.fetching}
	<div class="flex items-center justify-center w-full h-full">
		<Icon name="spinner" size={96} class="fill-gray-400 animate-spin" />
	</div>
{/if}
{#if !!$ratingsStore.data}
	<div class="grid gap-4">
		{#each ratings as rating (rating.ratingId)}
			<UserCell
				user={rating.from}
				customLabel={`${rating.positive ? '+' : '-'} Установил(а) ${
					rating.positive ? 'положительный' : 'негативный'
				} рейтинг`}
			/>
		{/each}
	</div>
{/if}
