<script lang="ts" context="module">
	export type SelectValue = {
		label: string;
		value: string | number;
		disabled?: boolean;
	};
</script>

<script lang="ts">
	import DropDown, { DropDownPosition } from './DropDown.svelte';
	import DropDownRadioItem from './DropDownRadioItem.svelte';
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let selectedValue = '';
	export let values: SelectValue[] = [];
	export let closeOnSelect = true;
	export let isLoading = false;
	export let label = 'Не выбрано';
	export let disabled = false;
	export let dropDownPosition: DropDownPosition = 'center';

	let isOpen = false;
	let selectButton: HTMLButtonElement;

	$: selectLabel = values.filter((v) => v.value === selectedValue)[0]?.label || label;

	const handleClick = () => {
		if (disabled) return;
		isOpen = !isOpen;
	};
</script>

<div {...$$restProps} class={['relative select-none', $$restProps.class].filter(Boolean).join(' ')}>
	<button
		class="rounded-lg h-10 transition ease-in flex-1 appearance-none border w-full px-4 bg-gray-700 text-gray-50 shadow-sm focus-within:outline-none focus-within:ring-2 focus-within:ring-primary-600 flex items-center space-x-1"
		class:border-gray-500={!disabled && !isLoading}
		class:hover:border-primary-600={!disabled && !isLoading}
		class:cursor-not-allowed={disabled || isLoading}
		class:border-transparent={disabled || isLoading}
		on:click={handleClick}
		bind:this={selectButton}
	>
		<Typography variant="body1" class="flex-1 text-left">
			{selectLabel}
		</Typography>
		<Icon
			name="chevron-down"
			size={20}
			class={isOpen ? 'transition ease-in rotate-180' : 'transition ease-in'}
		/>
	</button>
	<DropDown bind:isOpen {selectButton} position={dropDownPosition}>
		{#if isLoading}
			<DropDownRadioItem disabled={true} value={new Date().getTime()} {isLoading} />
		{:else}
			{#each values as v (v.value)}
				<DropDownRadioItem
					bind:closeOnSelect
					bind:group={selectedValue}
					bind:isOpen
					bind:selectButton
					disabled={v.disabled}
					value={v.value}
				>
					{v.label}
				</DropDownRadioItem>
			{:else}
				<DropDownRadioItem value={new Date().getTime()} disabled={true}>
					Нет вариантов
				</DropDownRadioItem>
			{/each}
		{/if}
	</DropDown>
</div>
