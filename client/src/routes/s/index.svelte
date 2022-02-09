<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import Button from '../../components_ui/Button.svelte';
	import Icon from '../../components_ui/Icon.svelte';
	import Paper from '../../components_ui/Paper.svelte';
</script>

<svelte:head>
	<title>Настройки</title>
</svelte:head>

<Paper class="flex flex-col space-y-4 max-w-2xl mx-auto">
	<Button to="/s/i" color="secondary" variant="text"
		><Icon size={24} name="layout" slot="start" class="" />Интерфейса
		<Icon size={24} name="chevron-right" slot="end" class="fill-info-400" /></Button
	>
	<Button to="/s/d" color="secondary" variant="text">
		<Icon size={24} name="discord" slot="start" class="" />
		Уведомления
		<Icon size={24} name="chevron-right" slot="end" class="fill-info-400" />
	</Button>
</Paper>
