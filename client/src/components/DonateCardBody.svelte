<script lang="ts" context="module">
</script>

<script lang="ts">
	import { slide } from 'svelte/transition';
	import type { IconName } from '../components_ui/Icon.svelte';
	import Icon from '../components_ui/Icon.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { DonateItemType } from '../utils/donateItems';

	export let showImage: boolean;
	export let description: string;
	export let type: number;
	export let amount: number;
	export let iconName: IconName;
</script>

{#if showImage}
	<div
		transition:slide={{ duration: 150 }}
		class="w-full flex flex-col items-center justify-center"
		style="aspect-ratio: 350 / 200"
	>
		{#if type === DonateItemType.Lite}
			<Icon name={iconName} class="fill-lite" size={128} />
			<Typography variant="h6" class="text-gray-400">{amount} дней</Typography>
		{:else if type === DonateItemType.Premium}
			<Icon name={iconName} class="fill-premium" size={128} />
			<Typography variant="h6" class="text-gray-400">{amount} дней</Typography>
		{:else if type === DonateItemType.Case}
			<img src="https://i.postimg.cc/fbMb3CdF/chest.png" alt="case" class="w-full" />
		{/if}
	</div>
{:else}
	<div
		transition:slide={{ duration: 150 }}
		class="w-full overflow-y-auto whitespace-pre-wrap select-text"
		style="aspect-ratio: 350 / 200"
		on:click={(e) => e.stopPropagation()}
	>
		{description}
	</div>
{/if}
