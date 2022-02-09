<script lang="ts">
	import { fade, scale } from 'svelte/transition';
	import { ui } from '../stores/ui';
	import IconButton from './IconButton.svelte';

	$: modalZIndex = 1000;
</script>

<div
	class="h-screen w-screen fixed flex items-center justify-center p-2 sm:p-4 lg:p-6 bg-modalBackDrop"
	style={`z-index: ${modalZIndex}`}
	on:click={() => ui.setModal(null)}
	in:fade={{ duration: 150 }}
>
	<div
		class={[
			'bg-gray-800 py-4 flex flex-col items-stretch space-y-4 rounded-2xl w-full max-w-3xl max-h-full',
			$$props.class
		]
			.filter(Boolean)
			.join(' ')}
		on:click|stopPropagation={() => {}}
		transition:scale={{ duration: 150 }}
	>
		<div class={['flex items-start justify-between px-4', $$props.class].filter(Boolean).join(' ')}>
			<slot name="header" />
			<IconButton
				name="cancel"
				size="small"
				variant="text"
				color="secondary"
				on:click={() => ui.setModal(null)}
			/>
		</div>
		<slot name="body" />
		<slot name="footer" />
	</div>
</div>
