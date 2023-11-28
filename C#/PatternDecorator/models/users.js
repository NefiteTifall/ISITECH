const { DataTypes, Model } = require("sequelize");
const GlobalModel = require("./global");
const { Log } = require("../utils/helper");

module.exports = (sequelize) => {
	class Users extends GlobalModel {
		SENDABLE_DATA = ["id", "displayname", "balance", "subscription_plan", "email", "minimal_balance", "minimal_credit"];
		EDITABLE_DATA = ["displayname", "minimal_balance", "minimal_credit", "email"];
	}

	Users.init({
		id: {
			type: DataTypes.UUID,
			primaryKey: true,
			defaultValue: DataTypes.UUIDV4,
			allowNull: false
		},
		displayname: {
			type: DataTypes.STRING,
			allowNull: false
		},
		email: {
			type: DataTypes.STRING,
			allowNull: false
		},
	}, {
		sequelize,
		modelName: "users"
	});
};