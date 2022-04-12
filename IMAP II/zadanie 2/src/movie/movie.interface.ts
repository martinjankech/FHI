
enum rating {
  A = "A",
  B = "B",
  C = "C",
  D = "D",
  E=  "E"
}

interface Movie {
    title: string
    category:string
    release_year:number
    lenght_in_minutes:number
    rating:rating
    Premiere_date:Date
    language:string
    director?: {
        firstname: string,
        lastname: string,
        bio: string,  
        date_of_birth: Date 
    } 
    actor?: {
        firstname: string,
        lastname: string,
        bio: string,  
        date_of_birth: Date 
    } 
    prize?:{
name:string,
award_date:Date,
awardet_for:string
}
};
export default Movie;