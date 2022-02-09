<script lang="ts">
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let isOpen = false;
	export let group: any = undefined;
	export let disabled: boolean = false;
	export let value: any = undefined;
	export let closeOnSelect = false;
	export let selectButton: HTMLButtonElement = undefined;
	export let isLoading = false;

	$: isSelected = group === value;

	const handleClick = () => {
		if (!!disabled) return;
		if (!!selectButton) selectButton.focus();
		if (!!closeOnSelect) isOpen = false;
	};
</script>

<label id="radio" class="relative" class:cursor-pointer={!disabled} on:click={handleClick}>
	<input class="visually-hidden" type="radio" bind:group {disabled} {value} />
	<div
		class="py-1.5 px-3 flex items-center space-x-1 transition ease-in"
		class:hover:bg-gray-900={!disabled}
		class:hover:bg-opacity-25={!disabled}
		class:text-gray-50={!disabled && !isSelected}
		class:text-gray-400={disabled}
		class:bg-gray-900={isSelected}
		class:bg-opacity-25={isSelected}
		class:cursor-not-allowed={disabled}
	>
		<Typography
			variant="body1"
			class="w-full truncate transition ease-in flex items-center space-x-1"
		>
			{#if isLoading}
				<Icon name="spinner" class="fill-current animate-spin" size={18} />
				<span>Загрузка</span>
			{:else}
				<slot />
			{/if}
		</Typography>
	</div>
</label>
