<script lang="ts">
	export let disabled: boolean = false;
	export let selected: boolean = false;

	const handleClick = () => {
		if (disabled) return;
		selected = !selected;
	};
</script>

<button
	class="w-12 h-6 p-0.5 rounded-full border border-transparent transition ease-in flex"
	class:hover:border-primary-600={!disabled}
	class:focus:ring-2={!disabled}
	class:focus:ring-primary-600={!disabled}
	class:focus:ring-opacity-50={!disabled}
	class:bg-gray-500={!selected && !disabled}
	class:bg-primary-700={selected && !disabled}
	class:bg-gray-darker={disabled}
	on:click={handleClick}
>
	<div
		class="rounded-full aspect-square h-full transition"
		class:ml-0={!selected}
		class:translate-x-6={selected}
		class:bg-gray-50={!disabled}
		class:bg-gray-400={disabled}
	/>
</button>
