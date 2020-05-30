import { IGetPatientVm } from '../api/generated';

export interface Patient extends IGetPatientVm {}

export interface PatientBase extends Omit<Patient, 'id'> {}
