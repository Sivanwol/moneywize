import { apiClientUnauthorized } from '@utility/apiCaller';
import { ApiResponse } from '@utility/ApiResponse';

export const requestLogin = async (email, passowrd) => {
	const response = await apiClientUnauthorized.post('/Auth/login/web', {
		Email: email,
		Password: passowrd,
		RememberMe: true,
		Provider: 'Email'
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