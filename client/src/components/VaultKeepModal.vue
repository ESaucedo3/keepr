<script setup>
import { AppState } from '@/AppState.js';
import { Account } from '@/models/Account.js';
import { VaultKeepKeep } from '@/models/VaultKeep.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed } from 'vue';
import { useRoute } from 'vue-router';

const vault = computed(() => AppState.vault);

const props = defineProps({
  vaultKeepProp: { type: VaultKeepKeep, required: true },
  accountProp: { type: Account }
})

async function deleteVaultKeep(keepId, vaultKeepId) {
  await vaultKeepsService.deleteVaultKeep(keepId, vaultKeepId);
  Pop.toast("Vault Keep successfully deleted", "success", "top");
  Modal.getInstance("#vault-keep-details").hide();
}
</script>


<template>
  <section class="container-fluid">
    <div class="row">
      <div class="col-md-6 p-0">
        <div>
          <img class="keep-img" :src="vaultKeepProp.imgUrl" :alt="vaultKeepProp.name">
        </div>
      </div>
      <div class="col-md-6">
        <div class="d-flex flex-column justify-content-between align-items-center h-100 px-2 py-3">
          <div class="d-flex">
            <p><i class="fa-regular fa-eye"></i> {{ vaultKeepProp.views }} | <i class="fa-solid fa-floppy-disk"></i> {{
              vaultKeepProp.kept }}</p>
          </div>

          <div class="details">
            <h4 class="text-center">{{ vaultKeepProp.name }}</h4>
            <p>{{ vaultKeepProp.description }}</p>
          </div>

          <div class="d-flex justify-content-between w-100">
            <button v-if="vault.creatorId === accountProp?.id"
              @click="deleteVaultKeep(vaultKeepProp.id, vaultKeepProp.vaultKeepId)" type="button"
              class="delete-vault-keep-btn"><i class="fa-solid fa-x" style="color: #ff0000;"></i>
              Remove</button>
            <div class="ms-auto">
              <RouterLink v-if="accountProp?.id === vaultKeepProp.creatorId" :to="{ name: 'Account' }"
                :title="`Go to ${vaultKeepProp.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="vaultKeepProp.creator.picture" :alt="vaultKeepProp.creator.name">
                  <p class="m-0 ms-2">{{ vaultKeepProp.creator.name }}</p>
                </div>
              </RouterLink>
              <RouterLink v-else :to="{ name: 'Profile', params: { profileId: vaultKeepProp.creatorId } }"
                :title="`Go to ${vaultKeepProp.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="vaultKeepProp.creator.picture" :alt="vaultKeepProp.creator.name">
                  <p class="m-0 ms-2">{{ vaultKeepProp.creator.name }}</p>
                </div>
              </RouterLink>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
a {
  text-decoration: none;
  color: inherit;
}

.keep-img {
  width: 100%;
  height: 475px;
  object-fit: cover;
  object-position: center;
  aspect-ratio: 2/1;
}

.creator-img {
  height: 70px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
}

@media (width < 768px) {
  .details {
    margin-top: 75px;
    margin-bottom: 75px;
  }
}

.delete-vault-keep-btn {
  border: none;
  background: none;
  cursor: pointer;
}
</style>