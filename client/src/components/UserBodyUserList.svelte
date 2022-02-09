<script lang="ts">
	import { slide } from 'svelte/transition';
	import Typography from '../components_ui/Typography.svelte';
	import UserCell from './UserCell.svelte';

	export let users: any;
	export let emptyLabel: string;
</script>

<div
	transition:slide={{ duration: 150 }}
	class="grid gap-4 md:grid-cols-2 xl:grid-cols-1 2xl:grid-cols-2"
>
	{#each users as user (user.userId)}
		<UserCell {user} />
	{:else}
		<Typography variant="h3" class="col-span-full text-center">{emptyLabel}</Typography>
	{/each}
</div>
