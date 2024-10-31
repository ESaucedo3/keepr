import {reactive} from 'vue';
import {Keep} from './models/Keep.js';
import {Vault} from './models/Vault.js';
import {Profile} from './models/Profile.js';
import {VaultKeepKeep} from './models/VaultKeep.js';

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,
  /**@type {Keep[]} */
  keeps: [],
  /**@type {Vault[]} */
  vaults: [],
  /**@type {Profile} */
  profile: null,
  /**@type {Vault} */
  vault: null,
  /**@type {VaultKeepKeep[]} */
  vaultKeeps: [],
});
