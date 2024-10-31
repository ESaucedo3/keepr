import {api} from './AxiosService.js';
import {AppState} from '@/AppState.js';
import {Keep} from '@/models/Keep.js';
import {logger} from '@/utils/Logger.js';

class KeepsService {
  async createKeep(createKeepData) {
    const response = await api.post('api/keeps', createKeepData);
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
    logger.log('👉', modifiedKeep);
    const keep = AppState.keeps.find((keep) => keep.id === keepId);
    // AppState.keeps.splice(keepIndex, 1, modifiedKeep);
    keep.views = modifiedKeep.views;
    keep.kept = modifiedKeep.kept;
  }
  async updateKeep(keepId, updatedKeepData) {
    const response = await api.put(`api/keeps/${keepId}`, updatedKeepData);
    const updatedKeep = new Keep(response.data);
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id === keepId);
    AppState.keeps.splice(keepIndex, 1, updatedKeep);
  }
  async deleteKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`);
    const keepIndex = AppState.keeps.findIndex((keep) => keep.id === keepId);
    AppState.keeps.splice(keepIndex, 1);
  }
  async getProfileKeeps(profileId) {
    const response = await api.get(`api/profiles/${profileId}/keeps`);
    AppState.keeps = response.data.map((keep) => new Keep(keep));
  }
  async getAccountKeeps(accountId) {
    const response = await api.get(`api/profiles/${accountId}/keeps`);
    AppState.keeps = response.data.map((keep) => new Keep(keep));
  }
}

export const keepsService = new KeepsService();
