export interface IGroupEvent {
    groupEventId: string;
    name: string;
    avatarSrc: string;
    date: Date;
    description: string;
    //participantsCount: number;
    //groupEventParticipants: Array<IGroupEventParticipant>;
    groupEventParticipantsCount: number;
    isCancelled:boolean;
    isHidden: boolean;
    isRequestCreatorParticipant: boolean;
}

export interface IGroupEventParticipant {
    groupEventParticipantId: string;
    groupEventId: string;
    groupParticipantId: string;
    joinedAt: Date;
}