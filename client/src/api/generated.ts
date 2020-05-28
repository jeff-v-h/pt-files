﻿/* tslint:disable */
/* eslint-disable */
//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.3.0.0 (NJsonSchema v10.1.11.0 (Newtonsoft.Json v12.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------
// ReSharper disable InconsistentNaming



export interface IGetCasefileVm {
    id: number;
    name: string;
    created: string;
    patientId: number;
    consultations: IConsultVm[];
    patient: IFilesPatientVm;
}

export interface IConsultVm {
    id: number;
    date: string;
    number: number;
    practitionerId: number;
}

export interface IFilesPatientVm {
    id: number;
    honorific: Honorific;
    firstName: string;
    lastName: string;
    dob: string;
    occupation: string;
}

/** 0 = Mr 1 = Mrs 2 = Miss 3 = Ms 4 = Master 5 = Mx 6 = M 7 = Sir 8 = Madam 9 = Dr 10 = Prof */
export enum Honorific {
    Mr = 0,
    Mrs = 1,
    Miss = 2,
    Ms = 3,
    Master = 4,
    Mx = 5,
    M = 6,
    Sir = 7,
    Madam = 8,
    Dr = 9,
    Prof = 10,
}

export interface ICreateCasefileCommand {
    patientId: number;
    name: string;
}

export interface IUpdateCasefileCommand {
    id: number;
    patientId: number;
    name: string;
}

export interface IGetConsultationVm {
    id: number;
    casefileId: number;
    date: string;
    number: number;
    practitioner: IPractitionerVm;
    subjectiveAssessment: ISubjectiveAssessmentVm;
    objectiveAssessment: IObjectiveAssessmentVm;
    treatments: string;
    plans: string;
}

export interface IPractitionerVm {
    id: number;
    firstName: string;
    lastName: string;
    jobLevel: string;
}

export interface ISubjectiveAssessmentVm {
    id: number;
    consultationId: number;
    moi: string;
    currentHistory: string;
    bodyChart: string;
    aggravatingFactors: string;
    easingFactors: string;
    vas: number | null;
    pastHistory: string;
    socialHistory: string;
    imaging: string;
    generalHealth: string;
}

export interface IObjectiveAssessmentVm {
    id: number;
    consultationId: number;
    observation: string;
    active: string;
    passive: string;
    resistedIsometric: string;
    functionalTests: string;
    neurologicalTests: string;
    specialTests: string;
    palpation: string;
    additional: string;
}

export interface IUpdateConsultationCommand {
    id: number;
    date: string;
    number: number;
    practitioner: IPractitionerVm | null;
    subjectiveAssessment: ISubjectiveAssessmentVm | null;
    objectiveAssessment: IObjectiveAssessmentVm | null;
    treatments: string;
    plans: string;
}

export interface IGetObjectiveAssessmentVm extends IObjectiveAssessmentVm {
}

export interface IGetPatientsVm {
    patients: IPatientVm[];
}

export interface IPatientVm {
    id: number;
    firstName: string;
    lastName: string;
    dob: string;
}

export interface IPersonVm {
    id: number;
    honorific: Honorific;
    firstName: string;
    lastName: string;
    dob: string;
    email: string;
    countryCode: string;
    homePhone: string;
    mobilePhone: string;
    gender: Gender;
}

export interface IGetPatientVm extends IPersonVm {
    occupation: string;
    casefiles: IPatientCasefileVm[];
}

export interface IPatientCasefileVm {
    id: number;
    name: string;
}

/** 0 = PreferNotToSay 1 = Male 2 = Female 3 = Other */
export enum Gender {
    PreferNotToSay = 0,
    Male = 1,
    Female = 2,
    Other = 3,
}

export interface ICreatePatientCommand {
    honorific: Honorific;
    firstName: string;
    lastName: string;
    dob: string;
    email: string;
    countryCode: string;
    homePhone: string;
    mobilePhone: string;
    gender: Gender;
    occupation: string;
}

export interface IUpdatePatientCommand {
    id: number;
    honorific: Honorific;
    firstName: string;
    lastName: string;
    dob: string;
    email: string;
    countryCode: string;
    homePhone: string;
    mobilePhone: string;
    gender: Gender;
    occupation: string;
}

export interface IGetSubjectiveAssessmentVm extends ISubjectiveAssessmentVm {
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}