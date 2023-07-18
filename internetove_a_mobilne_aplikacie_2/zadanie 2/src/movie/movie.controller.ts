import * as express from 'express';
import MovieModel from './movie.model';
import mongoose from 'mongoose';
import Movie from './movie.interface';

interface Controller {
    path: string;
    router: express.Router;
  }

export default class MovieController implements Controller {
    public path = '/movies';
    public router = express.Router();
    private movie = MovieModel;

    constructor() {
        this.initializeRoutes();
    }

    private initializeRoutes() {
        this.router.get(this.path,this.getAllMovies);
        this.router.get(`${this.path}/:id`,this.getMovieById)
        this.router.post(this.path,this.createMovie);
        this.router.put(`${this.path}/:id`,this.modifyMovie);
        this.router.delete(`${this.path}/:id`,this.deleteMovie);
    }

    private getAllMovies = async (request: express.Request, response: express.Response) => {
            const movies = await this.movie.find();
            response.status(200).send(movies);
    }

    private getMovieById = async (request: express.Request, response: express.Response) => {
        try {
            const id = request.params.id;
            const movies = await this.movie.findById(mongoose.Types.ObjectId(id));
            if (movies) {
            response.send(movies);
            } else {
            response.status(404).send(`Movie with id ${id} not found!`);
            }
        }
        catch (err) {
            response.status(500).send(err);
        }
      }

    private createMovie = async (request: express.Request, response: express.Response) => {
        try {
            const movieData = request.body;
            const createdMovie = new this.movie(movieData);
            // const savedBook = createdBook.save((err: any) => {
            //     if (err) {
            //         response.send(err)
            //     } else {
            //         response.send(savedBook)
            //     }
            //   })
        
            const savedMovie = await createdMovie.save();
            response.send(savedMovie);
        }
        catch (err) {
            response.status(500).send(err);
        }
       
    }

    
    private modifyMovie = async (request: express.Request, response: express.Response) => {
        try {
            const id = request.params.id;
            const movieData: Movie = request.body;
            const movie = await this.movie.findByIdAndUpdate(mongoose.Types.ObjectId(id), movieData, { new: true });
            if (movie) {
            response.send(movie);
            } else {
            response.status(404).send(`Movie with id ${id} not found!`);
            }
        }
        catch (err) {
            response.status(500).send(err);
        }
      }

      private deleteMovie = async (request: express.Request, response: express.Response) => {
        try {
            const id = request.params.id;
            const success = await this.movie.findByIdAndDelete(mongoose.Types.ObjectId(id));
            if (success) {
            response.sendStatus(200);
            } else {
            response.status(404).send(`Movie with id ${id} not found!`);
            }
        }
        catch (err) {
            response.status(500).send(err);
        }
      }
    
}
