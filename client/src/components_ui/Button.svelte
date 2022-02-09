<script context="module" lang="ts">
	export type ButtonVariant = 'standart' | 'text';
	export type ButtonColor = 'primary' | 'secondary' | 'info' | 'success' | 'warning' | 'danger';
	export type ButtonSize = 'large' | 'base' | 'small';
</script>

<script lang="ts">
	import { goto } from '$app/navigation';
	import { createEventDispatcher } from 'svelte';
	import Typography from './Typography.svelte';

	export let variant: ButtonVariant = 'standart';
	export let color: ButtonColor = 'primary';
	export let size: ButtonSize = 'base';
	export let disabled = false;
	export let href = '';
	export let to = '';
	export let type = 'button';
	export let disableRipple = false;

	const dispatch = createEventDispatcher();

	let button: HTMLButtonElement;

	const handleClick = (e: MouseEvent) => {
		dispatch('click', e);
		if (!!href) window.location.replace(href);
		if (!!to) goto(to);
		if (disableRipple) return;

		const rippleDiameter =
			Math.max(button.clientHeight, button.clientWidth) +
			Math.min(button.clientHeight, button.clientWidth);
		const rippleRadius = rippleDiameter / 2;

		const ripple = document.createElement('span');

		let offsetX = e.clientX - button.offsetLeft;
		let offsetY = e.clientY - button.offsetTop;

		ripple.classList.add(
			variant === 'standart'
				? ['primary', 'info', 'danger'].includes(color)
					? 'bg-gray-50'
					: 'bg-gray-900'
				: 'bg-gray-50',
			'bg-opacity-30',
			'rounded-full',
			'absolute',
			'opacity-0'
		);
		ripple.style.height = `${rippleDiameter}px`;
		ripple.style.width = `${rippleDiameter}px`;
		ripple.style.top = `${offsetY - rippleRadius}px`;
		ripple.style.left = `${offsetX - rippleRadius}px`;

		button.appendChild(ripple);

		ripple.animate(
			[
				{ transform: 'scale(0)', opacity: '1' },
				{ transform: 'scale(1)', opacity: '0' }
			],
			{
				duration: 200
			}
		);
		setTimeout(() => {
			ripple.remove();
		}, 250);
	};

	$: buttonSizeClass = [
		size === 'large' ? 'h-12' : undefined,
		size === 'base' ? 'h-10' : undefined,
		size === 'small' ? 'h-8' : undefined
	]
		.filter(Boolean)
		.join(' ');
</script>

{#if variant === 'standart'}
	<button
		{type}
		{disabled}
		on:click={handleClick}
		bind:this={button}
		class={[
			'focus:ring-offset-gray-900 transition ease-in text-center focus:ring-2 focus:ring-offset-2 select-none disabled:bg-gray-700 disabled:text-gray-400 disabled:cursor-not-allowed overflow-hidden space-x-2 flex items-center',
			disableRipple ? '' : 'relative',
			buttonSizeClass,
			$$restProps.class
		]
			.filter(Boolean)
			.join(' ')}
		class:px-8={size === 'large'}
		class:px-4={size === 'base'}
		class:px-3={size === 'small'}
		class:rounded-xl={size === 'large'}
		class:rounded-lg={size === 'base'}
		class:rounded-md={size === 'small'}
		class:shadow-lg={size === 'large'}
		class:shadow-md={size === 'base'}
		class:shadow-sm={size === 'small'}
		class:text-gray-50={['primary', 'info', 'danger'].includes(color)}
		class:text-gray-900={['secondary', 'success', 'warning'].includes(color)}
		class:bg-primary-600={color === 'primary'}
		class:hover:bg-primary-700={color === 'primary'}
		class:focus:ring-primary-500={color === 'primary'}
		class:bg-gray-200={color === 'secondary'}
		class:hover:bg-gray-300={color === 'secondary'}
		class:focus:ring-gray-100={color === 'secondary'}
		class:bg-info-600={color === 'info'}
		class:hover:bg-info-700={color === 'info'}
		class:focus:ring-info-500={color === 'info'}
		class:bg-success-600={color === 'success'}
		class:hover:bg-success-700={color === 'success'}
		class:focus:ring-success-500={color === 'success'}
		class:bg-warning-600={color === 'warning'}
		class:hover:bg-warning-700={color === 'warning'}
		class:focus:ring-warning-500={color === 'warning'}
		class:bg-danger-600={color === 'danger'}
		class:hover:bg-danger-700={color === 'danger'}
		class:focus:ring-danger-500={color === 'danger'}
	>
		<slot name="start" />
		<Typography variant="button" class="text-center flex-1"><slot /></Typography>
		<slot name="end" />
	</button>
{:else}
	<button
		{type}
		{disabled}
		on:click={handleClick}
		bind:this={button}
		class={[
			'transition ease-in text-center select-none disabled:text-gray-700 disabled:cursor-not-allowed overflow-hidden space-x-2 flex items-center',
			disableRipple ? '' : 'relative',
			buttonSizeClass,
			$$restProps.class
		]
			.filter(Boolean)
			.join(' ')}
		class:px-8={size === 'large'}
		class:px-4={size === 'base'}
		class:px-3={size === 'small'}
		class:rounded-xl={size === 'large'}
		class:rounded-lg={size === 'base'}
		class:rounded-md={size === 'small'}
		class:text-primary-600={color === 'primary'}
		class:hover:text-primary-700={color === 'primary'}
		class:text-gray-200={color === 'secondary'}
		class:hover:text-gray-300={color === 'secondary'}
		class:text-info-600={color === 'info'}
		class:hover:text-info-700={color === 'info'}
		class:text-success-600={color === 'success'}
		class:hover:text-success-700={color === 'success'}
		class:text-warning-600={color === 'warning'}
		class:hover:text-warning-700={color === 'warning'}
		class:text-danger-600={color === 'danger'}
		class:hover:text-danger-700={color === 'danger'}
	>
		<slot name="start" />
		<Typography variant="button" class="text-center flex-1"><slot /></Typography>
		<slot name="end" />
	</button>
{/if}
