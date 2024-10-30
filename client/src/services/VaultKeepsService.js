import {api} from './AxiosService.js';

class VaultKeepsService {
  async createVaultKeep(createVaultKeepData) {
    const response = await api.post('api/vaultkeeps', createVaultKeepData);
  }
}

export const vaultKeepsService = new VaultKeepsService();
