<script lang="ts">
	import Typography, { TypographyVariant } from './Typography.svelte';

	export let src: string;
	let alt = '';
	export let caption = '';
	export let size = undefined;
	export let variant: TypographyVariant = 'body1';
</script>

<figure
	{...$$restProps}
	class={['w-full overflow-hidden shadow-md flex items-center justify-center', $$restProps.class]
		.filter(Boolean)
		.join(' ')}
	style={[
		size
			? `width: ${size}px; height: ${size}px; min-width: ${size}px; min-height: ${size}px`
			: undefined,
		$$restProps.style
	]
		.filter(Boolean)
		.join(';')}
>
	<img {src} {alt} class="min-w-full min-h-full z-10" draggable={false} />
	{#if caption}
		<figcaption class="absolute">
			<Typography {variant}>{caption}</Typography>
		</figcaption>
	{/if}
</figure>
