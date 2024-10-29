<script setup>
import ModalWrapper from './ModalWrapper.vue';
import CreateUpdateVaultModal from './CreateUpdateVaultModal.vue';
import { computed, onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import { AppState } from '@/AppState.js';
import Pop from '@/utils/Pop.js';
import { logger } from '@/utils/Logger.js';
import { vaultsService } from '@/services/VaultsService.js';

const route = useRoute();
const account = computed(() => AppState.account);
const vaults = computed(() => AppState.vaults);

onMounted(() => getVaults());

async function getVaults() {
  try {
    if (route.name === 'Account') {
      logger.log("Getting your vaults");
      await vaultsService.getAccountVaults();
    }
    else if (route.name === 'Profile') {
      logger.log("Getting user's public vaults")
    }
  }
  catch (e) {
    Pop.error(e);
    logger.error(e)
  }
}

async function deleteVault(vaultId) {
  try {
    const confirmed = await Pop.confirm("Are you sure, you want to delete this vault?");
    if (!confirmed) return;
    await vaultsService.deleteVault(vaultId);
    Pop.toast("Vault successfully deleted", "success", "top");
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

const vaultToUpdate = ref(null);
const openUpdateVaultModal = (vault) => {
  vaultToUpdate.value = vault
}
</script>


<template>
  <div class="row gy-3">
    <div v-for="vault in vaults" :key="vault.id" class="col-md-3">
      <div class="position-relative shadow-sm">
        <img class="vault-img" :src="vault.imgUrl" :alt="`${vault.name} Vault`">
        <!-- NOTE Clicking vault functionality must go here! -->
        <div v-if="vault.creatorId === account?.id"
          class="position-absolute start-0 end-0 top-0 d-flex justify-content-between pt-1 px-1">
          <div class="special-vault-btn">
            <button @click="openUpdateVaultModal(vault)" class="update-vault-btn" data-bs-toggle="modal"
              data-bs-target="#create-update-vault" type="button"><i class="fa-solid fa-pen"
                style="color: #1b96fa;"></i></button>
          </div>
          <div class="special-vault-btn">
            <button @click="deleteVault(vault.id)" class="delete-vault-btn" type="button"><i class="fa-solid fa-x"
                style="color: #ff3333;"></i></button>
          </div>
        </div>
        <div class="position-absolute start-0 end-0 bottom-0 d-flex justify-content-between px-2">
          <h5 class="text-light fw-bold">{{ vault.name }}</h5>
          <p>{{ vault.isPrivate ? "🔒" : "" }}</p>
        </div>
      </div>
    </div>
  </div>
  <ModalWrapper id="create-update-vault">
    <CreateUpdateVaultModal :vaultProp="vaultToUpdate" @closed-update-modal="vaultToUpdate = null" />
  </ModalWrapper>
</template>


<style lang="scss" scoped>
.vault-img {
  aspect-ratio: 5/4;
  width: 100%;
  height: 170px;
  object-fit: cover;
  object-position: center;
}

.special-vault-btn {
  border: none;
  background: none;
  cursor: pointer;
}

.update-vault-btn,
.delete-vault-btn {
  border-radius: 50%;
  background-color: rgb(0, 0, 0, 40%);
  padding: .2rem .3rem .2rem .3rem;
}
</style>