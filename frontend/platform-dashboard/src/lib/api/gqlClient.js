import { ApolloClient, HttpLink, InMemoryCache, split } from '@apollo/client';
import { setContext } from '@apollo/client/link/context';
import { WebSocketLink } from '@apollo/client/link/ws';
import { getMainDefinition } from '@apollo/client/utilities';
import { setClient } from 'svelte-apollo';
import { GQL_API_URL, GQL_ENABLED_CACHE, GQL_WS_URL } from '$env/static/public';

export function createApolloClient(access_token) {
	const cache = (GQL_ENABLED_CACHE) ? new InMemoryCache().restore({}) : 'no-cache';

	const authLink = setContext((_, { headers }) => {
		const token = getToken();
		return {
			headers: {
				...headers,
				'Content-Type': 'application/json;charset=utf-8',
				'Access-Control-Allow-Origin': '*',
				'Authorization': `Bearer ${access_token}`
			}
		};
	});
	const httpLink = new HttpLink({
		uri: GQL_API_URL
	});
	const wsLink = new WebSocketLink({
		uri: GQL_WS_URL,
		options: {
			reconnect: true,
			lazy: true
		}
	});
	const link = split(
		({ query }) => {
			const definition = getMainDefinition(query);
			return (
				definition.kind === 'OperationDefinition' &&
				definition.operation === 'subscription'
			);
		},
		authLink.concat(wsLink),
		authLink.concat(httpLink)
	);
	const client = new ApolloClient({
		link,
		cache
	});
	setClient(client);
	return client;
}


 