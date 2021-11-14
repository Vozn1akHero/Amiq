import devConfig from "dev-config.json";

export class Utils {
    static getImageSrc(imageSrc: string) {
        return devConfig.monolithUrl + "/" + imageSrc;
    }

    static getDifferenceBetweenDates = (t1: Date, t2: Date) : number => {
        console.log(t1, t2)
        let dif = new Date(t1).getTime() - new Date(t2).getTime();
        let Seconds_from_T1_to_T2 = dif / 1000;
        let seconds_Between_Dates = Math.abs(Seconds_from_T1_to_T2);
        return seconds_Between_Dates;
    }
}