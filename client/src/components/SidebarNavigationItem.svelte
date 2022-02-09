<script lang="ts" context="module">
	import type { IconName } from '../components_ui/Icon.svelte';
	import Icon from '../components_ui/Icon.svelte';
	import Typography from '../components_ui/Typography.svelte';

	export type IconColor = 'primary' | 'secondary' | 'info' | 'success' | 'warning' | 'danger';

	export type NavigationItem = {
		href?: string;
		to?: string;
		badge?: number;
		label: string;
		icon: IconName;
		iconColor?: IconColor;
	};
</script>

<script lang="ts">
	export let icon: IconName;
	export let to: string = undefined;
	export let href: string = undefined;
	export let badge: number = undefined;
	export let iconColor: IconColor = 'secondary';
</script>

{#if to}
	<a
		{...$$restProps}
		class={[
			'hover:text-gray-50 hover:bg-gray-700 flex items-center p-2 transition-colors ease-in text-gray-400 rounded-lg focus:ring-2 focus:ring-primary-600',
			$$restProps.class
		]
			.filter(Boolean)
			.join(' ')}
		href={to}
	>
		<Icon
			name={icon}
			size={28}
			slot="icon"
			class={[
				iconColor === 'primary' ? 'fill-primary-400' : '',
				iconColor === 'info' ? 'fill-info-400' : '',
				iconColor === 'success' ? 'fill-success-400' : '',
				iconColor === 'warning' ? 'fill-warning-400' : '',
				iconColor === 'danger' ? 'fill-danger-400' : ''
			]
				.filter(Boolean)
				.join(' ')}
		/>
		<Typography variant="h6" class="mx-2 truncate"><slot /></Typography>
		{#if badge}
			<Typography
				variant="caption"
				class="h-6 text-xs rounded-full text-gray-50 bg-primary-500 flex items-center justify-center ml-auto px-1"
				style="min-width: 24px"
			>
				{badge}
			</Typography>
		{/if}
	</a>
{:else if href}
	<button
		type="button"
		{...$$restProps}
		class={[
			'hover:text-gray-50 hover:bg-gray-700 flex items-center p-2 transition-colors ease-in text-gray-400 rounded-lg focus:ring-2 focus:ring-primary-600',
			$$restProps.class
		]
			.filter(Boolean)
			.join(' ')}
		on:click={() => window.location.replace(href)}
	>
		<Icon
			name={icon}
			size={28}
			slot="icon"
			class={[
				iconColor === 'primary' ? 'fill-primary-400' : '',
				iconColor === 'info' ? 'fill-info-400' : '',
				iconColor === 'success' ? 'fill-success-400' : '',
				iconColor === 'warning' ? 'fill-warning-400' : '',
				iconColor === 'danger' ? 'fill-danger-400' : ''
			]
				.filter(Boolean)
				.join(' ')}
		/>
		<Typography variant="h6" class="mx-2 truncate"><slot /></Typography>
		{#if badge}
			<Typography
				variant="caption"
				class="h-6 text-xs rounded-full text-gray-50 bg-primary-500 flex items-center justify-center ml-auto px-1"
				style="min-width: 24px"
			>
				{badge}
			</Typography>
		{/if}
	</button>
{/if}
