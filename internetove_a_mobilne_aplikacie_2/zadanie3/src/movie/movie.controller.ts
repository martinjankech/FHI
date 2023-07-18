import * as express from 'express';
import movieModel from './movie.model';
import mongoose from 'mongoose';
import {Movie,movieResponse} from './movie.interface';
import {Controller,Route,Tags,Get,Query,Response, Request, Post, Body, Put, Delete } from "tsoa";


@Route('/movies')
@Tags('Movie')
export class MovieController extends Controller {
    public path = '/movies';
    public router = express.Router();
    private  movie = movieModel;

    constructor() {
        super();
    }
@Response<movieResponse>('200', 'Success')
@Get()  
    public async getAllMovies(@Query() limit: number,@Query() offset: number): Promise< movieResponse[]> {
        try {
            
             let movies = await this.movie.find()
                       .limit(limit)
                       .skip(offset);
            //books = books.map((item) => { return {id: item._id, title: item.title, author: item.author}});
            if (!movies) {
                throw new Error('Movie unavailable.');
            }
            return <movieResponse[]>movies;
        }
        catch(err) {
            this.setStatus(500);
            return [];
            console.error(err);
        }
            
    }

@Response<movieResponse>('200', 'Success')
    @Get('{id}')
    public async getMovieById(id: string, @Request() request: express.Request): Promise<movieResponse> {
        
        try {
            //const id = request.params.id;
            let findedMovie = await this.movie.findById(mongoose.Types.ObjectId(id));
            if (!findedMovie) {
                this.setStatus(404);
                return <movieResponse>{};
                throw new Error(`Book with id ${id} not found!`);
            }
            return <movieResponse>findedMovie;
        }
        catch (err) {
            this.setStatus(500);
            return <movieResponse>{};
            console.error(err);
        }
    }
@Response<movieResponse>('200', 'Success')
    @Post()
    public async createMovie( @Body() requestBody:Movie, @Request() request: express.Request):Promise<movieResponse> {
        try {
            const movieData = request.body;
            const createdMovie = new this.movie(movieData);
            const savedMovie = await createdMovie.save();
            if(!savedMovie) {
                throw new Error(`Movie not saved!`);
            }
            this.setStatus(201);
            return <movieResponse>savedMovie;
        }
        catch (err) {
            this.setStatus(500);
            console.error(err);
            return <movieResponse>{};
            
        }
       
    }

    @Response<movieResponse>('200', 'Success')
    @Put('{id}')
    public async modifyMovie(id: string, @Body() requestBody:Movie, @Request() request: express.Request):Promise<movieResponse> {
        try {
            const id = request.params.id;
            const movieData: Movie = request.body;
            const movie = await this.movie.findByIdAndUpdate(mongoose.Types.ObjectId(id), movieData, { new: true });
            if (!movie) {
                this.setStatus(404);
                return <movieResponse>{};
                throw new Error(`Book with id ${id} not found!`);
            }
            return <movieResponse>movie;
        }
        catch (err) {
            this.setStatus(500);
            return <movieResponse>{};
            console.error(err);
        }
      }
@Response<movieResponse>('200', 'Success')
      @Delete('{id}')
     public async deleteMovie(id: string, @Body() requestBody:Movie,  @Request() request: express.Request):Promise<movieResponse> {
        try {
            const id = request.params.id;
            const movieData: Movie = request.body;
            const movie = await this.movie.findByIdAndDelete(mongoose.Types.ObjectId(id));
            if (!movie) {
                this.setStatus(404);
                throw new Error(`Movie with id ${id} not found!`);
                return <movieResponse>{}; 
            }
            return <movieResponse>movie;
        }
        catch (err) {
            this.setStatus(500);
            console.error(err);
            return <movieResponse>{};  
        }
      }
    
}
