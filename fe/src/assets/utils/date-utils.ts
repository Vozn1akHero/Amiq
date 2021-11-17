import moment from "moment";

export class DateUtils {
    static getViewDate = (date: Date) => moment(date).fromNow();

    static getDifferenceBetweenDatesInDays = (date1: Date, date2: Date) : number => {
        const momentDate1 = moment(date1);
        const momentDate2 = moment(date2);
        return momentDate2.diff(momentDate1, 'days');
    }
}
