<script lang="ts">
	import Avatar from '../components_ui/Avatar.svelte';
	import Icon from '../components_ui/Icon.svelte';
	import Tooltip from '../components_ui/Tooltip.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { UserRoleEnum } from '../generated/graphql';
	import { getUserRoleString, isHaveSubscription, isPremium } from '../stores/profile';
	import isoTimeToPhrase from '../utils/isoTimeToPhrase';

	export let user: any;
	export let customLabel: string = undefined;

	$: href = '/u/' + user.userId;

	$: isOnline = isoTimeToPhrase(user.lastOnline) === 'Онлайн';
</script>

<button
	{...$$restProps}
	on:click={() => window.location.replace(href)}
	class={['bg-gray-800 shadow-md rounded-lg p-2 relative flex space-x-2', $$restProps.class]
		.filter(Boolean)
		.join(' ')}
>
	<div class="absolute top-0 left-0 w-full h-full rounded-lg overflow-hidden">
		<Avatar
			src={user.banner}
			class="absolute top-1/2 left-1/2 -translate-x-1/2 -translate-y-1/2 object-fill object-center brightness-50"
		/>
	</div>

	<Avatar
		src={user.avatar}
		size={64}
		class="rounded-xl relative aspect-square border border-gray-600"
		caption={user.nickname
			.split(' ')
			.map((v) => v.split('')[0])
			.join('')}
	/>
	<div class="grid max-w-full h-full z-10">
		<div class="max-w-full grid" style="grid-template-columns: auto 1fr">
			<Typography variant="h6" class="truncate text-left items-center flex">
				{user.nickname}
			</Typography>
			<div class="flex justify-start items-center w-full">
				<Tooltip popupText={isoTimeToPhrase(user.lastOnline)}>
					<Icon name="circle" size={20} class={isOnline ? 'fill-success-400' : 'fill-danger-400'} />
				</Tooltip>
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
			</div>
		</div>
		<Typography variant="body1" class="text-gray-400 truncate text-left"
			>{customLabel ?? user.discordRoles[0]?.name ?? ''}</Typography
		>
	</div>
</button>
