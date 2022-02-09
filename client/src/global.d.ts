/// <reference types="@sveltejs/kit" />

declare namespace svelte.JSX {
	interface HTMLAttributes<T> {
		onmousewheel?: (event: WheelEvent) => any;
	}
}
