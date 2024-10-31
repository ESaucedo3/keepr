import {Keep} from './Keep.js';

export class VaultKeep {
  constructor(data) {
    this.id = data.id;
    this.createdAt = new Date(data.createdAt);
    this.updatedAt = new Date(data.updatedAt);
    this.keepId = data.keepId;
    this.vaultId = data.vaultId;
    this.creatorId = data.creatorId;
  }
}

export class VaultKeepKeep extends Keep {
  constructor(data) {
    super(data);
    this.vaultKeepId = data.vaultKeepId;
  }
}
