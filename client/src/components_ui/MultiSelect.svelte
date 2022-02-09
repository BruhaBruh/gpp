<script lang="ts" context="module">
	export type MultiSelectValue = {
		label: string;
		value: string;
		disabled?: boolean;
	};
</script>

<script lang="ts">
	import { fade } from 'svelte/transition';
	import DropDown, { DropDownPosition } from './DropDown.svelte';
	import DropDownCheckboxItem from './DropDownCheckboxItem.svelte';
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let selectedValues = [];
	export let values: MultiSelectValue[] = [];
	export let isLoading = false;
	export let closeOnSelect = false;
	export let dropDownPosition: DropDownPosition = 'center';
	export let disabled = false;

	let isOpen = false;
	let selectButton: HTMLButtonElement;

	let scrollDiv: HTMLDivElement;

	const handleMouseWheel = (e) => {
		if (e.deltaY >= 100) {
			scrollDiv.scrollLeft += 20;
		} else if (e.deltaY <= -100) {
			scrollDiv.scrollLeft -= 20;
			if (scrollDiv.scrollLeft < 0) scrollDiv.scrollLeft = 0;
		}
	};

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
		{#if !!selectedValues.length}
			<div
				class="w-full scrollbar-hidden overflow-y-hidden overflow-x-scroll"
				bind:this={scrollDiv}
				on:mousewheel={handleMouseWheel}
			>
				<div class="w-max h-full flex space-x-2">
					{#each selectedValues.map((sv) => values.filter((v) => v.value === sv)[0]) as v (v.value)}
						<div
							class="bg-gray-800 h-full rounded-full w-max px-2 flex items-center space-x-1"
							transition:fade={{ duration: 75 }}
						>
							<Typography variant="helpertext">{v.label}</Typography>
							<Icon
								on:click={(e) => {
									e.stopPropagation();
									selectedValues = selectedValues.filter((s) => s !== v.value);
								}}
								name="cancel"
								size={20}
							/>
						</div>
					{/each}
				</div>
			</div>
		{:else}
			<div in:fade={{ duration: 75, delay: 75 }} class="w-full">
				<Typography variant="body1" class="w-full truncate transition ease-in text-left">
					Не выбрано
				</Typography>
			</div>
		{/if}
		<Icon
			name="chevron-down"
			size={20}
			class={isOpen ? 'transition ease-in rotate-180' : 'transition ease-in'}
		/>
	</button>
	<DropDown {selectButton} bind:isOpen position={dropDownPosition}>
		{#if isLoading}
			<DropDownCheckboxItem disabled={true} value={new Date().getTime()} {isLoading} />
		{:else}
			{#each values as v (v.value)}
				<DropDownCheckboxItem
					bind:closeOnSelect
					bind:group={selectedValues}
					bind:isOpen
					bind:selectButton
					disabled={v.disabled}
					value={v.value}
				>
					{v.label}
				</DropDownCheckboxItem>
			{:else}
				<DropDownCheckboxItem value={new Date().getTime()} disabled={true}>
					Нет вариантов
				</DropDownCheckboxItem>
			{/each}
		{/if}
	</DropDown>
</div>
