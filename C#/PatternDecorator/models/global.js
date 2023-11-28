const { DataTypes, Model } = require("sequelize");
const { Log } = require("../utils/helper");

class GlobalModel extends Model {
	TEST_DATA = "test";

	get sendable() {
		const data = {};
		for (const value of this.SENDABLE_DATA) {
			const key = typeof value === "object" ? value.key : value;
			const destKey = typeof value === "object" ? value.as : value;
			if (!this.dataValues[key]) continue;
			if (typeof this.dataValues[key][0] === "object") {
				data[destKey] = this.dataValues[key].map((item) => item.sendable);
			} else data[destKey] = this.dataValues[key].sendable ? this.dataValues[key].sendable : this.dataValues[key];
		}
		return data;
	}
}

module.exports = GlobalModel;