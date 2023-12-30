export class Language {
	constructor({ name, code, flag, id }) {

		this.id = id;
		this.name = name;
		this.code = code;
		this.flag = flag;
	}

	static fromJson(json) {
		return new Language({
			name: json.name,
			code: json.code,
			flag: json.flag,
			id: json.id
		});
	}
}