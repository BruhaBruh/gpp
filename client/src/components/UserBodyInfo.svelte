<script lang="ts">
	import { goto } from '$app/navigation';
	import { slide } from 'svelte/transition';
	import Plate from '../components_ui/Plate.svelte';
	import Tooltip from '../components_ui/Tooltip.svelte';
	import Typography from '../components_ui/Typography.svelte';
	import { getSex, profile } from '../stores/profile';
	import { ui } from '../stores/ui';
	import UserSetRating from './UserSetRating.svelte';

	export let user: any;

	$: sex = user ? getSex(user.sex) : getSex(0);

	$: isOwner = user ? $profile.userId === user.userId : false;

	const handleClickOnRatingPlate = () => {
		isOwner
			? goto(`/u/r`)
			: ui.setModal({
					header: 'Установка рейтинга',
					body: UserSetRating,
					props: {
						user
					}
			  });
	};
</script>

<div
	transition:slide={{ duration: 150 }}
	class="grid sm:grid-cols-2 md:grid-cols-3 xl:grid-cols-4 2xl:grid-cols-5 gap-4"
>
	{#if user.isBanned}
		<Plate
			iconClass="from-danger-600 to-danger-400 text-gray-900"
			iconName="cancel"
			name="Забанен"
			label={'Помянем'}
		/>
	{/if}
	{#if $profile.isLoggedIn}
		<Tooltip
			popupText={isOwner
				? 'Нажми, чтобы открыть список оценивших'
				: 'Нажми, чтобы поставить рейтинг'}
		>
			<Plate
				iconClass="from-success-600 to-success-400 text-gray-900"
				iconName="tops"
				name="Рейтинг"
				label={user.rating.result}
				on:click={handleClickOnRatingPlate}
				withProgress
				progressWidthPercent={(user.rating.positive / user.rating.total) * 100}
			/>
		</Tooltip>
	{:else}
		<Plate
			iconClass="from-success-600 to-success-400 text-gray-900"
			iconName="tops"
			name="Рейтинг"
			label={user.rating.result}
			withProgress
			progressWidthPercent={(user.rating.positive / user.rating.total) * 100}
		/>
	{/if}
	<Plate
		iconClass="from-primary-600 to-primary-400 text-gray-900"
		iconName="user-group"
		name="Cоц. Рейтинг"
		label={user.socialPoints}
	/>
	<Plate
		iconClass="from-info-600 to-info-400 text-gray-900"
		iconName="eye"
		name="Просмотры"
		label={user.views}
	/>
	<Plate
		iconClass="from-warning-500 to-warning-400 text-gray-900"
		iconName="age"
		name="Лет в городе"
		label={user.level}
	/>
	<Plate
		iconClass="from-danger-500 to-danger-400 text-gray-900"
		iconName="user"
		name="Пол"
		label={sex}
	/>
	<Plate
		iconClass="from-danger-500 to-warning-400 text-gray-900"
		iconName="users"
		name="Друзей"
		label={user.friendUsers.length}
	/>
	<Plate
		iconClass="from-primary-500 to-danger-400 text-gray-900"
		iconName="users"
		name="Подписчиков"
		label={user.subscriberUsers.length}
	/>
</div>

{#if user.description}
	<div class="p-4 rounded-lg shadow-md bg-gray-800" transition:slide={{ duration: 150 }}>
		<Typography variant="h4">Описание</Typography>
		<Typography variant="body1" class="whitespace-pre-wrap">{user.description}</Typography>
	</div>
{/if}
