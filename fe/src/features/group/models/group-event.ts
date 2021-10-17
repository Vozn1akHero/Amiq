export interface IGroupEvent {
    groupEventId: string;
    name: string;
    avatarSrc: string;
    date: Date;
    //participantsCount: number;
    groupEventParticipants: Array<IGroupEventParticipant>;
    isCancelled:boolean;
    isHidden: boolean;
}

export interface IGroupEventParticipant {
    groupEventParticipantId: string;
    groupEventId: string;
    groupParticipantId: string;
    joinedAt: Date;
}