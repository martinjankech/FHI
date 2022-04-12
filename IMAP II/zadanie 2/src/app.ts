import express from 'express';
import mongoose from 'mongoose';
import MovieController from './movie/movie.controller';
import bodyParser from 'body-parser';

 export default class App {
    public app:express.Application;
    constructor() {
        this.app = express();
        this.initializeMiddlewares();
        this.initializeControlers();
        this.connectToTheDatabase();
       
    }

    public listen() {
        this.app.set("port", process.env.PORT || 3000);
        this.app.listen(this.app.get('port'), () => {
            console.log('App is running on http://localhost:%d', this.app.get('port'));
        });
    }

    public getServer() {
        return this.app;
      }

    private initializeMiddlewares() {
        this.app.use(bodyParser.json());
      }

    private connectToTheDatabase() {
        const uri: string = 'mongodb+srv://user:hesoyam89@cluster0.2peue.mongodb.net/<movie_database>?retryWrites=true&w=majority';
        
        mongoose.set('useUnifiedTopology', true);
        mongoose.connect(uri, { useNewUrlParser: true, useFindAndModify: false }).then(() => {
            console.log('Succesfully Connected!');
        }).catch(err => {
            console.log('connection: error');
            throw err;
        })
    }

    private initializeControlers() {
        this.app.use('/',new MovieController().router);
    }
}
