<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../components_ui/Button.svelte';
	import Input from '../components_ui/Input.svelte';
	import ModalBody from '../components_ui/ModalBody.svelte';
	import ModalFooter from '../components_ui/ModalFooter.svelte';
	import { AddSocialPointsDocument, AddSocialPointsMutation } from '../generated/graphql';
	import { ui } from '../stores/ui';

	export let user: any;

	let value = '';

	$: error = /^(-|)\d*$/i.test(value) ? '' : 'Не является числом';
	$: numberValue = error ? 0 : Number(value);
	$: afterAddSocialPoints = user.socialPoints + numberValue;
	$: description = `Социальный рейтинг после сохранения: ${afterAddSocialPoints}`;

	const addSocialPointsStore = operationStore<AddSocialPointsMutation>(AddSocialPointsDocument);

	const addSocialPoints = mutation(addSocialPointsStore);

	const handleSubmit = () => {
		addSocialPoints({
			userId: user.userId,
			socialPoints: numberValue
		});
	};

	const handleCancel = () => {
		ui.setModal(null);
	};

	$: !!$addSocialPointsStore.data && ui.setModal(null);
	$: !!$addSocialPointsStore.error && console.log($addSocialPointsStore.error);
	$: isLoading = $addSocialPointsStore.fetching;
</script>

<ModalBody>
	<Input bind:value status={error ? 'error' : undefined} description={error || description} />
</ModalBody>

<ModalFooter>
	<Button color="primary" on:click={handleSubmit} disabled={isLoading}>Сохранить</Button>
	<Button variant="text" color="secondary" on:click={handleCancel} disabled={isLoading}
		>Отмена</Button
	>
</ModalFooter>
