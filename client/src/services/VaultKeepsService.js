import {api} from './AxiosService.js';
import {AppState} from '@/AppState.js';
import {VaultKeepKeep} from '@/models/VaultKeep.js';

class VaultKeepsService {
  async getSpecificVaultKeep(vaultKeep) {
    AppState.activeVaultKeep = vaultKeep;
    AppState.activeVaultKeep.views++;
  }
  async createVaultKeep(createVaultKeepData) {
    await api.post('api/vaultkeeps', createVaultKeepData);
    AppState.activeKeep.kept++;
  }
  async getVaultKeeps(vaultId) {
    AppState.vaultKeeps = [];
    const response = await api.get(`api/vaults/${vaultId}/keeps`);
    AppState.vaultKeeps = response.data.map((vaultKeep) => new VaultKeepKeep(vaultKeep));
  }
  async deleteVaultKeep(keepId, vaultKeepId) {
    await api.delete(`api/vaultkeeps/${vaultKeepId}`);
    AppState.activeVaultKeep.kept--;
    const findIndex = AppState.vaultKeeps.findIndex((vaultKeep) => vaultKeep.id === keepId);
    AppState.vaultKeeps.splice(findIndex, 1);
  }
}

export const vaultKeepsService = new VaultKeepsService();
