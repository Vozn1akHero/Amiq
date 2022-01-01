export interface INotification {
    notificationId: string;
    notificationGroupId: string;
    text: string;
    createdAt: Date;
    imageSrc: string;
    link: string;
    isRead: boolean;
}