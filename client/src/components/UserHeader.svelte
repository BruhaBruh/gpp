<script lang="ts">
	import { mutation, operationStore, query } from '@urql/svelte';
	import Avatar from '../components_ui/Avatar.svelte';
	import Button from '../components_ui/Button.svelte';
	import {
		EndSubscribeDocument,
		EndSubscribeMutation,
		RemoveFriendDocument,
		RemoveFriendMutation,
		SetBanDocument,
		SetBanMutation,
		StartSubscribeDocument,
		StartSubscribeMutation,
		StatusDocument,
		StatusQuery,
		UserRoleEnum
	} from '../generated/graphql';
	import { checkPermissions, checkPermissionsWA, Permissions, profile } from '../stores/profile';
	import { ui } from '../stores/ui';
	import UserEditForm from './UserEditForm.svelte';
	import UserSetPermissions from './UserSetPermissions.svelte';
	import UserSetRole from './UserSetRole.svelte';
	import UserSetSocialPoints from './UserSetSocialPoints.svelte';

	export let user: any;
	const statusStore = operationStore<StatusQuery>(
		StatusDocument,
		{ userId: user.userId },
		{ requestPolicy: 'cache-first' }
	);
	const startSubscribeStore = operationStore<StartSubscribeMutation>(StartSubscribeDocument, {
		userId: user.userId
	});
	const endSubscribeStore = operationStore<EndSubscribeMutation>(EndSubscribeDocument, {
		userId: user.userId
	});
	const removeFriendStore = operationStore<RemoveFriendMutation>(RemoveFriendDocument, {
		userId: user.userId
	});
	const setBanStore = operationStore<SetBanMutation>(SetBanDocument);

	const startSubscribe = mutation(startSubscribeStore);
	const endSubscribe = mutation(endSubscribeStore);
	const removeFriend = mutation(removeFriendStore);
	const setBan = mutation(setBanStore);

	$: isOwner = user ? $profile.userId === user.userId : false;
	$: canSetBan = user
		? checkPermissions(Permissions.SetBan, $profile.permissions) &&
		  !isOwner &&
		  !user.isBanned &&
		  user.userRole === UserRoleEnum.None
		: false;
	$: canRemoveBan = user
		? checkPermissions(Permissions.RemoveBan, $profile.permissions) &&
		  !isOwner &&
		  user.isBanned &&
		  user.userRole === UserRoleEnum.None
		: false;
	$: canSetRole = user
		? checkPermissions(Permissions.ModifyRoles, $profile.permissions) &&
		  !isOwner &&
		  !checkPermissionsWA(Permissions.All, user.permissions)
		: false;
	$: canEditPermissions = user
		? checkPermissions(Permissions.ModifyPermissions, $profile.permissions) &&
		  !isOwner &&
		  !checkPermissionsWA(Permissions.All, user.permissions)
		: false;

	$: canSetSocialPoints = user
		? checkPermissions(Permissions.ModifySocialPoints, $profile.permissions) && !isOwner
		: false;

	$: {
		if ($profile.userId !== user.userId && !$statusStore.data) {
			query(statusStore);
		}
	}

	$: ($startSubscribeStore.data ||
		$endSubscribeStore.data ||
		$removeFriendStore.data ||
		$setBanStore.data) &&
		statusStore.reexecute({ requestPolicy: 'network-only' });

	const handleEdit = () =>
		ui.setModal({
			header: 'Редактирование',
			body: UserEditForm,
			props: {
				user
			}
		});

	const handleSetRole = () =>
		ui.setModal({
			header: 'Выбор роли',
			body: UserSetRole,
			props: {
				user
			}
		});

	const handleSetPermissions = () =>
		ui.setModal({
			header: 'Установка прав',
			body: UserSetPermissions,
			props: {
				user
			}
		});

	const handleSetSocialPoints = () =>
		ui.setModal({
			header: 'Добавление соц. рейтинга',
			body: UserSetSocialPoints,
			props: {
				user
			}
		});
</script>

<div class="flex flex-col items-stretch space-y-4 lg:max-w-xs flex-1">
	<Avatar
		src={user.avatar}
		caption={user.nickname
			.split(' ')
			.map((v) => v.split('')[0])
			.join('')}
		variant={'h1'}
		class="bg-gray-800 rounded-lg lg:max-w-xs sm:aspect-square relative"
	/>
	{#if $statusStore.data}
		{#if !isOwner && $statusStore.data.status.heIsSubscriber}
			<Button on:click={() => startSubscribe()}>Добавить в друзья</Button>
		{:else if !isOwner && $statusStore.data.status.isFriends}
			<Button on:click={() => removeFriend()}>Удалить из друзей</Button>
		{:else if !isOwner && $statusStore.data.status.youIsSubscriber}
			<Button on:click={() => endSubscribe()}>Отписаться</Button>
		{:else}
			<Button on:click={() => startSubscribe()}>Подписаться</Button>
		{/if}
	{/if}
	{#if isOwner}
		<Button on:click={handleEdit}>Редактировать</Button>
	{/if}
	{#if canSetSocialPoints}
		<Button color="info" on:click={handleSetSocialPoints}>Добавить соц. рейтинг</Button>
	{/if}
	{#if canSetRole}
		<Button color="info" on:click={handleSetRole}>Установить роль</Button>
	{/if}
	{#if canEditPermissions}
		<Button color="info" on:click={handleSetPermissions}>Установить права</Button>
	{/if}
	{#if canSetBan}
		<Button color="danger" on:click={() => setBan({ userId: user.userId, isBanned: true })}
			>Забанить</Button
		>
	{/if}
	{#if canRemoveBan}
		<Button color="danger" on:click={() => setBan({ userId: user.userId, isBanned: false })}
			>Разбанить</Button
		>
	{/if}
</div>
