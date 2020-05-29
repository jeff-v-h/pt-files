import { AxiosResponse } from 'axios';
import * as G from './generated';
import { message } from 'antd';
import { ApiService } from './apiService';
import { keys } from '../helpers/keys';

const { apiUrl } = keys;

class CasefileService extends ApiService {
  createCasefile = async (casefile: G.ICreateCasefileCommand): Promise<G.IGetCasefileVm> => {
    try {
      const url = `${apiUrl}/casefiles`;
      const resp = (await this.post(url, casefile)) as AxiosResponse<number>;
      return { ...casefile, id: resp.data, created: '' };
    } catch (e) {
      return this.handleRequestError(e);
    }
  };

  getCasefiles = async (patientId: string): Promise<G.IGetCasefileVm[]> => {
    try {
      const url = `${apiUrl}/casefiles?patientId=${patientId}`;
      const resp = (await this.get(url)) as AxiosResponse<G.IGetCasefileVm[]>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  };

  getCasefile = async (id: string): Promise<G.IGetCasefileVm> => {
    try {
      const url = `${apiUrl}/casefiles/${id}`;
      const resp = (await this.get(url)) as AxiosResponse<G.IGetCasefileVm>;
      return resp.data;
    } catch (e) {
      return this.handleRequestError(e);
    }
  };

  updateCasefile = async (id: string, casefile: G.IUpdateCasefileCommand): Promise<void> => {
    try {
      const url = `${apiUrl}/casefiles/${id}`;
      await this.put(url, casefile);
      message.success('Casefile updated');
    } catch (e) {
      return this.handleRequestError(e);
    }
  };

  deleteCasefile = async (id: string): Promise<void> => {
    try {
      const url = `${apiUrl}/casefiles/${id}`;
      await this.delete(url);
      message.success('Casefile deleted');
    } catch (e) {
      return this.handleRequestError(e);
    }
  };
}

const casefileService = new CasefileService();
export default casefileService;
