import { apiClientAuthorized, apiClientUnauthorized } from '@utility/apiCaller';
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
	try {
		console.log(userToken, otpToken, code);
		const response = await apiClientUnauthorized.put('/Auth/login/confirm', {
			UserToken: userToken,
			OTPToken: otpToken,
			Code: code
		});
		return new ApiResponse(response);
	} catch (error) {
		console.log(error);
		return new ApiResponse(error.response);
	}
};
export const requestRefreshToken = async (refreshToken) => {
	try {
		const response = await apiClientAuthorized.post('/Auth/refresh_tokens', {
			refreshToken
		});
		return new ApiResponse(response);
	} catch (error) {
		console.log(error);
		return new ApiResponse(error.response);
	}
};