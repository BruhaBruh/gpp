<script lang="ts" context="module">
	import { browser } from '$app/env';
	import { mutation, operationStore, query, setClient } from '@urql/svelte';
	import { onDestroy, onMount } from 'svelte';
	import { writable } from 'svelte/store';
	import '../app.css';
	import ActionSheet from '../components/ActionSheet.svelte';
	import ActionSheetItem from '../components/ActionSheetItem.svelte';
	import CookieAlert from '../components/CookieAlert.svelte';
	import Sidebar from '../components/Sidebar.svelte';
	import type { NavigationItem } from '../components/SidebarNavigationItem.svelte';
	import IconButton from '../components_ui/IconButton.svelte';
	import Modal from '../components_ui/Modal.svelte';
	import ModalHeader from '../components_ui/ModalHeader.svelte';
	import NotificationsContainer from '../components_ui/NotificationsContainer.svelte';
	import { GetMeDocument, GetMeQuery, LoginDocument, LoginMutation } from '../generated/graphql';
	import { profile } from '../stores/profile';
	import { ui } from '../stores/ui';
	import { GraphQLException } from '../types/graphqlExceptions';
	import _client from '../_client';
	export const prerender = true;

	const isLoggedNavigation: NavigationItem[] = [
		{ label: 'Профиль', icon: 'user', to: '/u/1' },
		{ label: 'Профили', icon: 'users', to: '/u' },
		{ label: 'Форум', icon: 'chats', to: '/f' },
		{ label: 'Репорты', icon: 'report', to: '/r', iconColor: 'warning' },
		{ label: 'Донат', icon: 'diamond', to: '/d', iconColor: 'info' },
		{ label: 'Топы', icon: 'tops', to: '/t' },
		{ label: 'Настройки', icon: 'settings', to: '/s' },
		{ label: 'Оповещения', icon: 'notification', to: '/n' },
		{ label: 'Статистика', icon: 'statistics', to: '/stat' },
		{ label: 'Выйти', icon: 'logout', href: '/api/auth/logout', iconColor: 'danger' }
	];

	const isNotLoggedNavigation: NavigationItem[] = [
		{ label: 'Профили', icon: 'users', to: '/u' },
		{ label: 'Топы', icon: 'tops', to: '/t' },
		{ label: 'Статистика', icon: 'statistics', to: '/stat' },
		{ label: 'Войти', icon: 'login', href: '/api/auth/login', iconColor: 'primary' }
	];

	export const navigationItems = writable<NavigationItem[]>(isNotLoggedNavigation);

	profile.subscribe((p) => {
		if (p.isLoggedIn) {
			navigationItems.set(
				isLoggedNavigation.map((v) => {
					if (v.icon !== 'user') return v;
					return { label: 'Профиль', icon: 'user', to: `/u/${p.userId}` };
				})
			);
		} else {
			navigationItems.set(isNotLoggedNavigation);
		}
	});
</script>

<script lang="ts">
	let isOpenActionSheet = false;

	$: showCookieAlert = browser ? !window.localStorage.getItem('cookie_accept') : false;

	let isEnableScrollUp = false;
	let main: HTMLElement;

	const handleScroll = (e: UIEvent) => {
		isEnableScrollUp = window.innerHeight * 2 < main.scrollTop;
	};

	// #region Авторизация и подсос данных о пользователе
	setClient(_client);

	const loginStore = operationStore<LoginMutation>(LoginDocument);
	const me = operationStore<GetMeQuery>(GetMeDocument);

	const login = mutation(loginStore);

	let timer: NodeJS.Timer;

	onMount(() => {
		login();
	});

	onDestroy(() => {
		if (timer) clearInterval(timer);
	});

	const onIsLogged = () => {
		profile.setIsLoggedIn(true);
		query(me);
		if (timer) clearInterval(timer);
		timer = setInterval(() => query(me), 10000);
	};

	const onIsNotLogged = (isBeenLogged: boolean) => {
		profile.reset();
		if ($loginStore.error) {
			switch ($loginStore.error.graphQLErrors[0]?.extensions?.code) {
				case GraphQLException.Auth: {
					ui.addNotification('Вы не авторизованы', 'warning', undefined, undefined, 1500);
					break;
				}
				case GraphQLException.Forbidden: {
					ui.addNotification($loginStore.error.graphQLErrors[0].message, 'danger');
					break;
				}
				case GraphQLException.Internal: {
					ui.addNotification('Ошибка авторизации', 'warning', 'Попробуйте позже');
					break;
				}
				default:
					console.log($loginStore.error);
			}
		} else if ($me.error) {
			switch ($me.error.graphQLErrors[0]?.extensions?.code) {
				case GraphQLException.Internal: {
					ui.addNotification('Ошибка сервера', 'warning', 'Попробуйте позже');
					break;
				}
				case GraphQLException.DoesNotExists: {
					ui.addNotification($me.error.graphQLErrors[0].message, 'warning', 'Как ты это сделал?!');
					break;
				}
				default: {
					console.log($me.error);
				}
			}
		}
		if (timer) clearInterval(timer);
		if (isBeenLogged) timer = setInterval(() => login(), 5000);
	};

	const onMeIsOk = () => {
		profile.setAvatar($me.data.me.avatar);
		profile.setBanReportEndAt($me.data.me.banreportEndAt ?? null);
		profile.setSubscriptionEndAt($me.data.me.subscriptionEndAt ?? null);
		profile.setNickname($me.data.me.nickname);
		profile.setPermissions($me.data.me.permissions);
		profile.setSettings($me.data.me.settings);
		profile.setUserId($me.data.me.userId);
		profile.setUserRole($me.data.me.userRole);
	};

	$: $loginStore.data?.login && onIsLogged();
	$: $loginStore.error && onIsNotLogged(false);
	$: $me.error && onIsNotLogged(true);
	$: $me.data?.me && onMeIsOk();
	// #endregion
</script>

<NotificationsContainer />

{#if $ui.modal}
	<Modal>
		<ModalHeader slot="header">{$ui.modal.header}</ModalHeader>
		<svelte:component this={$ui.modal.body} {...$ui.modal.props} slot="body" />
	</Modal>
{/if}

{#if showCookieAlert}
	<CookieAlert bind:isOpen={showCookieAlert} />
{/if}
{#if isOpenActionSheet}
	<ActionSheet bind:isOpen={isOpenActionSheet}>
		{#each $navigationItems as item (item.label + item.icon)}
			<ActionSheetItem
				icon={item.icon}
				href={item.href}
				to={item.to}
				badge={item.badge}
				iconColor={item.iconColor}
			>
				{item.label}
			</ActionSheetItem>
		{/each}
	</ActionSheet>
{/if}
{#if isEnableScrollUp}
	<IconButton
		name="chevron-up"
		class="fixed right-4 bottom-4 z-50"
		disableRipple
		on:click={() => main.scrollTo({ top: 0, behavior: 'smooth' })}
	/>
{:else}
	<IconButton
		name="menu"
		class="fixed right-4 bottom-4 xl:hidden z-50"
		disableRipple
		on:click={() => (isOpenActionSheet = true)}
	/>
{/if}
<div class="flex max-h-screen">
	<Sidebar />
	<main
		bind:this={main}
		on:scroll={handleScroll}
		class="p-4 lg:p-8 overflow-y-auto flex-1 relative"
	>
		<slot />
	</main>
</div>
