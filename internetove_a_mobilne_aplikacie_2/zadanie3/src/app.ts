import express from 'express';
import mongoose from 'mongoose';
import bodyParser from 'body-parser';
import { RegisterRoutes} from "./movie/routes";
import * as swaggerUi from 'swagger-ui-express';
import {MovieController} from './movie/movie.controller';

 export default class App {
    public app: any;
    constructor() {
        this.app = express();
        this.initializeMiddlewares();
        this.initializeControlers();
        this.connectToTheDatabase();
         this.startSwagger();       
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
        //this.app.use('/',new BookController().router);
        RegisterRoutes(this.app);
    }
    
    private startSwagger () {
        try {
            const swaggerDocument = require('../swagger.json');
            this.app.use('/docs', swaggerUi.serve, swaggerUi.setup(swaggerDocument));
        } catch (err) {
            console.log('Unable to load swagger.json', err);
        }
    }
}

