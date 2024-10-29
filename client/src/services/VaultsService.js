import {AppState} from '@/AppState.js';
import {api} from './AxiosService.js';
import {Vault} from '@/models/Vault.js';

class VaultsService {
  async getAccountVaults() {
    const response = await api.get('account/vaults');
    AppState.accountVaults = response.data.map((accountVault) => new Vault(accountVault));
  }
}

export const vaultsService = new VaultsService();
