import {Profile} from '@/models/Profile.js';
import {AppState} from '../AppState.js';
import {Account} from '../models/Account.js';
import {logger} from '../utils/Logger.js';
import {api} from './AxiosService.js';

class AccountService {
  async updateAccount(accountUpdateData) {
    const response = await api.put(`account`, accountUpdateData);
    const updatedAccount = new Account(response.data);
    AppState.account = updatedAccount;
  }
  async getUserProfile(profileId) {
    const response = await api.get(`api/profiles/${profileId}`);
    const user = new Profile(response.data);
    AppState.profile = user;
  }
  async getAccount() {
    try {
      const res = await api.get('/account');
      AppState.account = new Account(res.data);
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err);
    }
  }
}

export const accountService = new AccountService();
