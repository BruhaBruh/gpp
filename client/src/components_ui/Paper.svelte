<script lang="ts">
</script>

<div
	{...$$restProps}
	class={['p-4 rounded-lg shadow-md bg-gray-800', $$restProps.class].filter(Boolean).join(' ')}
>
	<slot />
</div>
