import { ApolloClient, InMemoryCache } from '@apollo/client/core';
import { split } from '@apollo/client/link/core';
import { HttpLink } from '@apollo/client/link/http';
import { setClient } from 'svelte-apollo';
import { PUBLIC_GQL_API_URL, PUBLIC_GQL_ENABLED_CACHE } from '$env/static/public';
import { getMainDefinition } from '@apollo/client/utilities';

export function createApolloClient(access_token) {

	const headers = {
		Authorization: `Bearer ${access_token}`,
	};
	const httpLink = new HttpLink({
		uri: PUBLIC_GQL_API_URL,
		headers,
	});

	// const wsLink = new WebSocketLink({
	// 	uri: PUBLIC_GQL_WS_URL,
	// 	options: {
	// 		reconnect: true,
	// 		connectionParams: {
	// 			headers,
	// 		},
	// 	},
	// });

	const link = split(
		({ query }) => {
			const definition = getMainDefinition(query);
			return (
				definition.kind === "OperationDefinition" &&
				definition.operation === "subscription"
			);
		},
		//wsLink,
		httpLink
	);
	const cache = (PUBLIC_GQL_ENABLED_CACHE)?new InMemoryCache().restore({}): "no-cache";
	const client = new ApolloClient({
		link: httpLink,
		cache,
	});
	setClient(client);
}

 