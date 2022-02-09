<script lang="ts">
	import { onDestroy, onMount } from 'svelte';
	import { slide } from 'svelte/transition';
	import { NotificationType, ui } from '../stores/ui';
	import Avatar from './Avatar.svelte';
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let id: number;
	export let header: string;
	export let type: NotificationType;
	export let body = '';
	export let timeout: number;
	export let avatar = '';

	let t: NodeJS.Timeout;

	const closeNotification = () => {
		clearTimeout(t);
		ui.removeNotification(id);
	};

	onMount(() => {
		t = setTimeout(closeNotification, timeout);
	});

	onDestroy(() => {
		clearTimeout(t);
	});
</script>

<div
	class="w-full bg-gray-800 border border-gray-600 shadow-md rounded-lg flex overflow-hidden"
	transition:slide={{ duration: 150 }}
>
	<div class="w-10 min-h-full flex items-center justify-center p-1">
		{#if type === 'success'}
			<Icon name="circle-check" class="fill-success-400" size={32} />
		{:else if type === 'warning'}
			<Icon name="circle-warning" class="fill-warning-400" size={32} />
		{:else if type === 'danger'}
			<Icon name="circle-cancel" class="fill-danger-400" size={32} />
		{:else if type === 'info'}
			<Icon name="circle-info" class="fill-info-400" size={32} />
		{:else if type === 'avatar'}
			<Avatar src={avatar} size={32} class="rounded-full" />
		{/if}
	</div>
	<div class="flex-1 p-1 px-2 border-x border-gray-600 flex flex-col justify-center">
		<Typography variant="h6">{header}</Typography>
		{#if body}
			<Typography variant="body1" class="text-gray-300">{body}</Typography>
		{/if}
	</div>
	<button
		class="w-10 min-h-full flex items-center justify-center p-1 hover:bg-gray-50 hover:bg-opacity-10 transition ease-in cursor-pointer"
		on:click={closeNotification}
	>
		<Icon name="cancel" class="fill-gray-50" size={28} />
	</button>
</div>
