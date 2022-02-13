import devConfig from "dev-config.json";
import moment from "moment";

export class Utils {
    static getImageSrc(imageSrc: string) {
        return devConfig.staticContentUrl + "/" + imageSrc;
    }

    static getDifferenceBetweenDates = (t1: Date, t2: Date) : number => {
        let dif = new Date(t1).getTime() - new Date(t2).getTime();
        let Seconds_from_T1_to_T2 = dif / 1000;
        return Math.abs(Seconds_from_T1_to_T2);
    }


}
