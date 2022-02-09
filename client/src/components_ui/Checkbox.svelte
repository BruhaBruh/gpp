<script lang="ts">
	import { scale } from 'svelte/transition';
	import Icon from './Icon.svelte';

	export let group: any[] = [];
	export let disabled: boolean = false;
	export let error: boolean = false;
	export let value: any = undefined;
	export let name: string = undefined;
	export let id: string = undefined;

	const onChange = (e: Event) => {
		const { value, checked } = e.target as HTMLInputElement;
		if (checked) {
			group = [...group, value];
		} else {
			group = group.filter((item) => item !== value);
		}
	};
</script>

<label class="relative block cursor-pointer checkbox">
	<input
		class="visually-hidden"
		type="checkbox"
		checked={group.includes(value)}
		{disabled}
		{value}
		data-error={error}
		on:change={onChange}
		{name}
		{id}
	/>
	<span
		class="checkbox__checkmark block w-6 h-6 border border-gray-500 rounded overflow-hidden hover:border-primary-700 transition ease-in text-gray-50"
	>
		{#if group.includes(value)}
			<div transition:scale={{ duration: 150 }}>
				<Icon name="check" class="w-full h-full fill-gray-50" />
			</div>
		{/if}
	</span>
</label>

<style>
	.checkbox input:disabled ~ .checkbox__checkmark {
		@apply border-gray-700 cursor-not-allowed;
	}
	.checkbox input:checked:disabled ~ .checkbox__checkmark {
		@apply bg-gray-700 text-gray-400;
	}

	.checkbox input[data-error='true'] ~ .checkbox__checkmark {
		@apply border-danger-600;
	}
	.checkbox input[data-error='true']:checked ~ .checkbox__checkmark {
		@apply bg-danger-600;
	}

	.checkbox input[data-error='false']:checked:not(:disabled) ~ .checkbox__checkmark {
		@apply border-primary-600 bg-primary-600;
	}

	.checkbox input:focus ~ .checkbox__checkmark {
		@apply ring-2 ring-primary-600 ring-opacity-50;
	}
</style>
