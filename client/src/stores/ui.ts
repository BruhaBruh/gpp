import { browser } from '$app/env';
import { writable } from 'svelte/store';

export type NotificationType = 'success' | 'warning' | 'info' | 'danger' | 'avatar';

export interface INotification {
	id: number;
	header: string;
	type: NotificationType;
	avatar?: string;
	body?: string;
	timeout: number;
}

export type HorizontalPosition = 'left' | 'center' | 'right';
export type VerticalPosition = 'top' | 'bottom';

export type NotificationPosition = {
	horizontal: HorizontalPosition;
	vertical: VerticalPosition;
};

export interface IModal {
	header: string;
	body?: any;
	props?: object;
}

type Modal = IModal | null;

interface IUI {
	notificationDefaultTimeout: number;
	notificationPosition: NotificationPosition;
	notifications: INotification[];
	modal: IModal | null;
}

const defaultState: IUI = {
	notificationDefaultTimeout: 3000,
	notificationPosition: {
		horizontal: 'center',
		vertical: 'bottom'
	},
	notifications: [],
	modal: null
};

const getNotificationsWithNew = (
	notifications: INotification[],
	newNotification: INotification
): INotification[] => {
	const lastNotification = {
		...notifications[notifications.length - 1],
		id: 0
	};
	const mNew = {
		...newNotification,
		id: 0
	};
	if (JSON.stringify(lastNotification) === JSON.stringify(mNew))
		return [
			...notifications.filter((v) => v.id !== notifications[notifications.length - 1].id),
			newNotification
		];
	return [...notifications, newNotification];
};

const loadHorizontalNotificationPosition = (): any => {
	const pos = window.localStorage.getItem('horizontal');
	if (!['left', 'center', 'right'].includes(pos))
		return defaultState.notificationPosition.horizontal;
	return pos;
};

const loadVerticalNotificationPosition = (): any => {
	const pos = window.localStorage.getItem('vertical');
	if (!['top', 'bottom'].includes(pos)) return defaultState.notificationPosition.vertical;
	return pos;
};

function createUI() {
	const { subscribe, set, update } = writable<IUI>(defaultState);

	if (browser) {
		if (!!window.localStorage.getItem('site-version')) window.localStorage.clear();
		set({
			...defaultState,
			notificationPosition: {
				horizontal: loadHorizontalNotificationPosition(),
				vertical: loadVerticalNotificationPosition()
			}
		});
		subscribe((v) => {
			window.localStorage.setItem('horizontal', v.notificationPosition.horizontal);
			window.localStorage.setItem('vertical', v.notificationPosition.vertical);
		});
	}
	return {
		subscribe,
		setModal: (modal: Modal) => update((v) => ({ ...v, modal: modal })),
		setNotificationPosition: (horizontal: HorizontalPosition, vertical: VerticalPosition) =>
			update((v) => ({
				...v,
				notificationPosition: {
					horizontal,
					vertical
				}
			})),
		addNotification: (
			header: string,
			type: NotificationType,
			body?: string,
			avatar?: string,
			timeout?: number
		) =>
			update((v) => ({
				...v,
				notifications: getNotificationsWithNew(v.notifications, {
					id: new Date().getTime(),
					header,
					type,
					body,
					avatar,
					timeout: timeout ?? v.notificationDefaultTimeout
				})
			})),
		removeNotification: (id: number) =>
			update((v) => ({ ...v, notifications: v.notifications.filter((n) => n.id !== id) })),
		reset: () => set(defaultState)
	};
}

export const ui = createUI();
