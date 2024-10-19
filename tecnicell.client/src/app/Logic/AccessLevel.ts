
function checkLevel(role: string){
    switch(role){
        case "KKYW_rkaT_Sñ64_jtRK":
            return AccessLevel.HIGH;
            break;
        case "YHYc_ISif_7os0_ZqBR":
            return AccessLevel.MEDIUM;
            break;
        default :
            return AccessLevel.LOW;
            break;
    }
}
export enum AccessLevel {
    HIGH = 2,
    MEDIUM = 1,
    LOW = 0
}
export default checkLevel;