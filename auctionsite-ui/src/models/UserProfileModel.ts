import { Gender } from "./enum/Gender";

export default interface UserProfileModel {
    id: string,
    userName: string,
    firstName?: string,
    lastName?: string,
    phoneNumber?: string,
    city?: string,
    postalCode?: string,
    country?: string,
    address?: string,
    gender?: Gender
}