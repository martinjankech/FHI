"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const mongoose_1 = __importDefault(require("mongoose"));
const PrizeSchema = new mongoose_1.default.Schema({
    name: String,
    award_date: Date,
    awardet_for: String
});
const DirectorSchema = new mongoose_1.default.Schema({
    firstname: String,
    lastname: String,
    bio: String,
    date_of_birth: { type: Date, default: Date.now }
});
const ActorSchema = new mongoose_1.default.Schema({
    firstname: String,
    lastname: String,
    bio: String,
    date_of_birth: { type: Date, default: Date.now }
});
const MovieSchema = new mongoose_1.default.Schema({
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
const movieModel = mongoose_1.default.model('Movie', MovieSchema);
exports.default = movieModel;
