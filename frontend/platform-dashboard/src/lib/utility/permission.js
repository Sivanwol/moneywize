export function allowAccess(roles = [], userRoles = []) {
	if (roles.length === 0) {
		return true;
	}
	return userRoles.some((role) => roles.includes(role));
}