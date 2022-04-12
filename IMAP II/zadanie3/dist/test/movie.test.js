"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const supertest_1 = __importDefault(require("supertest"));
const app_1 = __importDefault(require("../app"));
const testMovie = {
    "title": "Forrest Gumb",
    "category": "Drama, Romance",
    "release_year": 1994,
    "lenght_in_minutes": 144,
    "rating": "A",
    "Premiere_date": "1994-08-31",
    "language": "English",
    "directors": [{ "firstname": "Robert",
            "lastname": "Zemeckis",
            "bio": "blablablabal",
            "date_of_birth": "1951-05-14" }],
    "actors": [{ "firstname": "Tom",
            "lastname": "Hanks",
            "bio": "blablablabal",
            "date_of_birth": "1956-07-09" },
        { "firstname": "Sally  ",
            "lastname": "Fieldt",
            "bio": "blablablabal",
            "date_of_birth": "1946-11-06" }],
    "prizes": [{ "name": "oscar",
            "award_date": "1985-02-20",
            "awardet_for": "best actress" },
        { "name": "Golden Globes",
            "award_date": "1985-02-20",
            "awardet_for": "best performance by an Actress" }]
};
describe('Test the root path', () => {
    test('It should response the GET method', () => {
        {
            const app = new app_1.default();
            return supertest_1.default(app.getServer()).get("/movies").then(response => {
                expect(response.status).toBe(200);
            });
        }
    });
    test('responds with JSON array', () => {
        {
            const app = new app_1.default();
            return supertest_1.default(app.getServer()).get('/movies')
                .then(response => {
                expect(response.status).toBe(200);
                expect(response.type).toEqual("application/json");
                //expect(res.body).toHaveLength(4);
                expect(response.body).toMatchSnapshot;
            });
        }
        ;
    });
    it('responds with JSON object', () => {
        {
            const app = new app_1.default();
            return supertest_1.default(app.getServer()).get('/movies/5fcd0a9f721d18579c879855')
                .then(res => {
                expect(res.status).toBe(200);
                expect(res.type).toEqual("application/json");
                //expect(res.body).toHaveLength(4);
                expect(res.body.title).toEqual('Joker');
                console.log(res.body);
            });
        }
        ;
    });
    it('posts new book', () => {
        {
            const app = new app_1.default();
            return supertest_1.default(app.getServer()).post('/movies').send(testMovie)
                .then(res => {
                expect(res.status).toBe(200);
                expect(res.type).toEqual("application/json");
                console.log(res.body);
            });
        }
        ;
    });
});
