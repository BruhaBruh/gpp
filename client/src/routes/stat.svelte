<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import SiteOnlineChart from '../components/SiteOnlineChart.svelte';
</script>

<svelte:head>
	<title>Статистика</title>
</svelte:head>

<div class="grid grid-cols-2 gap-4">
	<SiteOnlineChart />
</div>
