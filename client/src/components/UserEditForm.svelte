<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../components_ui/Button.svelte';
	import Input from '../components_ui/Input.svelte';
	import ModalBody from '../components_ui/ModalBody.svelte';
	import ModalFooter from '../components_ui/ModalFooter.svelte';
	import Select, { SelectValue } from '../components_ui/Select.svelte';
	import TextArea from '../components_ui/TextArea.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { SaveUserDocument, SaveUserMutation } from '../generated/graphql';
	import { ui } from '../stores/ui';

	export let user: any;

	let avatar = user.avatar;
	let banner = user.banner;
	let sex = user.sex;
	let status = user.status;
	let description = user.description;

	let sexValues: SelectValue[] = [
		{
			value: 0,
			label: 'Не указан'
		},
		{
			value: 1,
			label: 'Мужчина'
		},
		{
			value: 2,
			label: 'Женщина'
		}
	];

	const saveUserStore = operationStore<SaveUserMutation>(SaveUserDocument);

	const saveUser = mutation(saveUserStore);

	const handleSubmit = () => {
		saveUser({
			userId: user.userId,
			avatar: avatar.trim(),
			banner: banner.trim(),
			description: description.trim(),
			status: status.trim(),
			sex
		});
	};

	const handleCancel = () => {
		ui.setModal(null);
	};

	const mediaLinkPattern =
		/^((https:\/\/(i\.postimg\.cc|i\.imgur\.com|c\.tenor\.com)\/.*(\.jpeg|\.jpg|\.gif|\.png|\.webp))|)$/i;

	$: statusError = status.trim().length > 128 ? 'Максимальная длина 128 символов' : '';
	$: descriptionError = description.trim().length > 6000 ? 'Максимальная длина 6000 символов' : '';
	$: avatarError =
		avatar.trim().length > 128
			? 'Максимальная длина 128 символов'
			: mediaLinkPattern.test(avatar.trim())
			? ''
			: 'Ссылка должна быть с imgur/tenor/postimg и gif/jpg/jpeg/png/webp';
	$: bannerError =
		banner.trim().length > 128
			? 'Максимальная длина 128 символов'
			: mediaLinkPattern.test(banner.trim())
			? ''
			: 'Ссылка должна быть с imgur/tenor/postimg и gif/jpg/jpeg/png/webp';

	$: !!$saveUserStore.data && ui.setModal(null);
	$: !!$saveUserStore.error && console.log($saveUserStore.error);
	$: isLoading = $saveUserStore.fetching;
</script>

<ModalBody class={'overflow-y-scroll'}>
	<Input
		title="Аватар"
		bind:value={avatar}
		disabled={isLoading}
		status={avatarError ? 'error' : undefined}
		description={avatarError}
	/>
	<Input
		title="Баннер"
		bind:value={banner}
		disabled={isLoading}
		status={bannerError ? 'error' : undefined}
		description={bannerError}
	/>
	<div class="flex flex-col">
		<Typography variant="body1" class="text-gray-50">Пол</Typography>
		<Select values={sexValues} bind:selectedValue={sex} disabled={isLoading} />
	</div>
	<Input
		title="Статус"
		bind:value={status}
		status={statusError ? 'error' : undefined}
		description={statusError}
		disabled={isLoading}
	/>
	<TextArea
		title="Описание"
		bind:value={description}
		disabled={isLoading}
		status={descriptionError ? 'error' : undefined}
		description={descriptionError}
		maxLength={6000}
	/>
</ModalBody>

<ModalFooter>
	<Button color="primary" on:click={handleSubmit} disabled={isLoading}>Сохранить</Button>
	<Button variant="text" color="secondary" on:click={handleCancel} disabled={isLoading}>
		Отмена
	</Button>
</ModalFooter>
