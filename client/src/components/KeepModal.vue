<script setup>
import { AppState } from '@/AppState.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const vaults = computed(() => AppState.vaults);
const account = computed(() => AppState.account);
const activeKeep = computed(() => AppState.activeKeep);
const activeVaultKeep = computed(() => AppState.activeVaultKeep);
const vault = computed(() => AppState.vault);

const createVaultKeepData = ref({
  keepId: null,
  vaultId: null
});

async function createVaultKeep() {
  try {
    createVaultKeepData.value.keepId = activeKeep.value.id;
    await vaultKeepsService.createVaultKeep(createVaultKeepData.value);
    Pop.toast("VaultKeep created successfully!", "success", "top");
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function deleteVaultKeep(keepId, vaultKeepId) {
  const confirmed = await Pop.confirm("You sure you want to remove this keep from your vault?");
  if (!confirmed) return;
  Modal.getOrCreateInstance("#keep-details").hide();
  await vaultKeepsService.deleteVaultKeep(keepId, vaultKeepId);
  Pop.toast("Vault Keep successfully deleted", "success", "top");
}

</script>

<template>
  <section class="container-fluid">
    <div v-if="activeKeep && route.name !== 'Vault'" class="row">
      <div class="col-md-6 p-0">
        <div>
          <img class="keep-img" :src="activeKeep.imgUrl" :alt="activeKeep.name">
        </div>
      </div>
      <div class="col-md-6">
        <div class="d-flex flex-column justify-content-between align-items-center h-100 px-2 py-3">
          <div class="d-flex">
            <p><i class="fa-regular fa-eye"></i> {{ activeKeep.views }} | <i class="fa-solid fa-floppy-disk"></i> {{
              activeKeep.kept }}</p>
          </div>

          <div class="details">
            <h4 class="text-center">{{ activeKeep.name }}</h4>
            <p>{{ activeKeep.description }}</p>
          </div>

          <div class="d-flex justify-content-between w-100">
            <form v-if="route.name === 'Home' && account" class="align-self-end" @submit.prevent="createVaultKeep()">
              <div class="d-flex">
                <select v-model="createVaultKeepData.vaultId" class="form-select" name="vaultSelect" id="vaultSelect">
                  <option disabled selected value="">Select a vault</option>
                  <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                </select>
                <button v-if="createVaultKeepData.vaultId" class="btn btn-outline-dark rounded ms-3">Save</button>
              </div>
            </form>
            <div class="ms-auto">
              <RouterLink v-if="account && account?.id === activeKeep.creatorId" :to="{ name: 'Account' }"
                :title="`Go to ${activeKeep.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name">
                  <p class="m-0 ms-2">{{ activeKeep.creator.name }}</p>
                </div>
              </RouterLink>
              <RouterLink v-else :to="{ name: 'Profile', params: { profileId: activeKeep.creatorId } }"
                :title="`Go to ${activeKeep.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="activeKeep.creator.picture" :alt="activeKeep.creator.name">
                  <p class="m-0 ms-2">{{ activeKeep.creator.name }}</p>
                </div>
              </RouterLink>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-else-if="activeVaultKeep && route.name === 'Vault'" class="row">
      <div class="col-md-6 p-0">
        <div>
          <img class="keep-img" :src="activeVaultKeep.imgUrl" :alt="activeVaultKeep.name">
        </div>
      </div>
      <div class="col-md-6">
        <div class="d-flex flex-column justify-content-between align-items-center h-100 px-2 py-3">
          <div class="d-flex">
            <p><i class="fa-regular fa-eye"></i> {{ activeVaultKeep.views }} | <i class="fa-solid fa-floppy-disk"></i>
              {{
                activeVaultKeep.kept }}</p>
          </div>

          <div class="details">
            <h4 class="text-center">{{ activeVaultKeep.name }}</h4>
            <p>{{ activeVaultKeep.description }}</p>
          </div>

          <div class="d-flex justify-content-between w-100">
            <button v-if="vault.creatorId === account?.id" type="button" class="delete-vault-keep-btn"
              @click="deleteVaultKeep(activeVaultKeep.id, activeVaultKeep.vaultKeepId)">
              <i class="fa-solid fa-x" style="color: #ff0000;"></i>
              Remove
            </button>
            <div class="ms-auto">
              <RouterLink v-if="account && account?.id === activeVaultKeep.creatorId" :to="{ name: 'Account' }"
                :title="`Go to ${activeVaultKeep.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="activeVaultKeep.creator.picture" :alt="activeVaultKeep.creator.name">
                  <p class="m-0 ms-2">{{ activeVaultKeep.creator.name }}</p>
                </div>
              </RouterLink>
              <RouterLink v-else :to="{ name: 'Profile', params: { profileId: activeVaultKeep.creatorId } }"
                :title="`Go to ${activeVaultKeep.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="activeVaultKeep.creator.picture" :alt="activeVaultKeep.creator.name">
                  <p class="m-0 ms-2">{{ activeVaultKeep.creator.name }}</p>
                </div>
              </RouterLink>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      Loading...
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