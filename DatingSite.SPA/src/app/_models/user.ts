import { Photo } from './photo';

export interface User {
    /* Basic info */
    id: number;
    username: string;
    gender: string;
    age: number;
    zodiacSign: string;
    dateOfCreated: Date;
    lastActive: Date;
    city: string;
    country: string;

    /* Info page */
    growth: string;
    eyeColor: string;
    hairColor: string;
    martialStatus: string;
    education: string;
    profession: string;
    children: string;
    languages: string;

    /* about me page */
    motto: string;
    description: string;
    prsonality: string;
    lookingFor: string;

    /* interests page */
    interests: string;
    freeTime: string;
    activity: string;
    movies: string;
    music: string;

    /* preferences page */
    iLike: string;
    iDoNotLike: string;
    makesMeHappy: string;
    friendsWouldDescribeMe: string;
    photos: Photo[];
    photoUrl: string;
}



