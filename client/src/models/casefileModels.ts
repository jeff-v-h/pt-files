import { IGetCasefileVm } from '../api/generated';

export interface CasefileBase extends Omit<IGetCasefileVm, 'id' | 'createdAt'> {}
