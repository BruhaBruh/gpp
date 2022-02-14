<script lang="ts" context="module">
</script>

<script lang="ts">
	import Paper from '../components_ui/Paper.svelte';
	import { DonateItemIcon } from '../utils/donateItems';
	import DonateCardBody from './DonateCardBody.svelte';
	import DonateCardFooter from './DonateCardFooter.svelte';
	import DonateCardHeader from './DonateCardHeader.svelte';

	export let donateitemId: number;
	export let name: string;
	export let description: string;
	export let type: number;
	export let amount: number;
	export let cost: number;
	export let icon: number = undefined;

	let showImage = true;
</script>

<Paper
	class="whitespace-pre hover:shadow-lg cursor-pointer space-y-1 select-none"
	on:click={() => console.log('test')}
>
	<DonateCardHeader
		bind:showImage
		iconName={icon === DonateItemIcon.Case ? 'box' : 'crown'}
		{type}
		{name}
	/>
	<DonateCardBody
		{description}
		{amount}
		iconName={icon === DonateItemIcon.Case ? 'box' : 'crown'}
		{type}
		{showImage}
	/>
	<DonateCardFooter {cost} />
</Paper>
