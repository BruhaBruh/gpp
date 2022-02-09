<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { onMount } from 'svelte';

	onMount(() => goto('/f'));
</script>

<svelte:head>
	<title>Home</title>
</svelte:head>

IndexPage
