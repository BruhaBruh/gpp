<script lang="ts">
	import { fade } from 'svelte/transition';
	import Button from '../components_ui/Button.svelte';
	import Typography from '../components_ui/Typography.svelte';
	export let isOpen = false;

	const handleClick = () => {
		window.localStorage.setItem('cookie_accept', '1');
		isOpen = false;
	};
</script>

<footer
	class="w-screen p-2 xl:p-4 fixed bottom-0 left-0 flex items-center justify-between bg-info-600 cursor-pointer flex-col xl:flex-row space-y-2 xl:space-y-0"
	on:click={handleClick}
	transition:fade
	style="z-index: 10000"
>
	<Typography variant="body1" class="flex-1">
		Мы используем куки, потому что без кук вообще весь интернет работал бы через жопу
	</Typography>
	<Button>Прекрасно</Button>
</footer>
