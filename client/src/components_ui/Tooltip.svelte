<script lang="ts">
	import { fade } from 'svelte/transition';
	import Typography from './Typography.svelte';

	export let popupText: string;

	let showTooltip = false;
</script>

<div
	{...$$restProps}
	class:relative={true}
	class:cursor-pointer={true}
	on:mouseenter={() => (showTooltip = true)}
	on:mouseleave={() => (showTooltip = false)}
>
	{#if showTooltip && !!popupText}
		<div
			transition:fade={{ duration: 150 }}
			class="absolute z-10 bg-gray-900 bg-opacity-90  border border-gray-600 px-2 py-1 rounded-lg truncate left-1/2 bottom-full -translate-y-1 -translate-x-1/2"
		>
			<Typography variant="body2">{popupText}</Typography>
		</div>
	{/if}
	<slot />
</div>
