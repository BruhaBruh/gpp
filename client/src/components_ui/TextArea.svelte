<script lang="ts" context="module">
	export type TextAreaStatus = 'validation' | 'error' | 'success';
</script>

<script lang="ts">
	import textareaResize from '../actions/textareaResize';
	import Icon from './Icon.svelte';
	import Typography from './Typography.svelte';

	export let placeholder: string = '';
	export let value: string = '';
	export let status: TextAreaStatus = undefined;
	export let isLoading = false;
	export let title: string = '';
	export let description: string = '';
	export let maxLength: number = undefined;
	export let id: string = undefined;
	export let disabled = false;

	let ta: HTMLTextAreaElement;

	$: currentLength = value.trim().length;
</script>

<div {...$$restProps} class={['flex flex-col', $$props.class].join(' ')}>
	{#if !!title}
		<Typography variant="body1" class="text-gray-50">{title}</Typography>
	{/if}
	<div
		class="rounded-lg h-10 transition ease-in flex-1 appearance-none border w-full px-4 bg-gray-700 text-gray-50 placeholder-gray-400 shadow-sm focus-within:outline-none focus-within:ring-2 focus-within:ring-primary-600 font-bold text-base flex items-center select-none"
		class:text-gray-400={disabled || isLoading}
		class:text-gray-50={!disabled && !isLoading}
		class:border-gray-500={!disabled && !isLoading && !status}
		class:hover:border-primary-600={!disabled && !isLoading && !status}
		class:border-danger-500={status === 'error'}
		class:border-warning-500={status === 'validation'}
		class:border-success-500={status === 'success'}
		class:cursor-not-allowed={disabled || isLoading}
		style="min-height: 40px"
		on:click={() => ta.focus()}
	>
		<textarea
			use:textareaResize={value}
			{placeholder}
			bind:value
			class="flex-1 bg-inherit text-inherit placeholder-gray-400  font-bold text-base w-full h-full resize-none scrollbar-hidden"
			class:cursor-not-allowed={disabled || isLoading}
			{maxLength}
			{id}
			bind:this={ta}
			disabled={disabled || isLoading}
		/>
	</div>
	{#if !!description || isLoading || maxLength}
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
			{#if maxLength}
				<span class="flex-1 text-right">{currentLength}/{maxLength}</span>
			{/if}
		</Typography>
	{/if}
</div>
