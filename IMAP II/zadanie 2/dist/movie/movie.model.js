"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
var mongoose_1 = __importDefault(require("mongoose"));
var PrizeSchema = new mongoose_1.default.Schema({
    name: String,
    award_date: Date,
    awardet_for: String
});
var DirectorSchema = new mongoose_1.default.Schema({
    firstname: String,
    lastname: String,
    bio: String,
    date_of_birth: { type: Date, default: Date.now }
});
var ActorSchema = new mongoose_1.default.Schema({
    firstname: String,
    lastname: String,
    bio: String,
    date_of_birth: { type: Date, default: Date.now }
});
var MovieSchema = new mongoose_1.default.Schema({
    title: { type: String, required: true },
    category: String,
    release_year: Number,
    lenght_in_minutes: Number,
    rating: String,
    Premiere_date: { type: Date, format: '%YZ-%mm-%dd', default: Date.now },
    language: String,
    directors: [DirectorSchema],
    actors: [ActorSchema],
    prizes: [PrizeSchema]
});
var MovieModel = mongoose_1.default.model('Movie', MovieSchema);
exports.default = MovieModel;
