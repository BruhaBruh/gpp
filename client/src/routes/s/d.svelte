<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import { mutation, operationStore } from '@urql/svelte';
	import Button from '../../components_ui/Button.svelte';
	import Paper from '../../components_ui/Paper.svelte';
	import Switch from '../../components_ui/Switch.svelte';
	import Typography from '../../components_ui/Typography.svelte';
	import { SetSettingsDocument, SetSettingsMutation } from '../../generated/graphql';
	import { checkSettings, profile, Settings } from '../../stores/profile';
	import { ui } from '../../stores/ui';

	let newReport = checkSettings(Settings.NotifyOnReport, $profile.settings);
	let newReportMessage = checkSettings(Settings.NotifyOnReportMessage, $profile.settings);
	let newFriend = checkSettings(Settings.NotifyOnNewFriend, $profile.settings);
	let newSubscriber = checkSettings(Settings.NotifyOnNewSubscriber, $profile.settings);

	const setSettingsStore = operationStore<SetSettingsMutation>(SetSettingsDocument);

	const setSettings = mutation(setSettingsStore);

	const save = () => {
		setSettings({
			userId: $profile.userId,
			r: newReport,
			rm: newReportMessage,
			f: newFriend,
			s: newSubscriber
		});
	};

	$: isLoading = $setSettingsStore.fetching;
	$: !!$setSettingsStore.data && ui.addNotification('Успешно', 'success');
	$: !!$setSettingsStore.error && console.log($setSettingsStore.error);
</script>

<svelte:head>
	<title>Настройки - Discord Уведомления</title>
</svelte:head>

<Paper class="flex flex-col space-y-4 max-w-2xl mx-auto">
	<Typography variant="body2" class="text-gray-400">Уведомления</Typography>
	<div
		class="space-x-2 p-2 px-4 hover:bg-gray-50 hover:bg-opacity-10 rounded-lg transition ease-in flex items-center cursor-pointer select-none"
		on:click={() => (newReport = !newReport)}
	>
		<div class="flex-1">
			<Typography variant="body1">Новая жалоба</Typography>
			<Typography variant="helpertext" class="text-gray-400"
				>При новой жалобе на вас, вам будет отправляться уведомление</Typography
			>
		</div>
		<Switch selected={newReport} />
	</div>
	<div
		class="space-x-2 p-2 px-4 hover:bg-gray-50 hover:bg-opacity-10 rounded-lg transition ease-in flex items-center cursor-pointer select-none"
		on:click={() => (newReportMessage = !newReportMessage)}
	>
		<div class="flex-1">
			<Typography variant="body1">Новое сообщение в репорте</Typography>
			<Typography variant="helpertext" class="text-gray-400"
				>При новом сообщении в репорты, вам будет отправляться уведомление</Typography
			>
		</div>
		<Switch selected={newReportMessage} />
	</div>
	<div
		class="space-x-2 p-2 px-4 hover:bg-gray-50 hover:bg-opacity-10 rounded-lg transition ease-in flex items-center cursor-pointer select-none"
		on:click={() => (newFriend = !newFriend)}
	>
		<div class="flex-1">
			<Typography variant="body1">Новый друг</Typography>
			<Typography variant="helpertext" class="text-gray-400"
				>При новом друге, вам будет отправляться уведомление</Typography
			>
		</div>
		<Switch selected={newFriend} />
	</div>
	<div
		class="space-x-2 p-2 px-4 hover:bg-gray-50 hover:bg-opacity-10 rounded-lg transition ease-in flex items-center cursor-pointer select-none"
		on:click={() => (newSubscriber = !newSubscriber)}
	>
		<div class="flex-1">
			<Typography variant="body1">Новый подписчик</Typography>
			<Typography variant="helpertext" class="text-gray-400"
				>При новом подписчике, вам будет отправляться уведомление</Typography
			>
		</div>
		<Switch selected={newSubscriber} />
	</div>
	<Button disabled={isLoading} on:click={save}>Сохранить</Button>
</Paper>
