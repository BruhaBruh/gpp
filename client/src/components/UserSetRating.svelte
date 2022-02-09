<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../components_ui/Button.svelte';
	import ModalBody from '../components_ui/ModalBody.svelte';
	import ModalFooter from '../components_ui/ModalFooter.svelte';
	import type { SelectValue } from '../components_ui/Select.svelte';
	import Select from '../components_ui/Select.svelte';
	import { SetRatingDocument, SetRatingMutation } from '../generated/graphql';
	import { ui } from '../stores/ui';

	export let user: any;

	let currentRating = user.rating.your === 0 ? 'neutral' : (user.rating.your === 1).toString(); // -1 - False, 0 - undefined, 1 - True

	let ratings: SelectValue[] = [
		{
			value: 'true',
			label: 'Положительный'
		},
		{
			value: 'neutral',
			label: 'Нейтральный'
		},
		{
			value: 'false',
			label: 'Отрицательный'
		}
	];

	const setRatingStore = operationStore<SetRatingMutation>(SetRatingDocument);

	const setRating = mutation(setRatingStore);

	const handleSubmit = () => {
		setRating({
			userId: user.userId,
			isPositive: currentRating === 'neutral' ? undefined : currentRating === 'true'
		});
	};

	const handleCancel = () => {
		ui.setModal(null);
	};

	$: !!$setRatingStore.data && ui.setModal(null);
	$: !!$setRatingStore.error && console.log($setRatingStore.error);
	$: isLoading = $setRatingStore.fetching;
</script>

<ModalBody>
	<Select bind:selectedValue={currentRating} values={ratings} disabled={isLoading} />
</ModalBody>

<ModalFooter>
	<Button color="primary" on:click={handleSubmit} disabled={isLoading}>Установить</Button>
	<Button variant="text" color="secondary" on:click={handleCancel} disabled={isLoading}>
		Отмена
	</Button>
</ModalFooter>
