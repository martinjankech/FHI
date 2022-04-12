"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
var express = __importStar(require("express"));
var movie_model_1 = __importDefault(require("./movie.model"));
var mongoose_1 = __importDefault(require("mongoose"));
var MovieController = /** @class */ (function () {
    function MovieController() {
        var _this = this;
        this.path = '/movies';
        this.router = express.Router();
        this.movie = movie_model_1.default;
        this.getAllMovies = function (request, response) { return __awaiter(_this, void 0, void 0, function () {
            var movies;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.movie.find()];
                    case 1:
                        movies = _a.sent();
                        response.send(movies);
                        return [2 /*return*/];
                }
            });
        }); };
        this.getMovieById = function (request, response) { return __awaiter(_this, void 0, void 0, function () {
            var id, movies, err_1;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        id = request.params.id;
                        return [4 /*yield*/, this.movie.findById(mongoose_1.default.Types.ObjectId(id))];
                    case 1:
                        movies = _a.sent();
                        if (movies) {
                            response.send(movies);
                        }
                        else {
                            response.status(404).send("Movie with id " + id + " not found!");
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        err_1 = _a.sent();
                        response.status(500).send(err_1);
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        }); };
        this.createMovie = function (request, response) { return __awaiter(_this, void 0, void 0, function () {
            var movieData, createdMovie, savedMovie, err_2;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        movieData = request.body;
                        createdMovie = new this.movie(movieData);
                        return [4 /*yield*/, createdMovie.save()];
                    case 1:
                        savedMovie = _a.sent();
                        response.send(savedMovie);
                        return [3 /*break*/, 3];
                    case 2:
                        err_2 = _a.sent();
                        response.status(500).send(err_2);
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        }); };
        this.modifyMovie = function (request, response) { return __awaiter(_this, void 0, void 0, function () {
            var id, movieData, movie, err_3;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        id = request.params.id;
                        movieData = request.body;
                        return [4 /*yield*/, this.movie.findByIdAndUpdate(mongoose_1.default.Types.ObjectId(id), movieData, { new: true })];
                    case 1:
                        movie = _a.sent();
                        if (movie) {
                            response.send(movie);
                        }
                        else {
                            response.status(404).send("Book with id " + id + " not found!");
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        err_3 = _a.sent();
                        response.status(500).send(err_3);
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        }); };
        this.deleteMovie = function (request, response) { return __awaiter(_this, void 0, void 0, function () {
            var id, success, err_4;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        _a.trys.push([0, 2, , 3]);
                        id = request.params.id;
                        return [4 /*yield*/, this.movie.findByIdAndDelete(mongoose_1.default.Types.ObjectId(id))];
                    case 1:
                        success = _a.sent();
                        if (success) {
                            response.sendStatus(200);
                        }
                        else {
                            response.status(404).send("Movie with id " + id + " not found!");
                        }
                        return [3 /*break*/, 3];
                    case 2:
                        err_4 = _a.sent();
                        response.status(500).send(err_4);
                        return [3 /*break*/, 3];
                    case 3: return [2 /*return*/];
                }
            });
        }); };
        this.initializeRoutes();
    }
    MovieController.prototype.initializeRoutes = function () {
        this.router.get(this.path, this.getAllMovies);
        this.router.get(this.path + "/:id", this.getMovieById);
        this.router.post(this.path, this.createMovie);
        this.router.put(this.path + "/:id", this.modifyMovie);
        this.router.delete(this.path + "/:id", this.deleteMovie);
    };
    return MovieController;
}());
exports.default = MovieController;
