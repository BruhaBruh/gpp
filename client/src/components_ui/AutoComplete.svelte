<script lang="ts" context="module">
	export type AutoCompleteValue = {
		label: string;
		value: string;
		disabled?: boolean;
	};
</script>

<script lang="ts">
	import DropDown, { DropDownPosition } from './DropDown.svelte';
	import DropDownRadioItem from './DropDownRadioItem.svelte';
	import Icon from './Icon.svelte';

	export let selectedValue = '';
	export let values: AutoCompleteValue[] = [];
	export let filterValue = '';
	export let dropDownPosition: DropDownPosition = 'center';
	export let isLoading = false;
	export let disabled = false;

	let isOpen = false;
	let selectButton: HTMLButtonElement;
	let input: HTMLInputElement;

	const setFilterValueLikeSelectedValueLabel = () => {
		filterValue = values.filter((v) => v.value === selectedValue)[0].label;
	};

	const checkSelectValue = () => {
		if (values.filter((v) => v.value === selectedValue)[0]?.label !== filterValue)
			selectedValue = '';
	};

	$: selectedValue && setFilterValueLikeSelectedValueLabel();

	$: filterValue && checkSelectValue();

	const handleClick = () => {
		if (disabled || !input) return;
		input.focus();
	};
</script>

<div {...$$restProps} class={['relative select-none', $$restProps.class].filter(Boolean).join(' ')}>
	<button
		tabindex="-1"
		class="rounded-lg h-10 transition ease-in flex-1 appearance-none border w-full px-4 bg-gray-700 text-gray-50 shadow-sm focus-within:outline-none focus-within:ring-2 focus-within:ring-primary-600 flex items-center space-x-1 cursor-text"
		class:border-gray-500={!disabled && !isLoading}
		class:hover:border-primary-600={!disabled && !isLoading}
		class:cursor-not-allowed={disabled || isLoading}
		class:border-transparent={disabled || isLoading}
		bind:this={selectButton}
		on:click={handleClick}
	>
		<input
			class="w-full bg-transparent text-base font-normal"
			placeholder="Не выбрано"
			on:focus={() => (isOpen = true)}
			bind:value={filterValue}
			bind:this={input}
		/>
		{#if filterValue}
			<Icon
				on:click={(e) => {
					e.stopPropagation();
					filterValue = '';
					selectedValue = '';
				}}
				name="cancel"
				size={20}
				class={'transition ease-in cursor-pointer'}
			/>
		{/if}
	</button>
	<DropDown bind:isOpen {selectButton} position={dropDownPosition}>
		{#if isLoading}
			<DropDownRadioItem value={new Date().getTime()} disabled={true} {isLoading} />
		{:else}
			{#each values as v (v.value)}
				<DropDownRadioItem
					bind:group={selectedValue}
					bind:isOpen
					bind:selectButton={input}
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
