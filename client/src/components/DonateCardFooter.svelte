<script lang="ts" context="module">
</script>

<script lang="ts">
	import Icon from '../components_ui/Icon.svelte';
	import Typography from '../components_ui/Typography.svelte';

	export let cost: number;
</script>

<div
	class="flex items-center space-x-2 p-2 rounded-xl hover:bg-gray-50 hover:bg-opacity-10 transition ease-in"
>
	<Icon name="rouble" class="fill-info-500" size={28} />
	<Typography variant="h6" class="flex-1 select-text">{cost}</Typography>
	<Icon name="shopping-cart" class="fill-info-500" size={28} />
</div>
