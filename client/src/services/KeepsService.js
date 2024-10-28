import {api} from './AxiosService.js';
import {AppState} from '@/AppState.js';
import {Keep} from '@/models/Keep.js';

class KeepsService {
  async getAllKeeps() {
    const response = await api.get('api/keeps');
    AppState.keeps = response.data.map((keep) => new Keep(keep));
  }
}

export const keepsService = new KeepsService();
