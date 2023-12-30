export class UserMessage {
	constructor({ message, byUsername, avatarImage, duration, id }) {
		this.message = message;
		this.duration = duration;
		this.byUsername = byUsername;
		this.id = id;
		this.avatarImage = avatarImage;
	}

	static fromJson(json) {
		return new UserMessage({
			message: json.message,
			avatarImage: json.avatarImage,
			byUsername: json.byUsername,
			duration: json.duration,
			id: json.id
		});
	}
}