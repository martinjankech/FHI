"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = __importDefault(require("express"));
const mongoose_1 = __importDefault(require("mongoose"));
const body_parser_1 = __importDefault(require("body-parser"));
const routes_1 = require("./movie/routes");
const swaggerUi = __importStar(require("swagger-ui-express"));
class App {
    constructor() {
        this.app = express_1.default();
        this.initializeMiddlewares();
        this.initializeControlers();
        this.connectToTheDatabase();
        this.startSwagger();
    }
    listen() {
        this.app.set("port", process.env.PORT || 3000);
        this.app.listen(this.app.get('port'), () => {
            console.log('App is running on http://localhost:%d', this.app.get('port'));
        });
    }
    getServer() {
        return this.app;
    }
    initializeMiddlewares() {
        this.app.use(body_parser_1.default.json());
    }
    connectToTheDatabase() {
        const uri = 'mongodb+srv://user:hesoyam89@cluster0.2peue.mongodb.net/<movie_database>?retryWrites=true&w=majority';
        mongoose_1.default.set('useUnifiedTopology', true);
        mongoose_1.default.connect(uri, { useNewUrlParser: true, useFindAndModify: false }).then(() => {
            console.log('Succesfully Connected!');
        }).catch(err => {
            console.log('connection: error');
            throw err;
        });
    }
    initializeControlers() {
        //this.app.use('/',new BookController().router);
        routes_1.RegisterRoutes(this.app);
    }
    startSwagger() {
        try {
            const swaggerDocument = require('../swagger.json');
            this.app.use('/docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));
        }
        catch (err) {
            console.log('Unable to load swagger.json', err);
        }
    }
}
exports.default = App;
