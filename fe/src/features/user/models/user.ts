import {IDescriptionBlock} from "common/models/description-block";

export interface IUser {
    userId: string;
    name: string;
    surname: string;
    status: string;
    birthdate?: Date;
    descriptionBlocks: Array<IDescriptionBlock>;
}