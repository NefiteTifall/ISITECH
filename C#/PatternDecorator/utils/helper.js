const chalk = require("chalk");

const log = (type) => (...data) => {
	if (typeof data === "string") data = [data];
	console.log(chalk.gray(`${ formatDate(new Date(), "DD/MM hh:mm:ss") } •`), type, ...data);
};
const Log = {
	info: log(chalk.blue("[ ℹ ]")),
	success: log(chalk.green("[ ✔ ]")),
	warning: log(chalk.yellow("[ ⚠ ]")),
	money: log(chalk.green("[ $ ]")),
	debug: log(chalk.cyanBright("[ ⚙ ]")),
	error: log(chalk.red("[ ✖ ]")),
	orders: log(chalk.blue("[ 🛒 ]")),
	what: log(chalk.magenta("[ ⁈ ]")),
	admin: log(chalk.yellow("[ ⌂ ][ADMIN]")),
	progress: log(chalk.blue("[ ⌛ ]")),
	printer: log(chalk.blue("[ 🖨️  ]")),
	updated: log(chalk.blue("[ 🔄 ]")),
	deleted: log(chalk.blue("[ 🗑️  ]")),
	created: log(chalk.blue("[ ➕ ]")),
	verbose: log(chalk.gray("[ ⚡ ]")),
	AxiosError: (error) => {
		if (error.response) {
			// The request was made and the server responded with a status code
			// that falls out of the range of 2xx
			if (error.response.data?.response) log(chalk.red("[ ✖ ][AXIOS]"))(`Code: ${ error.response.status }`, error.response.data.response);
			else log(chalk.red("[ ✖ ][AXIOS]"))(`Code: ${ error.response.status }`, error.response.data);
		} else if (error.request) {
			// The request was made but no response was received
			// `error.request` is an instance of XMLHttpRequest in the browser and an instance of
			// http.ClientRequest in node.js
			log(chalk.red("[ ✖ ]"))(error.request);
		} else {
			// Something happened in setting up the request that triggered an Error
			log(chalk.red("[ ✖ ]"))(error.message);
		}
	}
};
exports.Log = Log;

/**
 * Format DATE
 * @param {Date} date
 * @param {String} format
 * @return {String}
 */
const formatDate = (date, format) => {
	if (!format) format = "DD/MM/YYYY hh:mm:ss";
	if (typeof date === "string") date = new Date(date);
	const map = {
		YYYY: date.getFullYear(),
		MM: date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1,
		DD: date.getDate() < 10 ? "0" + date.getDate() : date.getDate(),
		hh: date.getHours() < 10 ? "0" + date.getHours() : date.getHours(),
		mm: date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes(),
		ss: date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds(),
		SSS: date.getMilliseconds() < 10 ? "00" + date.getMilliseconds() : date.getMilliseconds() < 100 ? "0" + date.getMilliseconds() : date.getMilliseconds(),
	};
	for (const key in map) format = format.replace(key, map[key]);
	return format;
};
exports.formatDate = formatDate;

/**
 * Read directory and return all files
 * @param {string} path - Path to read
 * @param {string} [ext] - Extension to filter
 * @return {string[]} - Array of files
 */
exports.readDir = function readDir(path, ext) {
	let results = [];

	try {
		const items = fs.readdirSync(path);

		for (const item of items) {
			const fullPath = join(path, item);
			const stat = fs.statSync(fullPath);

			if (stat.isDirectory()) {
				results = results.concat(readDir(fullPath, ext)); // Récursion pour les sous-répertoires
			} else if (!ext || extname(item) === ext) {
				results.push(fullPath);
			}
		}
	} catch (error) {
		console.error(`Erreur lors de la lecture du répertoire: ${ error.message }`);
	}

	return results;
};


/**
 * Create a simple et logique path
 * @param {string} path - Asked path
 * @return {string} - Simple and logique path
 */
const { join, extname } = require("path");
const fs = require("fs");
exports.path = (...path) => {
	const base = join(__dirname, "../");
	return join(base, ...path);
};