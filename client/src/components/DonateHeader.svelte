<script lang="ts" context="module">
</script>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { slide } from 'svelte/transition';
	import Plate from '../components_ui/Plate.svelte';
	import Tooltip from '../components_ui/Tooltip.svelte';
	import { isHaveSubscription, isPremium } from '../stores/profile';

	export let money: number;
	export let trefs: number;
	export let subscriptionEndDate: Date | null;
	export let permissions: number;

	$: isPermanent = subscriptionEndDate === null;

	const openDonateMoneyForm = () => {};
	const openDonateTrefForm = () => {};
	const openDonateHistory = () => {
		goto('/d/h');
	};
</script>

<div transition:slide={{ duration: 150 }} class="grid gap-4 sm:grid-cols-2 2xl:grid-cols-4">
	<Tooltip popupText={'Нажми, чтобы пополнить монеты'} verticalPosition="bottom">
		<Plate
			iconName="rouble"
			iconClass="from-primary-600 to-primary-400"
			name="Монеты"
			label={money.toString()}
			on:click={openDonateMoneyForm}
		/>
	</Tooltip>
	<Tooltip popupText={'Нажми, чтобы конвертировать монеты в трефы'} verticalPosition="bottom">
		<Plate
			iconName="tref"
			iconClass="from-gray-700 to-gray-500"
			name="Трефы"
			label={trefs.toString()}
			on:click={openDonateTrefForm}
		/>
	</Tooltip>
	{#if isHaveSubscription(permissions)}
		{#if isPremium(permissions)}
			<Plate
				iconName="crown"
				iconClass="from-premium to-primary-500"
				name={`Подписка Premium`}
				label={isPermanent
					? 'Навсегда'
					: `До ${subscriptionEndDate.getDate()}.${
							subscriptionEndDate.getMonth() + 1
					  }.${subscriptionEndDate.getFullYear()}`}
			/>
		{:else}
			<Plate
				iconName="crown"
				iconClass="from-lite to-warning-500 text-gray-900"
				name={`Подписка Lite`}
				label={isPermanent
					? 'Навсегда'
					: `До ${subscriptionEndDate.getDate()}.${
							subscriptionEndDate.getMonth() + 1
					  }.${subscriptionEndDate.getFullYear()}`}
			/>
		{/if}
	{:else}
		<Plate
			iconName="crown"
			iconClass="from-gray-100 to-gray-300 text-gray-900"
			name="Без подписки"
			label={'Грустно :c'}
		/>
	{/if}
	<Tooltip popupText={'Нажми, чтобы посмотреть историю пополнений'} verticalPosition="bottom">
		<Plate
			iconName="donate-history"
			iconClass="from-info-600 to-info-400"
			name="История"
			label={'Ваши донаты'}
			on:click={openDonateHistory}
		/>
	</Tooltip>
</div>
