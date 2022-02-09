import { writable } from 'svelte/store';
import { UserRoleEnum } from '../generated/graphql';
import differenceBetweenDates from '../utils/differenceBetweenDates';
import isoTimeToPhrase from '../utils/isoTimeToPhrase';

export interface IProfile {
	isLoggedIn: boolean;
	nickname: string;
	userRole: UserRoleEnum;
	settings: number;
	userId: number;
	permissions: number;
	avatar: string;
	banReportEndAt: string | null;
	subscriptionEndAt: string | null;
}

export const isHaveSubscription = (userPermissions: number | undefined) => {
	return isLite(userPermissions) || isPremium(userPermissions);
};

export const isPremium = (userPermissions: number | undefined) => {
	return checkPermissionsWA(Permissions.Premium, userPermissions);
};

export const isLite = (userPermissions: number | undefined) => {
	return checkPermissionsWA(Permissions.Lite, userPermissions);
};

export const getUserRoleString = (userRole: UserRoleEnum): string => {
	switch (userRole) {
		case UserRoleEnum.None:
			return 'Игрок';
		case UserRoleEnum.JrModerator:
			return 'Мл. Модератор';
		case UserRoleEnum.Moderator:
			return 'Модератор';
		case UserRoleEnum.Admin:
			return 'Администратор';
		case UserRoleEnum.SiteDeveloper:
			return 'Разработчик сайта';
		default:
			return '';
	}
};

export const getSex = (sex: number) => {
	switch (sex) {
		case 1:
			return 'Мужчина';
		case 2:
			return 'Женщина';
		default:
			return 'Не указан';
	}
};

export const checkPermissions = (allow: number, userPermissions: number | undefined): boolean => {
	if (!userPermissions) return false;
	return (userPermissions & (allow % 2 === 0 ? allow + 1 : allow)) !== 0;
};

export const checkPermissionsWA = (allow: number, userPermissions: number | undefined): boolean => {
	if (!userPermissions) return false;
	return (userPermissions & allow) !== 0;
};

export const checkSettings = (allow: number, userSettings: number | undefined): boolean => {
	if (!userSettings) return false;
	return (userSettings & allow) !== 0;
};

export const getLastOnline = (lo: string): string => {
	const dif = differenceBetweenDates(new Date(lo), new Date());
	if (
		dif.years === 0 &&
		dif.months === 0 &&
		dif.days === 0 &&
		dif.hours === 0 &&
		dif.minutes < 5 &&
		dif.minutes >= 0
	)
		return 'Онлайн';
	return isoTimeToPhrase(lo);
};

export enum Permissions {
	All = 1,
	GlobalChatRemove = 2,
	ModifyPermissions = 4,
	SetBan = 8,
	RemoveBan = 16,
	ShowReports = 32,
	ModifyRoles = 64,
	Lite = 128,
	Premium = 256,
	ModifyForum = 512,
	ModifyThread = 1024,
	ModifySocialPoints = 2048
}

export enum Settings {
	ShowPhone = 1,
	NotifyOnReport = 2,
	NotifyOnReportMessage = 4,
	NotifyOnNewSubscriber = 8,
	NotifyOnNewFriend = 16
}

function createProfile() {
	const { subscribe, set, update } = writable<IProfile>({
		isLoggedIn: false,
		nickname: '',
		userId: 0,
		userRole: UserRoleEnum.None,
		permissions: 0,
		settings: 0,
		avatar: '',
		subscriptionEndAt: null,
		banReportEndAt: null
	});

	return {
		subscribe,
		setIsLoggedIn: (isLoggedIn: boolean) => update((v) => ({ ...v, isLoggedIn })),
		setNickname: (nickname: string) => update((v) => ({ ...v, nickname })),
		setUserId: (userId: number) => update((v) => ({ ...v, userId })),
		setUserRole: (userRole: UserRoleEnum) => update((v) => ({ ...v, userRole })),
		setPermissions: (permissions: number) => update((v) => ({ ...v, permissions })),
		setSettings: (settings: number) => update((v) => ({ ...v, settings })),
		setAvatar: (avatar: string) => update((v) => ({ ...v, avatar })),
		setSubscriptionEndAt: (subscriptionEndAt: string | null) =>
			update((v) => ({ ...v, subscriptionEndAt })),
		setBanReportEndAt: (banReportEndAt: string | null) => update((v) => ({ ...v, banReportEndAt })),
		reset: () =>
			set({
				isLoggedIn: false,
				nickname: '',
				userId: 0,
				userRole: UserRoleEnum.None,
				permissions: 0,
				settings: 0,
				avatar: '',
				subscriptionEndAt: null,
				banReportEndAt: null
			})
	};
}

export const profile = createProfile();
