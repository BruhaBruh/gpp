<script lang="ts" context="module">
	export type InputStatus = 'validation' | 'error' | 'success';
</script>

<script lang="ts">
	import type { IconName } from './Icon.svelte';
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let placeholder: string = '';
	export let value: string = '';
	export let status: InputStatus = undefined;
	export let isLoading = false;
	export let title: string = '';
	export let description: string = '';
	export let name: IconName | '' = '';
	export let id: string = undefined;
	export let disabled = false;

	let input: HTMLInputElement;
</script>

<div {...$$restProps} class={['flex flex-col', $$props.class].filter(Boolean).join(' ')}>
	{#if !!title}
		<Typography variant="body1" class="text-gray-50">{title}</Typography>
	{/if}
	<div class="flex items-stretch select-none" on:click={() => input.focus()}>
		{#if !!name}
			<div
				class="bg-gray-700 h-10 w-10 flex items-center justify-center rounded-l-lg border-gray-500 border border-r-0 text-gray-50"
			>
				<Icon {name} size={28} />
			</div>
		{/if}
		<div
			class="rounded-r-lg h-10 transition ease-in flex-1 appearance-none border w-full px-4 bg-gray-700 text-gray-50 shadow-sm focus-within:outline-none focus-within:ring-2 focus-within:ring-primary-600 flex items-center"
			class:text-gray-400={disabled || isLoading}
			class:text-gray-50={!disabled && !isLoading}
			class:border-gray-500={!disabled && !isLoading && !status}
			class:hover:border-primary-600={!disabled && !isLoading && !status}
			class:border-danger-500={status === 'error'}
			class:border-warning-500={status === 'validation'}
			class:border-success-500={status === 'success'}
			class:cursor-not-allowed={disabled || isLoading}
			class:rounded-l-lg={!name}
			style="min-height: 40px"
		>
			<input
				type="text"
				class="flex-1 bg-inherit text-inherit placeholder-gray-400 font-bold text-base"
				class:cursor-not-allowed={disabled || isLoading}
				{placeholder}
				bind:value
				{id}
				disabled={disabled || isLoading}
				bind:this={input}
				on:change
			/>
		</div>
	</div>
	{#if !!description || isLoading}
		<Typography
			variant="helpertext"
			class={!status
				? 'text-gray-400 flex space-x-1 items-center mt-1'
				: status === 'validation'
				? 'text-warning-500 flex space-x-1 items-center mt-1'
				: status === 'error'
				? 'text-danger-500 flex space-x-1 items-center mt-1'
				: 'text-success-500 flex space-x-1 items-center mt-1'}
		>
			{#if isLoading}
				<Icon name="spinner" class="fill-current animate-spin h-4 w-4" />
				<span class="flex-1">Загрузка...</span>
			{:else if !!description}
				{#if status === 'validation'}
					<Icon name="circle-warning" class="fill-current stroke-current h-4 w-4" />
					<span class="flex-1">{description}</span>
				{:else if status === 'error'}
					<Icon name="circle-cancel" class="fill-current h-4 w-4" />
					<span class="flex-1">{description}</span>
				{:else if status === 'success'}
					<Icon name="circle-check" class="fill-current h-4 w-4" />
					<span class="flex-1">{description}</span>
				{:else}
					<span class="flex-1">{description}</span>
				{/if}
			{/if}
		</Typography>
	{/if}
</div>
