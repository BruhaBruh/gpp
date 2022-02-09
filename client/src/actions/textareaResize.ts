const textareaResize = (node: HTMLTextAreaElement, value: any) => {
	const resize = () => {
		if (!node) return;
		node.style.height = '1px';
		node.style.height = node.scrollHeight + 'px';
	};

	resize();

	return {
		update(value) {
			resize();
		}
	};
};

export default textareaResize;
