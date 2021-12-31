export interface IGroupVisitation {
    groupId: number;
    lastVisited: Date;
    visitationTotalTime: number;
}

export interface IProfileVisitation {
    visitedUserId: number;
    lastVisited: Date;
    visitationTotalTime: number;
}

export interface IPageVisitationActivity {
    groupVisitations: Array<IGroupVisitation>;
    userProfileVisitations: Array<IProfileVisitation>;
    //lastRequestTime?: Date;
}