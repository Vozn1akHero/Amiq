import ISubpagePreloadSpecification from "../subpage-preload-specification";
import {GroupParticipantService} from "features/group/services/group-participant-service";

// TODO
export class IsBannedInGroupPreloadSpecification implements ISubpagePreloadSpecification {
    groupParticipantService = new GroupParticipantService();

    isSatisfied(): boolean|Promise<boolean> {
        return false;
    }
}