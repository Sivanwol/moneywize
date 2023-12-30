export class ApiResponse {
	constructor(response) {
		this.statusCode = response.status;
		this.errors = response.data.errors;
		this.status = response.data.status;
		this.data = response.data.data;
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