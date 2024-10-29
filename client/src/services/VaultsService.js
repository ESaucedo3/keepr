import {AppState} from '@/AppState.js';
import {api} from './AxiosService.js';
import {Vault} from '@/models/Vault.js';

class VaultsService {
  async createVault(createVaultData) {
    const response = await api.post('api/vaults', createVaultData);
    const createdVault = new Vault(response.data);
    AppState.vaults.push(createdVault);
  }
  async getAccountVaults() {
    const response = await api.get('account/vaults');
    AppState.vaults = response.data.map((vault) => new Vault(vault));
  }
  async updateVault(vaultId, updatedVaultData) {
    const response = await api.put(`api/vaults/${vaultId}`, updatedVaultData);
    const updatedVault = new Vault(response.data);
    const vaultIndex = AppState.vaults.findIndex((vault) => vault.id === vaultId);
    AppState.vaults.splice(vaultIndex, 1, updatedVault);
  }
  async deleteVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`);
    const vaultIndex = AppState.vaults.findIndex((vault) => vault.id === vaultId);
    AppState.vaults.splice(vaultIndex, 1);
  }
}

export const vaultsService = new VaultsService();
