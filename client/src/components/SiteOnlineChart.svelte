<script lang="ts">
	import { operationStore, query } from '@urql/svelte';
	import Paper from '../components_ui/Paper.svelte';
	import {
		OnlineTypes,
		SiteOnlineDocument,
		SiteOnlineQuery,
		SiteOnlineQueryVariables
	} from '../generated/graphql';

	let type: OnlineTypes = OnlineTypes.Hour;

	const siteOnlineStore = operationStore<SiteOnlineQuery>(SiteOnlineDocument, {
		type: type
	});

	const fetch = () => {
		if (!$siteOnlineStore.data && !$siteOnlineStore.error) return query(siteOnlineStore);
		($siteOnlineStore.variables as SiteOnlineQueryVariables).type = type;
		$siteOnlineStore.reexecute();
	};

	$: type && fetch();
	$: !!$siteOnlineStore.data && console.log($siteOnlineStore.data);
	$: !!$siteOnlineStore.error && console.log($siteOnlineStore.error);
</script>

<Paper>
	<svg id="site_online_svg" class="w-full" style="aspect-ratio: 4 / 2" />
</Paper>
