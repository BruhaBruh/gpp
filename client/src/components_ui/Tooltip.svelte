<script lang="ts">
	import { fade } from 'svelte/transition';
	import Typography from './Typography.svelte';

	export let popupText: string;
	export let horizontalPosition: 'center' | 'left' | 'right' = 'center';
	export let verticalPosition: 'center' | 'top' | 'bottom' = 'top';

	let showTooltip = false;

	$: tooltipClass = [
		'absolute z-10 bg-gray-900 bg-opacity-90  border border-gray-600 px-2 py-1 rounded-lg truncate m-1',
		horizontalPosition === 'left'
			? 'right-full'
			: horizontalPosition === 'right'
			? 'left-full'
			: 'left-1/2 -translate-x-1/2',
		verticalPosition === 'top'
			? 'bottom-full'
			: verticalPosition === 'bottom'
			? 'top-full'
			: 'top-1/2 -translate-y-1'
	].join(' ');
</script>

<div
	{...$$restProps}
	class:relative={true}
	class:cursor-pointer={true}
	on:mouseenter={() => (showTooltip = true)}
	on:mouseleave={() => (showTooltip = false)}
>
	{#if showTooltip && !!popupText}
		<div transition:fade={{ duration: 150 }} class={tooltipClass}>
			<Typography variant="body2">{popupText}</Typography>
		</div>
	{/if}
	<slot />
</div>
