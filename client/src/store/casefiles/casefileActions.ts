import { IUpdateCasefileCommand, IGetCasefileVm } from './../../api/generated';
import { AppThunkAction } from '../index';
import * as T from './casefileTypes';
import casefileService from '../../api/casefileService';
import { CasefileBase } from '../../models/casefileModels';
import history from '../../helpers/history';

const { C } = T;

export const createCasefile = (casefile: CasefileBase): AppThunkAction<T.CreateCasefileKnownAction> => async (
  dispatch
) => {
  dispatch({ type: C.CREATE_CASEFILE_REQUEST });

  try {
    const newCasefile = await casefileService.createCasefile(casefile);

    dispatch({ type: C.CREATE_CASEFILE_SUCCESS, payload: newCasefile });
    history.push(`/patients/${newCasefile.patientId}/casefiles/${newCasefile.id}/consultations`);
  } catch (e) {
    dispatch({ type: C.CREATE_CASEFILE_FAILURE });
  }
};

export const getCasefiles = (patientId: number): AppThunkAction<T.GetCasefilesKnownAction> => async (dispatch) => {
  dispatch({ type: C.GET_CASEFILES_REQUEST });

  try {
    const casefiles = await casefileService.getCasefiles(patientId);
    dispatch({ type: C.GET_CASEFILES_SUCCESS, payload: casefiles });
  } catch (e) {
    dispatch({ type: C.GET_CASEFILES_FAILURE });
  }
};

export const selectCasefile = (casefile: IGetCasefileVm): T.SelectCasefileAction => ({
  type: C.SELECT_CASEFILE,
  payload: casefile
});

export const getCasefile = (id: number): AppThunkAction<T.GetCasefileKnownAction> => async (dispatch, getState) => {
  const appState = getState();
  if (appState?.casefile?.id !== id) {
    dispatch({ type: C.GET_CASEFILE_REQUEST });

    try {
      const casefile = await casefileService.getCasefile(id);
      dispatch({ type: C.GET_CASEFILE_SUCCESS, payload: casefile });
    } catch (e) {
      dispatch({ type: C.GET_CASEFILE_FAILURE });
    }
  }
};

export const updateCasefile = (
  id: number,
  file: IUpdateCasefileCommand
): AppThunkAction<T.UpdateCasefileKnownAction> => async (dispatch) => {
  dispatch({ type: C.UPDATE_CASEFILE_REQUEST });

  try {
    await casefileService.updateCasefile(id, file);
    dispatch({ type: C.UPDATE_CASEFILE_SUCCESS, payload: file });
  } catch (e) {
    dispatch({ type: C.UPDATE_CASEFILE_FAILURE });
  }
};

export const deleteCasefile = (id: number): AppThunkAction<T.DeleteCasefileKnownAction> => async (
  dispatch,
  getState
) => {
  dispatch({ type: C.DELETE_CASEFILE_REQUEST });

  try {
    await casefileService.deleteCasefile(id);

    const appState = getState();
    const patientId = appState.casefile?.patientId;

    dispatch({ type: C.DELETE_CASEFILE_SUCCESS, payload: id });
    history.push(`/patients/${patientId}/casefiles`);
  } catch (e) {
    dispatch({ type: C.DELETE_CASEFILE_FAILURE });
  }
};
