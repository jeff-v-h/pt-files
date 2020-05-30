import { AppThunkAction } from '../index';
import * as T from './consultationTypes';
import consultationService from '../../api/consultationService';
import casefileService from '../../api/casefileService';
import { TreatmentsAndPlans } from '../../models/consultationModels';
import history from '../../helpers/history';
import * as G from '../../api/generated';

const { C } = T;
//#region simple action creators
export const getConsultsRequest = () => ({ type: C.GET_CONSULTATIONS_REQUEST });
export const getConsultsSuccess = (
  consults: G.IGetConsultationBaseVm[]
): T.GetConsultsSuccessAction => {
  return { type: C.GET_CONSULTATIONS_SUCCESS, payload: consults };
};
export const getConsultsFailure = () => ({ type: C.GET_CONSULTATIONS_FAILURE });

export const getConsultRequest = () => ({ type: C.GET_CONSULTATION_REQUEST });
export const getConsultSuccess = (consult: G.IGetConsultationVm): T.GetConsultSuccessAction => {
  return { type: C.GET_CONSULTATION_SUCCESS, payload: consult };
};
export const getConsultFailure = () => ({ type: C.GET_CONSULTATION_FAILURE });

export const clearConsult = () => ({ type: C.CLEAR_CONSULTATION });
export const modifyDate = (date: string) => ({ type: C.MODIFY_DATE, payload: date });
export const modifySubjective = (subjective: G.IGetSubjectiveAssessmentVm) =>
  ({ type: C.MODIFY_SUBJECTIVE, payload: subjective } as T.ModifySubjective);

export const modifyObjective = (objective: G.IGetObjectiveAssessmentVm) =>
  ({ type: C.MODIFY_OBJECTIVE, payload: objective } as T.ModifyObjective);

export const modifyTreatmentsAndPlans = (treatmentAndPlans: TreatmentsAndPlans) =>
  ({
    type: C.MODIFY_TREATMENTS_AND_PLANS,
    payload: treatmentAndPlans
  } as T.ModifyTreatmentsAndPlans);
//#endregion

//#region Thunk actions creators
export const createConsult = (
  newConsult: G.ICreateConsultationCommand
): AppThunkAction<T.CreateConsultKnownAction> => async (dispatch, getState) => {
  dispatch({ type: C.CREATE_CONSULTATION_REQUEST });

  try {
    const id = await consultationService.createConsultation(newConsult);

    const appState = getState();
    let patientId = appState.patient?.id;
    if (!patientId) {
      const casefile = await casefileService.getCasefile(newConsult.casefileId);
      patientId = casefile.patientId;
    }

    dispatch({ type: C.CREATE_CONSULTATION_SUCCESS, payload: newConsult });
    history.push(`/patients/${patientId}/casefiles/${newConsult.casefileId}/consultations`);
  } catch (e) {
    dispatch({ type: C.CREATE_CONSULTATION_FAILURE });
  }
};

export const getConsults = (casefileId: number): AppThunkAction<T.GetConsultKnownAction> => async (
  dispatch
) => {
  dispatch(getConsultsRequest());

  try {
    const consults = await consultationService.getConsultations(casefileId);
    dispatch(getConsultsSuccess(consults));
  } catch (e) {
    dispatch(getConsultsFailure());
  }
};

export const getConsult = (id: number): AppThunkAction<T.GetConsultKnownAction> => async (
  dispatch,
  getState
) => {
  const appState = getState();
  if (appState?.consultation?.id !== id) {
    dispatch(getConsultRequest());

    try {
      const consult = await consultationService.getConsultation(id);
      dispatch(getConsultSuccess(consult));
    } catch (e) {
      dispatch(getConsultFailure());
    }
  }
};

export const updateConsult = (
  id: number,
  consult: G.IUpdateConsultationCommand
): AppThunkAction<T.UpdateConsultKnownAction> => async (dispatch) => {
  dispatch({ type: C.UPDATE_CONSULTATION_REQUEST });

  try {
    await consultationService.updateConsultation(id, consult);
    dispatch({ type: C.UPDATE_CONSULTATION_SUCCESS, payload: consult });
  } catch (e) {
    dispatch({ type: C.UPDATE_CONSULTATION_FAILURE });
  }
};

export const deleteConsult = (id: number): AppThunkAction<T.DeleteConsultKnownAction> => async (
  dispatch,
  getState
) => {
  dispatch({ type: C.DELETE_CONSULTATION_REQUEST });

  try {
    await consultationService.deleteConsultation(id);

    const appState = getState();
    const casefileId = appState.consultation?.casefileId as number;
    let patientId = appState.patient?.id;

    if (!patientId) {
      const casefile = await casefileService.getCasefile(casefileId);
      patientId = casefile.patientId;
    }

    dispatch({ type: C.DELETE_CONSULTATION_SUCCESS, payload: id });
    history.push(`/patients/${patientId}/casefiles/${casefileId}/consultations`);
  } catch (e) {
    dispatch({ type: C.DELETE_CONSULTATION_FAILURE });
  }
};
//#endregion
