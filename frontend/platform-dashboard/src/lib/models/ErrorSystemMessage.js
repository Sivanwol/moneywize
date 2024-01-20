import moment from 'moment';

export class ErrorSystemMessage {
	constructor({ message, severity, type }) {
		this.message = message;
		this.severity = severity;
		this.type = type;
		this.createdAt = moment();
	}

	static fromJson(json) {
		return new ErrorSystemMessage({
			message: json.message,
			severity: json.severity,
			type: json.type
		});
	}
}