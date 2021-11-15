import moment from "moment";

export class DateUtils {
    static getViewDate = (date: Date) => moment(date).fromNow()
}
