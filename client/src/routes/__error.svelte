<script context="module">
	/** @type {import('@sveltejs/kit').ErrorLoad} */
	export function load({ error, status }) {
		return {
			props: {
				status,
				message: error.message
			}
		};
	}
</script>

<script>
	import Typography from '../components_ui/Typography.svelte';

	export let status;
	export let message;
</script>

<svelte:head>
	<title>Ошибка {status}</title>
</svelte:head>

<Typography variant="h5">
	Ошибка <span class="text-danger-600">{status}</span>
</Typography>
<Typography variant="body1">{message}</Typography>
