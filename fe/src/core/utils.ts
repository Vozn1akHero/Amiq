import devConfig from "dev-config.json";

export class Utils {
    static getImageSrc(imageSrc: string) {
        return devConfig.monolithUrl + "/" + imageSrc;
    }
}