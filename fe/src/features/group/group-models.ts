export interface IGroupCard {
    groupId: number;
    name: string;
    avatarSrc: string;
    //participantsCount: number;
    participants: Array<IGroupParticipant>;
    subjects: Array<string>;
}

export interface IGroupParticipant {
    userId: number;
    groupId: number;
    name: string;
    surname: string;
    avatarPath: string;
}