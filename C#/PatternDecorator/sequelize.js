const { Sequelize } = require("sequelize");
const { Log, readDir, path } = require("./utils/helper");
const sequelize = new Sequelize({
	dialect: "sqlite",
	storage: "./database.sqlite",
	logging: false
});

sequelize.initAllModels = async () => {
	return new Promise((resolve, reject) => {
		const models = [];

		for (const file of readDir(path("/models"), ".js")) {
			if (file.includes("global.js")) continue;
			models.push(require(file));
		}
		Log.verbose(`${ models.length } models loaded`);
		for (const model of models) model(sequelize);
		resolve();
	});
};

module.exports = sequelize;