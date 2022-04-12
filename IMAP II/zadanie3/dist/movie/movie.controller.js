"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (Object.hasOwnProperty.call(mod, k)) result[k] = mod[k];
    result["default"] = mod;
    return result;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const express = __importStar(require("express"));
const movie_model_1 = __importDefault(require("./movie.model"));
const mongoose_1 = __importDefault(require("mongoose"));
const tsoa_1 = require("tsoa");
class MovieController extends tsoa_1.Controller {
    constructor() {
        super();
        this.path = '/movies';
        this.router = express.Router();
        this.movie = movie_model_1.default;
    }
    getAllBooks(limit, offset) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                let books = yield this.movie.find()
                    .limit(limit)
                    .skip(offset);
                //books = books.map((item) => { return {id: item._id, title: item.title, author: item.author}});
                if (!books) {
                    throw new Error('Movie unavailable.');
                }
                return books;
            }
            catch (err) {
                this.setStatus(500);
                return [];
                console.error(err);
            }
        });
    }
    getBookById(id, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                //const id = request.params.id;
                let findedBook = yield this.movie.findById(mongoose_1.default.Types.ObjectId(id));
                if (!findedBook) {
                    this.setStatus(404);
                    return {};
                    throw new Error(`Book with id ${id} not found!`);
                }
                return findedBook;
            }
            catch (err) {
                this.setStatus(500);
                return {};
                console.error(err);
            }
        });
    }
    createBook(requestBody, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const bookData = request.body;
                const createdBook = new this.movie(bookData);
                const savedBook = yield createdBook.save();
                if (!savedBook) {
                    throw new Error(`Book not saved!`);
                }
                this.setStatus(201);
                return savedBook;
            }
            catch (err) {
                this.setStatus(500);
                console.error(err);
                return {};
            }
        });
    }
    modifyBook(id, requestBody, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const id = request.params.id;
                const bookData = request.body;
                const book = yield this.movie.findByIdAndUpdate(mongoose_1.default.Types.ObjectId(id), bookData, { new: true });
                if (!book) {
                    this.setStatus(404);
                    return {};
                    throw new Error(`Book with id ${id} not found!`);
                }
                return book;
            }
            catch (err) {
                this.setStatus(500);
                return {};
                console.error(err);
            }
        });
    }
    deleteBook(id, requestBody, request) {
        return __awaiter(this, void 0, void 0, function* () {
            try {
                const id = request.params.id;
                const bookData = request.body;
                const book = yield this.movie.findByIdAndDelete(mongoose_1.default.Types.ObjectId(id));
                if (!book) {
                    this.setStatus(404);
                    throw new Error(`Book with id ${id} not found!`);
                    return {};
                }
                return book;
            }
            catch (err) {
                this.setStatus(500);
                console.error(err);
                return {};
            }
        });
    }
}
exports.MovieController = MovieController;
