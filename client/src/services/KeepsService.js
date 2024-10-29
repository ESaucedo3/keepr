import {api} from './AxiosService.js';
import {AppState} from '@/AppState.js';
import {Keep} from '@/models/Keep.js';

class KeepsService {
  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id === keepId);
    AppState.keeps.splice(keepIndex, 1);
  }
  async createKeep(keepData) {
    const response = await api.post('api/keeps', keepData);
    const createdKeep = new Keep(response.data);
    AppState.keeps.push(createdKeep);
  }
  async getAllKeeps() {
    const response = await api.get('api/keeps');
    AppState.keeps = response.data.map((keep) => new Keep(keep));
  }
  async getSpecificKeep(keepId) {
    const response = await api.get(`api/keeps/${keepId}`);
    const modifiedKeep = new Keep(response.data);
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id === keepId);
    AppState.keeps.splice(keepIndex, 1, modifiedKeep);
  }

  async getUserKeeps() {
    const response = await api.get(`api/profiles/67101eb1422e8c2c9b081a25/keeps`);
    console.log(response.data);
  }
  async getAccountKeeps(accountId) {
    const response = await api.get(`api/profiles/${accountId}/keeps`);
    AppState.keeps = response.data.map((keep) => new Keep(keep));
  }
}

export const keepsService = new KeepsService();
