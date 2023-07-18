
enum rating {
  A = "A",
  B = "B",
  C = "C",
  D = "D",
  E=  "E"
}

export interface Movie {
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
export interface movieResponse {
    _id:string
    title: string
    category:string
    release_year:number
    lenght_in_minutes:number
    rating:rating
    Premiere_date:Date
    language:string
    director?: {
        _id:string,
        firstname: string,
        lastname: string,
        bio: string,  
        date_of_birth: Date 
    } 
    actor?: {
        _id:string,
        firstname: string,
        lastname: string,
        bio: string,  
        date_of_birth: Date 
    } 
    prize?:{
        _id:string,
name:string,
award_date:Date,
awardet_for:string
}
};
