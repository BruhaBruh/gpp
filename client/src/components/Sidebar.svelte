<script lang="ts">
	import SidebarHeader from './SidebarHeader.svelte';
	import SidebarNavigation from './SidebarNavigation.svelte';
</script>

<aside
	style="min-width: 288px"
	class="w-72 min-h-screen h-full text-gray-50 pt-10 bg-gray-800 hidden xl:block shadow-lg"
>
	<SidebarHeader />
	<SidebarNavigation />
</aside>
