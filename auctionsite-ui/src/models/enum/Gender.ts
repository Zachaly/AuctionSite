export enum Gender {
    not_specified = 0,
    male, 
    female
}

export const genderToString = (gender: Gender) => {
    if(gender === Gender.female){
        return 'Female'
    } else if(gender === Gender.male){
        return 'Male'
    }

    return 'Not specified'
}