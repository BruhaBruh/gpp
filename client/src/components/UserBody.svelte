<script lang="ts" context="module">
	export type UserPage = 'info' | 'friends' | 'subscribers';
</script>

<script lang="ts">
	import Button from '../components_ui/Button.svelte';
	import Icon from '../components_ui/Icon.svelte';
	import Paper from '../components_ui/Paper.svelte';
	import Tooltip from '../components_ui/Tooltip.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { UserRoleEnum } from '../generated/graphql';
	import { getUserRoleString, isHaveSubscription, isPremium } from '../stores/profile';
	import isoTimeToPhrase from '../utils/isoTimeToPhrase';
	import UserBodyInfo from './UserBodyInfo.svelte';
	import UserBodyUserList from './UserBodyUserList.svelte';

	export let user: any;

	let page: UserPage = 'info';

	$: friends = user.friendUsers.map((v) => v.friendNavigation);
	$: subscribers = user.subscriberUsers.map((v) => v.subscriberNavigation);
</script>

<div class="flex-1 flex flex-col space-y-4">
	<Paper class="flex-1 flex flex-col space-y-1">
		<div class="flex flex-col sm:flex-row items-baseline sm:space-x-4 flex-wrap">
			<Typography variant="h4">{user.nickname}</Typography>
			<div class="flex items-center space-x-1">
				<Typography variant="h6" class="text-gray-400">
					{isoTimeToPhrase(user.lastOnline)}
				</Typography>
			</div>
		</div>
		{#if user.discordRoles || user.userRole !== UserRoleEnum.None || isHaveSubscription(user.permissions)}
			<Typography variant="body2" class="text-gray-200 flex items-center space-x-1">
				{user.discordRoles[0]?.name ?? ''}
				{#if user.userRole !== UserRoleEnum.None}
					<Tooltip popupText={getUserRoleString(user.userRole)}>
						<Icon name="user-with-badge" class="fill-primary-400" size={20} />
					</Tooltip>
				{/if}
				{#if isHaveSubscription(user.permissions)}
					{#if isPremium(user.permissions)}
						<Tooltip popupText={'Premium'}>
							<Icon name="crown" size={20} class="fill-premium" />
						</Tooltip>
					{:else}
						<Tooltip popupText={'Lite'}>
							<Icon name="crown" size={20} class="fill-lite" />
						</Tooltip>
					{/if}
				{/if}
			</Typography>
		{/if}
		{#if user.status}
			<Typography variant="body2" class="text-gray-200">
				{user.status}
			</Typography>
		{/if}
	</Paper>
	<div class="grid sm:grid-cols-3 gap-4">
		<Button on:click={() => (page = 'info')} color={page === 'info' ? 'primary' : 'info'}>
			Информация
		</Button>
		<Button on:click={() => (page = 'friends')} color={page === 'friends' ? 'primary' : 'info'}>
			Друзья
		</Button>
		<Button
			on:click={() => (page = 'subscribers')}
			color={page === 'subscribers' ? 'primary' : 'info'}
		>
			Подписчики
		</Button>
	</div>
	{#if page === 'info'}
		<UserBodyInfo {user} />
	{:else if page === 'friends'}
		<UserBodyUserList users={friends} emptyLabel="Нет друзей" />
	{:else if page === 'subscribers'}
		<UserBodyUserList users={subscribers} emptyLabel="Нет подписчиков" />
	{/if}
</div>
