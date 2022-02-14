<script lang="ts" context="module">
</script>

<script lang="ts">
	import { operationStore, query } from '@urql/svelte';
	import { onDestroy, onMount } from 'svelte';
	import DonateHeader from '../../components/DonateHeader.svelte';
	import DonateList from '../../components/DonateList.svelte';
	import { DonateStatisticDocument, DonateStatisticQuery } from '../../generated/graphql';

	const donateStatisticStore = operationStore<DonateStatisticQuery>(DonateStatisticDocument);

	query(donateStatisticStore);

	let timer: NodeJS.Timer;

	onMount(() => {
		timer = setInterval(() => {
			$donateStatisticStore.reexecute({ requestPolicy: 'network-only' });
		}, 5000);
	});

	onDestroy(() => {
		clearInterval(timer);
	});
</script>

<svelte:head>
	<title>Донат</title>
</svelte:head>

<div class="space-y-4">
	{#if !!$donateStatisticStore.data?.donateStatistic}
		<DonateHeader
			permissions={$donateStatisticStore.data.donateStatistic.permissions}
			money={$donateStatisticStore.data.donateStatistic.money}
			trefs={$donateStatisticStore.data.donateStatistic.trefs}
			subscriptionEndDate={$donateStatisticStore.data.donateStatistic.subscriptionEndAt !== null
				? new Date($donateStatisticStore.data.donateStatistic.subscriptionEndAt)
				: null}
		/>
	{/if}
	<DonateList />
</div>
