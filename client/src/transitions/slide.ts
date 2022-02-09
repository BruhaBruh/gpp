import { expoIn } from 'svelte/easing';

type SlideFrom = 'right' | 'left' | 'top' | 'bottom';
type SlideOptions = {
	duration?: number;
	from?: SlideFrom;
};
const slide: (node: HTMLElement, options: SlideOptions) => any = (
	node: HTMLElement,
	{ duration = 150, from = 'bottom' }
) => {
	return {
		duration,
		css: (t: any) => {
			const eased = expoIn(t);

			switch (from) {
				case 'bottom':
					return `transform: translateY(${eased * -100 + 125}%);`;
				case 'left':
					return `transform: translateX(${eased * 100 - 125}%);`;
				case 'right':
					return `transform: translateX(${eased * -100 + 125}%);`;
				case 'top':
					return `transform: translateY(${eased * 100 - 125}%);`;
			}
		}
	};
};

export default slide;
