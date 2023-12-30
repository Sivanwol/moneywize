export class UserNotification {
	constructor({ message, byUsername, icon, cssType, duration, id }) {
		this.message = message;
		this.duration = duration;
		this.cssType = cssType;
		this.icon = icon;

		this.byUsername = byUsername;
		this.id = id;
	}

	static fromJson(json) {
		return new UserNotification({
			message: json.message,
			byUsername: json.byUsername,
			duration: json.duration,
			cssType: json.cssType,
			icon: json.icon,
			id: json.id
		});
	}
}