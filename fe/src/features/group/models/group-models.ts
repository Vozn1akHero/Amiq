import {IDescriptionBlock} from "../../../common/models/description-block";

export interface IGroupCard {
    groupId: number;
    name: string;
    avatarSrc: string;
    participantsCount: number;
    isHidden: boolean;
    description: string;
    //participants: Array<IGroupParticipant>;
    subjects: Array<string>;
    isRequestCreatorParticipant: boolean;
}

export interface IGroupParticipant {
    userId: number;
    groupId: number;
    name: string;
    surname: string;
    avatarPath: string;
}

export interface IGroupData {
    groupId: number;
    name: string;
    avatarSrc: string;
    participantsCount: number;
    description: string;
    //participants: Array<IGroupParticipant>;
    subjects: Array<string>;
    descriptionBlocks: Array<IDescriptionBlock>;
}

export interface IGroupViewer {
    groupId: number;
    userId: number;
    groupViewerRole: EnGroupViewerRole;
}

export enum EnGroupViewerRole {
    Creator,
    Admin,
    Participant,
    Guest,
    Blocked
}

export interface IGroupUserParams {
    groupId: number;
    isHidden: boolean;
}