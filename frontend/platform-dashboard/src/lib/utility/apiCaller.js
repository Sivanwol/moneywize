import axios from 'axios';
import axiosRetry from 'axios-retry';
import { PUBLIC_API_TIMEOUT, PUBLIC_API_URL } from '$env/static/public';

/** @type {string | null} */
let accessToken = null;
const setAccessToken = (token) => {
	accessToken = token;
};
const apiClientUnauthorized = axios.create({
	baseURL: PUBLIC_API_URL,
	timeout: PUBLIC_API_TIMEOUT || 5000,
	headers: {
		'Content-Type': 'application/json;charset=utf-8',
	}
});
const apiClientAuthorized = axios.create({
	baseURL: PUBLIC_API_URL,
	timeout: PUBLIC_API_TIMEOUT,
	withCredentials: true,
	headers: {
		'Content-Type': 'application/json;charset=utf-8',
	}
});

axiosRetry(apiClientUnauthorized, {
	retries: 3,
	retryDelay: axiosRetry.exponentialDelay
});

axiosRetry(apiClientAuthorized, {
	retries: 3,
	retryDelay: axiosRetry.exponentialDelay
});

// request interceptor for authorized api calls
apiClientAuthorized.interceptors.request.use(
	config => {
		if (accessToken === null) {
			throw new Error('Access token is null');
		}
		// get token from local storage
		const token = accessToken;
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
// response interceptor for unauthorized api calls fix cors issue


export { apiClientUnauthorized, apiClientAuthorized, setAccessToken };