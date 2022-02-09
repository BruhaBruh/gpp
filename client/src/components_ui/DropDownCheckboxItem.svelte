<script lang="ts">
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let isOpen = false;

	export let group: any[] = [];
	export let disabled: boolean = false;
	export let value: any = undefined;
	export let closeOnSelect = false;
	export let selectButton: HTMLButtonElement = undefined;
	export let isLoading: boolean = false;

	const handleClick = () => {
		if (disabled) return;
		if (closeOnSelect) {
			isOpen = false;
			if (selectButton) selectButton.focus();
		}
	};

	const onChange = (e: Event) => {
		const { value, checked } = e.target as HTMLInputElement;
		if (checked) {
			group = [...group, value];
		} else {
			group = group.filter((item) => item !== value);
		}
	};
</script>

<label id="radio" class="relative" class:cursor-pointer={!disabled} on:click={handleClick}>
	<input
		class="visually-hidden"
		type="checkbox"
		checked={group.includes(value)}
		{disabled}
		{value}
		on:change={onChange}
	/>
	<div
		class="py-1.5 px-3 flex items-center space-x-1 transition ease-in"
		class:hover:bg-gray-900={!disabled}
		class:hover:bg-opacity-25={!disabled}
		class:text-gray-50={!disabled && !group.includes(value)}
		class:text-gray-400={disabled}
		class:bg-gray-900={group.includes(value)}
		class:bg-opacity-25={group.includes(value)}
		class:cursor-not-allowed={disabled}
	>
		<div class="w-5 h-5">
			{#if isLoading}
				<Icon name="spinner" class="w-full h-full animate-spin" />
			{:else if !isLoading}
				{#if group.includes(value)}
					<Icon name="check" class="w-full h-full" />
				{/if}
			{/if}
		</div>
		<Typography variant="body2" class="w-full truncate transition ease-in">
			{#if isLoading}
				Загрузка
			{:else}
				<slot />
			{/if}
		</Typography>
	</div>
</label>
