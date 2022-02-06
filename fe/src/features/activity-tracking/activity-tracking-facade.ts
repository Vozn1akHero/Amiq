import {IPageVisitationActivity} from "./models";
import moment from "moment";

export class ActivityTrackingFacade {
    static storeGroupActivity(groupId: number, visitationTimeInMinutes: number){
        if(!sessionStorage.getItem("act"))
        {
            const obj : IPageVisitationActivity = {
                groupVisitations: [],
                userProfileVisitations: [],
            };
            sessionStorage.setItem("act", JSON.stringify(obj));
        }

        let visitationState = JSON.parse(sessionStorage.getItem("act")) as IPageVisitationActivity;

        if(!visitationState){
            visitationState = {
                userProfileVisitations: [],
                groupVisitations: []
            }
        }

        if(!visitationState.groupVisitations){
            visitationState.groupVisitations = [];
        }

        let groupVisitationIndex = visitationState.groupVisitations.findIndex(e=>e.groupId === groupId);
        if(groupVisitationIndex!==-1){
            visitationState.groupVisitations = visitationState.groupVisitations.map((value, index) => {
                if(index === groupVisitationIndex){
                    value.visitationTotalTime += visitationTimeInMinutes;
                    value.lastVisited =  moment().toDate()
                }
                return value;
            });
        } else {
            visitationState.groupVisitations.push({
                groupId,
                lastVisited: moment().toDate(),
                visitationTotalTime: visitationTimeInMinutes
            })
        }

        sessionStorage.setItem("act", JSON.stringify(visitationState));
    }

    static storeProfileActivity(profileId: number, visitationTimeInMinutes: number) {
        if(!sessionStorage.getItem("act"))
        {
            const obj : IPageVisitationActivity = {
                groupVisitations: [],
                userProfileVisitations: []
            };
            sessionStorage.setItem("act", JSON.stringify(obj));
        }

        let visitationState = JSON.parse(sessionStorage.getItem("act")) as IPageVisitationActivity;

        if(!visitationState){
            visitationState = {
                userProfileVisitations: [],
                groupVisitations: []
            }
        }

        if(!visitationState.userProfileVisitations){
            visitationState.userProfileVisitations = [];
        }

        let profileVisitationIndex = visitationState.userProfileVisitations.findIndex(e=>e.visitedUserId === profileId);
        if(profileVisitationIndex!==-1){
            visitationState.userProfileVisitations = visitationState.userProfileVisitations.map((value, index) => {
                if(index === profileVisitationIndex){
                    value.visitationTotalTime += visitationTimeInMinutes;
                    value.lastVisited =  moment().toDate()
                }
                return value;
            });
        } else {
            visitationState.userProfileVisitations.push({
                visitedUserId: profileId,
                lastVisited: moment().toDate(),
                visitationTotalTime: visitationTimeInMinutes
            })
        }

        sessionStorage.setItem("act", JSON.stringify(visitationState));
    }

    static resetState(){
        localStorage.removeItem("act")
    }
}