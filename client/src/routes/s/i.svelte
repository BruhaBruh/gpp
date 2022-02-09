<script lang="ts" context="module">
	export const prerender = true;
</script>

<script lang="ts">
	import Button from '../../components_ui/Button.svelte';
	import IconButton from '../../components_ui/IconButton.svelte';
	import Paper from '../../components_ui/Paper.svelte';
	import Typography from '../../components_ui/Typography.svelte';
	import { ui } from '../../stores/ui';
</script>

<svelte:head>
	<title>Настройки - Интерфейс</title>
</svelte:head>

<Paper class="flex flex-col space-y-4 max-w-2xl mx-auto">
	<Typography variant="body2" class="text-gray-400">Оповещения</Typography>
	<div class="space-y-1">
		<Typography variant="body1">Горизонтальное положение</Typography>
		<div class="grid grid-cols-1 sm:grid-cols-3 gap-2">
			<Button
				size="small"
				variant={$ui.notificationPosition.horizontal === 'left' ? 'standart' : 'text'}
				color={$ui.notificationPosition.horizontal === 'left' ? 'primary' : 'secondary'}
				on:click={() => ui.setNotificationPosition('left', $ui.notificationPosition.vertical)}
			>
				Слева
			</Button>
			<Button
				size="small"
				variant={$ui.notificationPosition.horizontal === 'center' ? 'standart' : 'text'}
				color={$ui.notificationPosition.horizontal === 'center' ? 'primary' : 'secondary'}
				on:click={() => ui.setNotificationPosition('center', $ui.notificationPosition.vertical)}
			>
				Центр
			</Button>
			<Button
				size="small"
				variant={$ui.notificationPosition.horizontal === 'right' ? 'standart' : 'text'}
				color={$ui.notificationPosition.horizontal === 'right' ? 'primary' : 'secondary'}
				on:click={() => ui.setNotificationPosition('right', $ui.notificationPosition.vertical)}
			>
				Справа
			</Button>
		</div>
	</div>
	<div class="space-y-1">
		<Typography variant="body1">Вертикальное положение</Typography>
		<div class="grid grid-cols-2 gap-1">
			<Button
				size="small"
				variant={$ui.notificationPosition.vertical === 'top' ? 'standart' : 'text'}
				color={$ui.notificationPosition.vertical === 'top' ? 'primary' : 'secondary'}
				on:click={() => ui.setNotificationPosition($ui.notificationPosition.horizontal, 'top')}
			>
				Сверху
			</Button>
			<Button
				size="small"
				variant={$ui.notificationPosition.vertical === 'bottom' ? 'standart' : 'text'}
				color={$ui.notificationPosition.vertical === 'bottom' ? 'primary' : 'secondary'}
				on:click={() => ui.setNotificationPosition($ui.notificationPosition.horizontal, 'bottom')}
			>
				Снизу
			</Button>
		</div>
	</div>
	<div class="flex justify-between space-x-2">
		<IconButton
			color="info"
			name="circle-info"
			on:click={() => ui.addNotification('Тестовое', 'info')}
		/>
		<IconButton
			color="success"
			name="circle-check"
			on:click={() => ui.addNotification('Тестовое', 'success')}
		/>
		<IconButton
			color="warning"
			name="circle-warning"
			on:click={() => ui.addNotification('Тестовое', 'warning')}
		/>
		<IconButton
			color="danger"
			name="circle-cancel"
			on:click={() => ui.addNotification('Тестовое', 'danger')}
		/>
	</div>
</Paper>
