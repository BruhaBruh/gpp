<script context="module" lang="ts">
	export type TypographyVariant =
		| 'h1'
		| 'h2'
		| 'h3'
		| 'h4'
		| 'h5'
		| 'h6'
		| 'body1'
		| 'body2'
		| 'helpertext'
		| 'button'
		| 'caption';
</script>

<script lang="ts">
	export let variant: TypographyVariant;
	export let style: string = undefined;
</script>

{#if variant === 'h1'}
	<h1
		{...$$restProps}
		class={['font-bold text-5xl', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 1<br />Heading 1</slot>
	</h1>
{:else if variant === 'h2'}
	<h2
		{...$$restProps}
		class={['font-bold text-4xl', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 2<br />Heading 2</slot>
	</h2>
{:else if variant === 'h3'}
	<h3
		{...$$restProps}
		class={['font-bold text-3xl', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 3<br />Heading 3</slot>
	</h3>
{:else if variant === 'h4'}
	<h4
		{...$$restProps}
		class={['font-bold text-2xl', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 4<br />Heading 4</slot>
	</h4>
{:else if variant === 'h5'}
	<h5
		{...$$restProps}
		class={['font-bold text-xl', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 5<br />Heading 5</slot>
	</h5>
{:else if variant === 'h6'}
	<h6
		{...$$restProps}
		class={['font-bold text-lg', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Heading 6<br />Heading 6</slot>
	</h6>
{:else if variant === 'body1'}
	<p
		{...$$restProps}
		class={['text-base font-normal', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot
			>Lorem ipsum dolor, sit amet consectetur adipisicing elit. Consequuntur repudiandae blanditiis
			voluptate minima eveniet exercitationem quas velit earum! Nisi, modi?</slot
		>
	</p>
{:else if variant === 'body2'}
	<p
		{...$$restProps}
		class={['text-sm font-normal', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot
			>Lorem ipsum dolor sit amet consectetur adipisicing elit. Ipsum excepturi dolore harum sed,
			sequi porro repellat dolor quasi odio voluptate.</slot
		>
	</p>
{:else if variant === 'helpertext'}
	<span
		{...$$restProps}
		class={['text-xs font-normal', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Lorem, ipsum.</slot>
	</span>
{:else if variant === 'button'}
	<span
		{...$$restProps}
		class={['font-bold text-base', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Button</slot>
	</span>
{:else if variant === 'caption'}
	<span
		{...$$restProps}
		class={['text-sm font-normal uppercase', $$restProps.class].filter(Boolean).join(' ')}
		{style}
	>
		<slot>Caption</slot>
	</span>
{:else}
	<slot>Typography</slot>
{/if}
