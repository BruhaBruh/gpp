<script lang="ts" context="module">
</script>

<script lang="ts">
	import type { IconName } from '../components_ui/Icon.svelte';
	import Icon from '../components_ui/Icon.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { DonateItemType } from '../utils/donateItems';

	export let showImage: boolean;
	export let type: number;
	export let name: string;
	export let iconName: IconName;

	$: iconClass =
		type === DonateItemType.Case
			? 'fill-info-500'
			: type === DonateItemType.Lite
			? 'fill-lite'
			: 'fill-premium';

	const handleClick = (e: MouseEvent) => {
		e.stopPropagation();
		showImage = !showImage;
	};
</script>

<div
	class="grid space-x-2 p-2 rounded-xl hover:bg-gray-50 hover:bg-opacity-10 transition ease-in"
	style="grid-template-columns: max-content 1fr max-content"
	on:click={handleClick}
>
	<Icon name={iconName} class={iconClass} size={28} />
	<Typography variant="h6" class="flex-1 truncate">{name}</Typography>
	<Icon name={'circle-info'} class={'fill-info-500'} size={28} />
</div>
