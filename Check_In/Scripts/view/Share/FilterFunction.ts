import moment = require("moment");

moment.locale('zh-tw');
export const dateToDateTimeString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('YYYY-MM-DD HH:mm:ss');
    return emptystr;
}

export const dateToDateString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('YYYY-MM-DD');
    return emptystr;
}

export const dateToTimeString = (date: Date | null, emptystr: any = '-') => {
    if (date)
        return moment(date).format('HH:mm:ss');
    return emptystr;
}

export const dateToWeek = (date: Date | null, emptystr: any = '-') => {
    if (date) {
        switch (moment(date).format("E")) {
            case "1":
                return '一';
            case "2":
                return '二';
            case "3":
                return '三';
            case "4":
                return '四';
            case "5":
                return '五';
            case "6":
                return '六';
            case "7":
                return '日';
            default:
                return '-'
        }
    }
}

export const TimeSpanToString = (date: any | null, emptystr: any = '-') => {
    if (date.Ticks != 0)
        return moment(date).format('HH:mm:ss');
    return emptystr;
}

export const GetLatestNewsEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '媒體報導';
        case 1:
            return '新聞報導';
        case 2:
            return '測試報導';
        default:
            return '-'
    }
}

export const GetCaseEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '行銷活動';
        case 1:
            return '臉書機器人';
        case 2:
            return '官網';
        default:
            return '-'
    }
}

export const GetBackOperationResult = (date: boolean | null) => {
    switch (date) {
        case true:
            return '成功';
        case false:
            return '失敗';
    }
}

export const GetSexEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '先生';
        case 1:
            return '小姐';
    }
}

export const GetContactEnum = (date: number | null) => {
    switch (date) {
        case 0:
            return '網站建置';
        case 1:
            return '聊天機器人';
        case 2:
            return '客製化抽獎';
        case 3:
            return '其他';
        default:
            return '-';
    }
}

export const GetContactStatus = (date: number | null) => {
    switch (date) {
        case 0:
            return '全新專案';
        case 1:
            return '改版增修';
        default:
            return '-';
    }
}
