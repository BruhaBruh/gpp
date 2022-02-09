<script lang="ts" context="module">
	export type DropDownPosition = 'left' | 'center' | 'right';
</script>

<script lang="ts">
	import { slide } from 'svelte/transition';

	export let position: DropDownPosition = 'center';
	export let isOpen = false;
	export let selectButton: any;

	const documentClick = (e) => {
		if (e.path.includes(selectButton)) return;
		isOpen = false;
	};
</script>

<svelte:window on:click={documentClick} />

{#if isOpen}
	<div
		class={[
			'bg-gray-700 rounded-md top-full translate-y-1 absolute min-w-full w-max max-w-xs overflow-y-auto overflow-x-hidden z-10 grid grid-cols-1 shadow-md py-1 mt-0.5',
			position === 'center' ? 'left-1/2 -translate-x-1/2' : '',
			$$restProps.class
		]
			.filter(Boolean)
			.join(' ')}
		style="max-height: 179px"
		class:left-0={position === 'left'}
		class:right-0={position === 'right'}
		transition:slide={{ duration: 150 }}
		on:click|stopPropagation={() => {}}
	>
		<slot />
	</div>
{/if}
