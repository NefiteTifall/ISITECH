const fs = require("fs");
const { readDir, path, Log } = require("./utils/helper");
const sequelize = require("./sequelize");


(async () => {
	try {
		await sequelize.authenticate();
		await sequelize.initAllModels();
		await sequelize.sync();
		const {
			models: {
				users: Users,
			}
		} = require("./sequelize");
		Log.success("Connection has been established successfully.");
		const [user] = await Users.findOrCreate({
			where: {
				email: "nefitetifall@gmail.com"
			},
			defaults: {
				displayname: "Nicolas G.",
			}
		});
		Log.info("User", user.getDataValue("displayname"));
	} catch (error) {
		Log.error("Unable to connect to the database:", error);
	}
})();