import * as G from '../../api/generated';
import { Action } from 'redux';
import { TreatmentsAndPlans } from '../../models/consultationModels';

export const C = {
  CREATE_CONSULTATION_REQUEST: 'CREATE_CONSULTATION_REQUEST',
  CREATE_CONSULTATION_SUCCESS: 'CREATE_CONSULTATION_SUCCESS',
  CREATE_CONSULTATION_FAILURE: 'CREATE_CONSULTATION_FAILURE',
  GET_CONSULTATIONS_REQUEST: 'GET_CONSULTATIONS_REQUEST',
  GET_CONSULTATIONS_SUCCESS: 'GET_CONSULTATIONS_SUCCESS',
  GET_CONSULTATIONS_FAILURE: 'GET_CONSULTATIONS_FAILURE',
  GET_CONSULTATION_REQUEST: 'GET_CONSULTATION_REQUEST',
  GET_CONSULTATION_SUCCESS: 'GET_CONSULTATION_SUCCESS',
  GET_CONSULTATION_FAILURE: 'GET_CONSULTATION_FAILURE',
  UPDATE_CONSULTATION_REQUEST: 'UPDATE_CONSULTATION_REQUEST',
  UPDATE_CONSULTATION_SUCCESS: 'UPDATE_CONSULTATION_SUCCESS',
  UPDATE_CONSULTATION_FAILURE: 'UPDATE_CONSULTATION_FAILURE',
  DELETE_CONSULTATION_REQUEST: 'DELETE_CONSULTATION_REQUEST',
  DELETE_CONSULTATION_SUCCESS: 'DELETE_CONSULTATION_SUCCESS',
  DELETE_CONSULTATION_FAILURE: 'DELETE_CONSULTATION_FAILURE',
  MODIFY_DATE: 'MODIFY_DATE',
  MODIFY_SUBJECTIVE: 'MODIFY_SUBJECTIVE',
  MODIFY_OBJECTIVE: 'MODIFY_OBJECTIVE',
  MODIFY_TREATMENTS_AND_PLANS: 'MODIFY_TREATMENTS_AND_PLANS',
  CLEAR_CONSULTATION: 'CLEAR_CONSULTATION'
};

export interface ConsultationState extends G.IGetConsultationVm {
  isFetching: boolean;
  list: G.IGetConsultationBaseVm[];
}

export interface CreateConsultSuccessAction extends Action {
  payload: G.ICreateConsultationCommand;
}

export interface GetConsultsSuccessAction extends Action {
  payload: G.IGetConsultationBaseVm[];
}

export interface GetConsultSuccessAction extends Action {
  payload: G.IGetConsultationVm;
}

export interface UpdateConsultSuccessAction extends Action {
  payload: G.IUpdateConsultationCommand;
}

export interface DeleteConsultSuccessAction extends Action {
  payload: number;
}

export interface ModifyDate extends Action {
  payload: string;
}

export interface ModifySubjective extends Action {
  payload: G.ISubjectiveAssessmentVm;
}

export interface ModifyObjective extends Action {
  payload: G.IObjectiveAssessmentVm;
}

export interface ModifyTreatmentsAndPlans extends Action {
  payload: TreatmentsAndPlans;
}

export type CreateConsultKnownAction = Action | CreateConsultSuccessAction;
export type GetConsultsKnownAction = Action | GetConsultsSuccessAction;
export type GetConsultKnownAction = Action | GetConsultSuccessAction;
export type UpdateConsultKnownAction = Action | UpdateConsultSuccessAction;
export type DeleteConsultKnownAction = Action | DeleteConsultSuccessAction;

export type KnownAction =
  | CreateConsultKnownAction
  | GetConsultsKnownAction
  | GetConsultKnownAction
  | UpdateConsultKnownAction
  | DeleteConsultKnownAction
  | ModifySubjective
  | ModifyObjective
  | ModifyTreatmentsAndPlans
  | ModifyDate;
