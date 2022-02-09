<script lang="ts">
	import { fade } from 'svelte/transition';
	import slide from '../transitions/slide';

	export let zIndex = 0;
	export let isOpen = false;

	$: actionSheetZIndex = 500 + zIndex;
</script>

<div
	class="bg-modalBackDrop w-screen h-screen fixed top-0 left-0 p-2 flex flex-col-reverse overflow-y-auto"
	transition:fade={{ duration: 150 }}
	style={'z-index: ' + actionSheetZIndex}
	on:click={() => (isOpen = false)}
>
	<section
		transition:slide
		class="bg-gray-800 flex flex-col items-stretch py-2 rounded-lg max-w-lg mx-auto w-full"
	>
		<slot />
	</section>
</div>
