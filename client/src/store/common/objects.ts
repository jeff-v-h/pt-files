import { ISubjectiveAssessmentVm, IObjectiveAssessmentVm } from './../../api/generated';

export const emptySubjective: ISubjectiveAssessmentVm = {
  id: 0,
  consultationId: 0,
  moi: '',
  currentHistory: '',
  bodyChart: '',
  aggravatingFactors: '',
  easingFactors: '',
  vas: 0,
  pastHistory: '',
  socialHistory: '',
  imaging: '',
  generalHealth: ''
};

export const emptyObjective: IObjectiveAssessmentVm = {
  id: 0,
  consultationId: 0,
  observation: '',
  active: '',
  passive: '',
  resistedIsometric: '',
  functionalTests: '',
  neurologicalTests: '',
  specialTests: '',
  palpation: '',
  additional: ''
};
