
import { BaseService } from "core/base-service";
import {IPageVisitationActivity} from "./models";

export class ActivityTrackingService extends BaseService {
    apiModule = "activity";

    create(pageVisitationActivity : IPageVisitationActivity){
        return this.httpClient.post(this.buildApiPath(), pageVisitationActivity);
    }
}