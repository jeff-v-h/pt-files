import { IUpdatePatientCommand } from '../../api/generated';
import { AppThunkAction } from '../index';
import * as T from './patientTypes';
import patientService from '../../api/patientService';
import { Patient, PatientBase } from '../../models/patientModels';
import history from '../../helpers/history';

const { C } = T;

export const createPatient = (patient: PatientBase): AppThunkAction<T.CreatePatientKnownAction> => async (dispatch) => {
  dispatch({ type: C.CREATE_PATIENT_REQUEST });

  try {
    const newPatient = await patientService.createPatient(patient);
    dispatch({ type: C.CREATE_PATIENT_SUCCESS, payload: newPatient });
    history.push(`/patients/${newPatient.id}/casefiles`);
  } catch (e) {
    dispatch({ type: C.CREATE_PATIENT_FAILURE, payload: e });
  }
};

export const getPatients = (): AppThunkAction<T.GetPatientsKnownAction> => async (dispatch) => {
  dispatch({ type: C.GET_PATIENTS_REQUEST });

  try {
    const patients = await patientService.getPatients();
    dispatch({ type: C.GET_PATIENTS_SUCCESS, payload: patients });
  } catch (e) {
    dispatch({ type: C.GET_PATIENTS_FAILURE, payload: e });
  }
};

export const selectPatient = (patient: Patient): T.SelectPatientAction => ({
  type: C.SELECT_PATIENT,
  payload: patient
});

export const getPatient = (id: number): AppThunkAction<T.GetPatientKnownAction> => async (dispatch, getState) => {
  // Only load data if it's something we don't already have (and are not already loading)
  const appState = getState();
  if (id !== appState?.patient?.id) {
    dispatch({ type: C.GET_PATIENT_REQUEST });

    try {
      const patient = await patientService.getPatient(id);
      dispatch({ type: C.GET_PATIENT_SUCCESS, payload: patient });
    } catch (e) {
      dispatch({ type: C.GET_PATIENT_FAILURE, payload: e });
    }
  }
};

export const updatePatient = (
  id: number,
  patient: IUpdatePatientCommand
): AppThunkAction<T.UpdatePatientKnownAction> => async (dispatch) => {
  dispatch({ type: C.UPDATE_PATIENT_REQUEST });

  try {
    await patientService.updatePatient(id, patient);
    dispatch({ type: C.UPDATE_PATIENT_SUCCESS, payload: patient });
  } catch (e) {
    dispatch({ type: C.UPDATE_PATIENT_FAILURE, payload: e });
  }
};

export const deletePatient = (id: number): AppThunkAction<T.DeletePatientKnownAction> => async (dispatch) => {
  dispatch({ type: C.DELETE_PATIENT_REQUEST });

  try {
    await patientService.deletePatient(id);
    dispatch({ type: C.DELETE_PATIENT_SUCCESS, payload: id });
    history.push(`/patients`);
  } catch (e) {
    dispatch({ type: C.DELETE_PATIENT_FAILURE, payload: e });
  }
};
