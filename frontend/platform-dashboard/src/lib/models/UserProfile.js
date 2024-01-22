export class UserProfile {
	constructor({ id, firstName, lastName, email, phoneNumber, roles, registerCompletedAt }) {
		this.id = id;
		this.firstName = firstName;
		this.lastName = lastName;
		this.email = email;
		this.phoneNumber = phoneNumber;
		this.roles = roles;
		this.registerCompletedAt = registerCompletedAt;
	}

	static fromJson(json) {
		return new UserProfile({
			id: json.id,
			firstName: json.firstName,
			lastName: json.lastName,
			email: json.email,
			phoneNumber: json.phoneNumber,
			roles: json.roles,
			registerCompletedAt: json.registerCompletedAt
		});
	}
}