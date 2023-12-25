import { apiClientUnauthorized } from '@utils/apiCaller';
import { ApiResponse } from '@utils/ApiResponse';

export const requestLogin = async (email, passowrd) => {
	const response = await apiClientUnauthorized.post('/Auth/login', {
		email,
		passowrd
	});
	return new ApiResponse(response);
};
export const requestLoginConfirm = async (userToken, otpToken, code) => {
	const response = await apiClientUnauthorized.post('/Auth/login/confirm', {
		userToken,
		otpToken,
		code
	});
	return new ApiResponse(response);
};