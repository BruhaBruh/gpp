<script lang="ts">
	import { navigationItems } from '../routes/__layout.svelte';
	import SidebarNavigationItem from './SidebarNavigationItem.svelte';
</script>

<nav class="mt-10 px-6 flex flex-col items-stretch space-y-2">
	{#each $navigationItems as item (item.label + item.icon)}
		<SidebarNavigationItem
			icon={item.icon}
			href={item.href}
			to={item.to}
			badge={item.badge}
			iconColor={item.iconColor}
		>
			{item.label}
		</SidebarNavigationItem>
	{/each}
</nav>
