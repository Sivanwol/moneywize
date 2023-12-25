import axios from 'axios';
import axiosRetry from 'axios-retry';
import { PUBLIC_API_TIMEOUT, PUBLIC_API_URL } from '$env/static/public';

const apiUnauthorized = axios.create({
	baseURL: PUBLIC_API_URL,
	timeout: PUBLIC_API_TIMEOUT || 5000,
	withCredentials: true,
	headers: {
		'Content-Type': 'application/json',
		'Access-Control-Allow-Origin': '*',
		'Access-Control-Allow-Credentials': true
	}
});
const apiAuthorized = axios.create({
	baseURL: PUBLIC_API_URL,
	timeout: PUBLIC_API_TIMEOUT,
	withCredentials: true,
	headers: {
		'Content-Type': 'application/json',
		'Access-Control-Allow-Origin': '*',
		'Access-Control-Allow-Credentials': true
	}
});

axiosRetry(apiUnauthorized, {
	retries: 3,
	retryDelay: axiosRetry.exponentialDelay
});

axiosRetry(apiAuthorized, {
	retries: 3,
	retryDelay: axiosRetry.exponentialDelay
});

// request interceptor for authorized api calls
apiAuthorized.interceptors.request.use(
	config => {
		// get token from local storage
		const token = localStorage.getItem('token');
		// if token exists add it to the header
		if (token) {
			config.headers.Authorization = `Bearer ${token}`;
		}
		return config;
	},
	error => {
		Promise.reject(error);
	}
);

export { apiUnauthorized, apiAuthorized };