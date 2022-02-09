<script lang="ts">
	import { ui } from '../stores/ui';
	import Notification from './Notification.svelte';

	$: horizontal = $ui.notificationPosition.horizontal;
	$: vertical = $ui.notificationPosition.vertical;
</script>

{#if !!$ui.notifications}
	<div
		class={[
			'p-2 lg:p-4 max-w-lg w-full flex flex-col items-center justify-end space-y-2 fixed',
			horizontal === 'left'
				? 'left-0'
				: horizontal === 'right'
				? 'right-0'
				: 'left-1/2 -translate-x-1/2',
			vertical === 'top' ? 'top-0' : 'bottom-0'
		]
			.filter(Boolean)
			.join(' ')}
		style="z-index: 51"
	>
		{#each $ui.notifications as n (n.id)}
			<Notification {...n} />
		{/each}
	</div>
{/if}
