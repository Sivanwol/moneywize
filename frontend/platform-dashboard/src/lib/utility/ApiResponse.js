export class ApiResponse {
	constructor(response) {
		this.statusCode = response.status;
		this.errors = response.data.Errors;
		this.status = response.data.Status;
		this.data = response.data.Data;
	}

	/** @returns {boolean} */
	hasErrors() {
		return !this.status && this.errors && this.errors.length > 0;
	}

	/** @returns {boolean} */
	isValidRequest() {
		return this.statusCode === 200 && this.status;
	}
}