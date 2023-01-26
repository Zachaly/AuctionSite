import { Gender } from "../enum/Gender";

export default interface RegisterModel {
    username: string,
    email: string,
    password: string,
    firstName?: string,
    lastName?: string,
    phoneNumber?: string,
    city?: string,
    postalCode?: string,
    country?: string,
    address?: string,
    gender?: Gender
}