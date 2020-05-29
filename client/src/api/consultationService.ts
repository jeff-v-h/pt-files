import { AxiosResponse } from 'axios';
import * as I from './generated';
import { message } from 'antd';
import { ApiService } from './apiService';
import { keys } from '../helpers/keys';

const { apiUrl } = keys;

class ConsultationService extends ApiService {
  async createConsultation(consult: I.ICreateConsultationCommand): Promise<I.IGetConsultationVm> {
    try {
      const url = `${apiUrl}/consultations`;
      const resp = (await this.post(url, consult)) as AxiosResponse<number>;
      return { ...consult, id: resp.data };
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async getConsultations(casefileId?: string): Promise<I.IGetConsultationBaseVm[]> {
    try {
      let url = `${apiUrl}/consultations`;
      if (casefileId) url += `?casefileId=${casefileId}`;
      const resp = (await this.get(url)) as AxiosResponse<I.IGetConsultationBaseVm[]>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async getConsultation(id: number): Promise<I.IGetConsultationVm> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      const resp = (await this.get(url)) as AxiosResponse<I.IGetConsultationVm>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async updateConsultation(id: number, consult: I.IUpdateConsultationCommand): Promise<void> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      (await this.put(url, consult)) as AxiosResponse<void>;
      message.success('Consultation saved');
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async deleteConsultation(id: string): Promise<void> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      const resp = (await this.delete(url)) as AxiosResponse<void>;
      message.success('Consultation deleted');
    } catch (e) {
      return this.handleRequestError(e);
    }
  }
}

export const consultationService = new ConsultationService();
