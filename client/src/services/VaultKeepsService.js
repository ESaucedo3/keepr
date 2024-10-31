import {logger} from '@/utils/Logger.js';
import {api} from './AxiosService.js';
import {AppState} from '@/AppState.js';
import {VaultKeepKeep} from '@/models/VaultKeep.js';

class VaultKeepsService {
  async deleteVaultKeep(keepId, vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`);
    const findIndex = AppState.vaultKeeps.findIndex((vaultKeep) => vaultKeep.id === keepId);
    AppState.vaultKeeps.splice(findIndex, 1);
  }
  async getVaultKeeps(vaultId) {
    AppState.vaultKeeps = [];
    const response = await api.get(`api/vaults/${vaultId}/keeps`);
    AppState.vaultKeeps = response.data.map((vaultKeep) => new VaultKeepKeep(vaultKeep));
  }
  async createVaultKeep(createVaultKeepData) {
    const response = await api.post('api/vaultkeeps', createVaultKeepData);
    logger.log(response.data);
  }
}

export const vaultKeepsService = new VaultKeepsService();
