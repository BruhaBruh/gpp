<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../components_ui/Button.svelte';
	import Checkbox from '../components_ui/Checkbox.svelte';
	import ModalBody from '../components_ui/ModalBody.svelte';
	import ModalFooter from '../components_ui/ModalFooter.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { SetPermissionsDocument, SetPermissionsMutation } from '../generated/graphql';
	import { Permissions } from '../stores/profile';
	import { ui } from '../stores/ui';

	export let user: any;

	const setPermissionsStore = operationStore<SetPermissionsMutation>(SetPermissionsDocument);

	const setPermissions = mutation(setPermissionsStore);

	let permissions = [
		user.permissions & Permissions.Lite,
		user.permissions & Permissions.ModifyForum,
		user.permissions & Permissions.ModifyPermissions,
		user.permissions & Permissions.ModifyRoles,
		user.permissions & Permissions.ModifySocialPoints,
		user.permissions & Permissions.ModifyThread,
		user.permissions & Permissions.Premium,
		user.permissions & Permissions.RemoveBan,
		user.permissions & Permissions.SetBan,
		user.permissions & Permissions.ShowReports
	]
		.filter(Boolean)
		.map((v) => v.toString());

	$: currentPermission = permissions.map((v) => Number(v)).reduce((p, c) => p + c, 0);

	const handleSubmit = () => {
		setPermissions({
			userId: user.userId,
			permissions: currentPermission
		});
	};

	const handleCancel = () => {
		ui.setModal(null);
	};

	$: !!$setPermissionsStore.data && ui.setModal(null);
	$: !!$setPermissionsStore.error && console.log($setPermissionsStore.error);
	$: isLoading = $setPermissionsStore.fetching;
</script>

<ModalBody class="overflow-y-auto">
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.Lite.toString()} bind:group={permissions} />
		<Typography variant="body1" class="text-info-500">Lite</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.Premium.toString()} bind:group={permissions} />
		<Typography variant="body1" class="text-info-500">Premium</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.ModifyForum.toString()} bind:group={permissions} />
		<Typography variant="body1">Редакитрование форума</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.ModifyPermissions.toString()} bind:group={permissions} />
		<Typography variant="body1" class="text-danger-500">Редакитрование прав</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.ModifySocialPoints.toString()} bind:group={permissions} />
		<Typography variant="body1">Редакитрование социального рейтинга</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.ModifyThread.toString()} bind:group={permissions} />
		<Typography variant="body1">Редакитрование закрытых постов на форуме</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.SetBan.toString()} bind:group={permissions} />
		<Typography variant="body1" class="text-warning-500">Выдача бана</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.RemoveBan.toString()} bind:group={permissions} />
		<Typography variant="body1" class="text-warning-500">Удаление бана</Typography>
	</div>
	<div class="flex space-x-2 select-none">
		<Checkbox value={Permissions.ShowReports.toString()} bind:group={permissions} />
		<Typography variant="body1">Просмотр репортов</Typography>
	</div>
	<div class="flex flex-col">
		<Typography variant="helpertext" class="text-danger-500">Опасно</Typography>
		<Typography variant="helpertext" class="text-warning-500">Не рекомендуется</Typography>
		<Typography variant="helpertext">Безобидно</Typography>
		<Typography variant="helpertext" class="text-info-500">Выбери один из вариантов</Typography>
	</div>
</ModalBody>

<ModalFooter>
	<Button color="primary" on:click={handleSubmit} disabled={isLoading}>Установить</Button>
	<Button variant="text" color="secondary" on:click={handleCancel} disabled={isLoading}
		>Отмена</Button
	>
</ModalFooter>
