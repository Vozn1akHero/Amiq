import moment from "moment";

export const getViewDate = (date: Date) => moment(date).fromNow()
