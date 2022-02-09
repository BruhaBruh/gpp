<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../components_ui/Button.svelte';
	import ModalBody from '../components_ui/ModalBody.svelte';
	import ModalFooter from '../components_ui/ModalFooter.svelte';
	import Select, { SelectValue } from '../components_ui/Select.svelte';
	import { SetUserRoleDocument, SetUserRoleMutation, UserRoleEnum } from '../generated/graphql';
	import { ui } from '../stores/ui';

	export let user: any;

	let userRole = user.userRole;

	let userRoles: SelectValue[] = [
		{
			value: UserRoleEnum.None,
			label: 'Пользователь'
		},
		{
			value: UserRoleEnum.JrModerator,
			label: 'Мл. Модератор'
		},
		{
			value: UserRoleEnum.Moderator,
			label: 'Модератор'
		},
		{
			value: UserRoleEnum.Admin,
			label: 'Администратор'
		}
	];

	const setUserRoleStore = operationStore<SetUserRoleMutation>(SetUserRoleDocument);

	const setUserRole = mutation(setUserRoleStore);

	const handleSubmit = () => {
		setUserRole({
			userId: user.userId,
			userRole
		});
	};

	const handleCancel = () => {
		ui.setModal(null);
	};

	$: !!$setUserRoleStore.data && ui.setModal(null);
	$: !!$setUserRoleStore.error && console.log($setUserRoleStore.error);
	$: isLoading = $setUserRoleStore.fetching;
</script>

<ModalBody>
	<Select bind:selectedValue={userRole} values={userRoles} disabled={isLoading} />
</ModalBody>

<ModalFooter>
	<Button color="primary" on:click={handleSubmit} disabled={isLoading}>Установить</Button>
	<Button variant="text" color="secondary" on:click={handleCancel} disabled={isLoading}>
		Отмена
	</Button>
</ModalFooter>
