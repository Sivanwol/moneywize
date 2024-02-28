const UserActivity = 'USERACTIVITY';
const Activity = 'ACTIVITY';
const Clients = 'CLIENTS';
const ClientContracts = 'CLIENTCONTRACTS';
const Users = 'USERS';

/** @param {string} value */
const isMatchingEntityType = (value) => value === UserActivity || value === Activity || value === Clients || value === ClientContracts || value === Users;

export {
	UserActivity,
	Activity,
	Clients,
	ClientContracts,
	Users,
	isMatchingEntityType
};