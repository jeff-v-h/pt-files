import { message } from 'antd';
import moment from 'moment';

export function getParsedUrlId(id: string) {
  const parsedId = parseInt(id, 10);
  if (isNaN(parsedId)) {
    message.error(`${id} is not a number`);
    return 0;
  }

  return parsedId;
}

export enum ConsultPart {
  Subjective,
  Objective,
  Treatments,
  Plan
}

export function capitalise(word: string) {
  return word.charAt(0).toUpperCase() + word.substring(1).toLowerCase();
}

export function parseDateString(ds: string) {
  if (!ds) return '';
  return moment(ds).format('Do MMM YYYY');
}
