
import { BaseService } from "core/base-service";
import {IPageVisitationActivity} from "./models";
import {ServicesDictionary} from "../../core/dictionaries/modules-dictionary";

export class ActivityTrackingService extends BaseService {
    apiModule = "activity";
    service = ServicesDictionary.Notification;

    create(pageVisitationActivity : IPageVisitationActivity){
        return this.httpClient.post(this.buildApiPath(), pageVisitationActivity);
    }
}