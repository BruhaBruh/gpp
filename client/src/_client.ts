import { cacheExchange, dedupExchange, fetchExchange, ssrExchange } from '@urql/core';
import { createClient } from '@urql/svelte';
const isServerSide = typeof window === 'undefined';

// The `ssrExchange` must be initialized with `isClient` and `initialState`
const ssr = ssrExchange({
	isClient: !isServerSide,
	initialState: !isServerSide ? (window as any).__URQL_DATA__ : undefined
});

export default createClient({
	url: 'http://localhost/graphql',
	preferGetMethod: false,
	exchanges: [
		dedupExchange,
		cacheExchange,
		ssr, // Add `ssr` in front of the `fetchExchange`
		fetchExchange
	]
});
