import { AxiosResponse } from 'axios';
import * as G from './generated';
import { message } from 'antd';
import { ApiService } from './apiService';
import { keys } from '../helpers/keys';

const { apiUrl } = keys;

class ConsultationService extends ApiService {
  async createConsultation(consult: G.ICreateConsultationCommand): Promise<number> {
    try {
      const url = `${apiUrl}/consultations`;
      const resp = (await this.post(url, consult)) as AxiosResponse<number>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async getConsultations(casefileId?: number): Promise<G.IGetConsultationBaseVm[]> {
    try {
      let url = `${apiUrl}/consultations`;
      if (casefileId) url += `?casefileId=${casefileId}`;
      const resp = (await this.get(url)) as AxiosResponse<G.IGetConsultationBaseVm[]>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async getConsultation(id: number): Promise<G.IGetConsultationVm> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      const resp = (await this.get(url)) as AxiosResponse<G.IGetConsultationVm>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async updateConsultation(id: number, consult: G.IUpdateConsultationCommand): Promise<void> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      (await this.put(url, consult)) as AxiosResponse<void>;
      message.success('Consultation saved');
    } catch (e) {
      return this.handleRequestError(e);
    }
  }

  async deleteConsultation(id: number): Promise<void> {
    try {
      const url = `${apiUrl}/consultations/${id}`;
      const resp = (await this.delete(url)) as AxiosResponse<void>;
      message.success('Consultation deleted');
    } catch (e) {
      return this.handleRequestError(e);
    }
  }
}

const consultationService = new ConsultationService();
export default consultationService;
