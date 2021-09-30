export interface IGroupPost {
    postId: number;
    userId: number;
    textContent: string;
    groupId: string;
    editedBy?: number;
    editedAt?: Date;
    createdAt: Date;
}