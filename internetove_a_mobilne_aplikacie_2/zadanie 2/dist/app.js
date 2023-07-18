"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
var express_1 = __importDefault(require("express"));
var mongoose_1 = __importDefault(require("mongoose"));
var movie_controller_1 = __importDefault(require("./movie/movie.controller"));
var body_parser_1 = __importDefault(require("body-parser"));
var App = /** @class */ (function () {
    function App() {
        this.app = express_1.default();
        this.initializeMiddlewares();
        this.initializeControlers();
        this.connectToTheDatabase();
    }
    App.prototype.listen = function () {
        var _this = this;
        this.app.set("port", process.env.PORT || 3000);
        this.app.listen(this.app.get('port'), function () {
            console.log('App is running on http://localhost:%d', _this.app.get('port'));
        });
    };
    App.prototype.getServer = function () {
        return this.app;
    };
    App.prototype.initializeMiddlewares = function () {
        this.app.use(body_parser_1.default.json());
    };
    App.prototype.connectToTheDatabase = function () {
        var uri = 'mongodb+srv://user:hesoyam89@cluster0.2peue.mongodb.net/<movie_database>?retryWrites=true&w=majority';
        mongoose_1.default.set('useUnifiedTopology', true);
        mongoose_1.default.connect(uri, { useNewUrlParser: true, useFindAndModify: false }).then(function () {
            console.log('Succesfully Connected!');
        }).catch(function (err) {
            console.log('connection: error');
            throw err;
        });
    };
    App.prototype.initializeControlers = function () {
        this.app.use('/', new movie_controller_1.default().router);
    };
    return App;
}());
exports.default = App;
