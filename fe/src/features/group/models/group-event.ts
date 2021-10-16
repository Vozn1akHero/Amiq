export interface IGroupEvent {
    groupEventId: string;
    name: string;
    avatarSrc: string;
    date: Date;
    participantsCount: number;
    isCancelled:boolean;
    isHidden: boolean;
}