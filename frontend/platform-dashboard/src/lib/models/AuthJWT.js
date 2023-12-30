import moment from 'moment';

export class AuthJWT {
	constructor({ userId, accessToken, refreshToken, expireAt }) {
		this.userId = userId;
		this.accessToken = accessToken;
		this.refreshToken = refreshToken;
		this.expireAt = moment(expireAt);
	}

	static fromJson(json) {
		return new AuthJWT({
			userId: json.userId,
			accessToken: json.accessToken,
			refreshToken: json.refreshToken,
			expireAt: json.expireAt
		});
	}
}