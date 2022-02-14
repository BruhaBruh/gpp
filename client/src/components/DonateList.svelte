<script lang="ts" context="module">
</script>

<script lang="ts">
	import { operationStore, query } from '@urql/svelte';
	import { slide } from 'svelte/transition';
	import { DonateItemsDocument, DonateItemsQuery } from '../generated/graphql';
	import DonateCard from './DonateCard.svelte';

	const donateItemsStore = operationStore<DonateItemsQuery>(DonateItemsDocument, {
		where: { type: { neq: 2 } }
	});

	query(donateItemsStore);
</script>

<div class="grid gap-4 sm:grid-cols-2 2xl:grid-cols-4" transition:slide={{ duration: 150 }}>
	{#if $donateItemsStore.data?.donateItems?.nodes}
		{#each $donateItemsStore.data.donateItems.nodes as item (item.donateitemId)}
			<DonateCard {...item} />
		{/each}
	{/if}
</div>
