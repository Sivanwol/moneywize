import { apiClientUnauthorized } from '@utility/apiCaller';
import { ApiResponse } from '@utility/ApiResponse';

export const requestLogin = async (email, passowrd) => {

	try {
		const response = await apiClientUnauthorized.post('/Auth/login/web', {
			Email: email,
			Password: passowrd,
			RememberMe: true,
			Provider: 'Email'
		});
		return new ApiResponse(response);
	} catch (error) {
		console.log(error);
		return new ApiResponse(error.response);
	}
};
export const requestLoginConfirm = async (userToken, otpToken, code) => {
	const response = await apiClientUnauthorized.post('/Auth/login/confirm', {
		userToken,
		otpToken,
		code
	});
	return new ApiResponse(response);
};