export class UserProfile {
	constructor({ id, firstName, lastName, email, phone, avatarImage, role, createdAt }) {
		this.id = id;
		this.avatarImage = avatarImage;
		this.firstName = firstName;
		this.lastName = lastName;
		this.email = email;
		this.phone = phone;
		this.role = role;
		this.createdAt = createdAt;
	}

	static fromJson(json) {
		return new UserProfile({
			id: json.id,
			avatarImage: json.avatarImage,
			firstName: json.firstName,
			lastName: json.lastName,
			email: json.email,
			phone: json.phone,
			role: json.role,
			createdAt: json.createdAt
		});
	}
}