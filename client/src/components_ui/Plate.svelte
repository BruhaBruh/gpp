<script lang="ts">
	import Icon, { IconName } from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let iconClass: string = '';
	export let iconName: IconName;
	export let name: string;
	export let label: string;
	export let withProgress: boolean = false;
	export let progressWidthPercent: number = 0;
</script>

<div class="shadow-lg rounded-2xl p-4 bg-gray-800 text-gray-50 relative select-none" on:click>
	<div class="flex items-center">
		<span
			class={['bg-gradient-to-br p-1 rounded-full relative', iconClass].filter(Boolean).join(' ')}
		>
			<Icon name={iconName} size={20} />
		</span>
		<Typography variant="body1" class="ml-2 truncate">{name}</Typography>
	</div>
	<div class="w-full flex flex-col justify-start">
		<Typography
			variant="h2"
			class={['mt-4 truncate', withProgress ? 'mb-2' : 'mb-4'].filter(Boolean).join(' ')}
		>
			{label}
		</Typography>
		{#if withProgress}
			<div
				class="relative w-full h-2 bg-gradient-to-l from-danger-400 to-danger-300 rounded overflow-hidden"
			>
				<div
					class="absolute top-0 h-2 left-0 bg-gradient-to-r from-success-400 to-success-300"
					style={`width: ${progressWidthPercent}%`}
				/>
			</div>
		{/if}
	</div>
</div>
