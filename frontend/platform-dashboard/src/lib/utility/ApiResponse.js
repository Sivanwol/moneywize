export class ApiResponse {
	constructor(response) {
		this.statusCode = response.status;
		this.errors = response.data.errors;
		this.status = response.data.status;
		this.data = response.data.data;
	}
}