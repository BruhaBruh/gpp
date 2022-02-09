<script lang="ts">
	import { scale } from 'svelte/transition';

	export let group: any = undefined;
	export let disabled: boolean = false;
	export let error: boolean = false;
	export let value: any = undefined;
	export let name: string = undefined;
	export let id: string = undefined;
</script>

<label class="radio relative block cursor-pointer">
	<input
		class="visually-hidden"
		type="radio"
		bind:group
		{disabled}
		{value}
		data-error={error}
		{name}
		{id}
	/>
	<span
		class="radio__checkmark w-6 h-6 border border-gray-500 rounded-full overflow-hidden hover:border-primary-700 transition ease-in flex items-center justify-center"
	>
		{#if group === value}
			<div transition:scale={{ duration: 150 }} class="h-3.5 w-3.5 rounded-full bg-current" />
		{/if}
	</span>
</label>

<style>
	.radio input:disabled ~ .radio__checkmark {
		@apply border-gray-700;
	}
	.radio input:checked:disabled ~ .radio__checkmark {
		@apply text-gray-700;
	}

	.radio input[data-error='true'] ~ .radio__checkmark {
		@apply border-danger-600;
	}
	.radio input[data-error='true']:checked ~ .radio__checkmark {
		@apply text-danger-600;
	}

	.radio input[data-error='false']:checked:not(:disabled) ~ .radio__checkmark {
		@apply border-primary-600;
	}
	.radio input:checked ~ .radio__checkmark {
		@apply text-primary-600;
	}

	.radio input:focus ~ .radio__checkmark {
		@apply ring-2 ring-primary-600 ring-opacity-50;
	}
</style>
