import mongoose from 'mongoose';
import Movie from './movie.interface';

const PrizeSchema=new mongoose.Schema({
name:String,
award_date:Date,
awardet_for:String
}) 
const DirectorSchema = new mongoose.Schema({
    firstname:String, 
    lastname: String,
    bio: String,
    date_of_birth:{type: Date, default: Date.now }  
});
const ActorSchema = new mongoose.Schema({
    firstname:String, 
    lastname: String,
    bio: String,
    date_of_birth:{type: Date , default: Date.now }
   
});
const MovieSchema = new mongoose.Schema({
    title: {type:String, required: true},
   category:String,
   release_year:Number,
   lenght_in_minutes:Number,
   rating:String,
   Premiere_date:{type: Date, format:'%YZ-%mm-%dd', default: Date.now },
   language:String,
    directors: [DirectorSchema],
    actors:[ActorSchema],
    prizes:[PrizeSchema]
});

const MovieModel = mongoose.model<Movie & mongoose.Document>('Movie', MovieSchema);
export default MovieModel;
