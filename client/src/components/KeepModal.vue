<script setup>
import { AppState } from '@/AppState.js';
import { Account } from '@/models/Account.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { computed, onMounted, ref, watch } from 'vue';
import { onBeforeRouteLeave, useRoute } from 'vue-router';

const route = useRoute();
const vaults = computed(() => AppState.vaults);

const props = defineProps({
  accountProp: { type: Account },
  keepProp: { type: Keep, required: true },
})

const createVaultKeepData = ref({
  keepId: null,
  vaultId: null
});

onMounted(() => {
  if (route.name === 'Home') {
    getAccountVaults();
  }
})

onBeforeRouteLeave(() => {
  Modal.getInstance("#keep-details").hide();
})

watch(() => props.keepProp, () => {
  incrementKeepViewCount();
  createVaultKeepData.value.keepId = props.keepProp?.id;
}, { immediate: true });

async function incrementKeepViewCount() {
  await keepsService.getSpecificKeep(props.keepProp.id);
}

async function getAccountVaults() {
  try {
    logger.log("Getting vaults because you are signed in")
    await vaultsService.getAccountVaults();
  }
  catch (error) {
    Pop.error(error);
  }
}

async function createVaultKeep() {
  try {
    await vaultKeepsService.createVaultKeep(createVaultKeepData.value);
    Pop.toast("VaultKeep created successfully!", "success", "top");
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

</script>

<template>
  <section class="container-fluid">
    <div class="row">
      <div class="col-md-6 p-0">
        <div>
          <img class="keep-img" :src="keepProp.imgUrl" :alt="keepProp.name">
        </div>
      </div>
      <div class="col-md-6">
        <div class="d-flex flex-column justify-content-between align-items-center h-100 px-2 py-3">
          <div class="d-flex">
            <p><i class="fa-regular fa-eye"></i> {{ keepProp.views }} | <i class="fa-solid fa-floppy-disk"></i> {{
              keepProp.kept }}</p>
          </div>

          <div class="details">
            <h4 class="text-center">{{ keepProp.name }}</h4>
            <p>{{ keepProp.description }}</p>
          </div>

          <div class="d-flex justify-content-between w-100">
            <form v-if="route.name !== 'Profile'" class="align-self-end" @submit.prevent="createVaultKeep()">
              <div class="d-flex">
                <select v-model="createVaultKeepData.vaultId" class="form-select" name="vaultSelect" id="vaultSelect">
                  <option disabled selected value="">Select a vault</option>
                  <option v-for="vault in vaults" :key="vault.id" :value="vault.id">{{ vault.name }}</option>
                </select>
                <button v-if="createVaultKeepData.vaultId" class="btn btn-outline-dark rounded ms-3">Save</button>
              </div>
            </form>
            <div class="ms-auto">
              <RouterLink v-if="accountProp?.id === keepProp.creatorId" :to="{ name: 'Account' }"
                :title="`Go to ${keepProp.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="keepProp.creator.picture" :alt="keepProp.creator.name">
                  <p class="m-0 ms-2">{{ keepProp.creator.name }}</p>
                </div>
              </RouterLink>
              <RouterLink v-else :to="{ name: 'Profile', params: { profileId: keepProp.creatorId } }"
                :title="`Go to ${keepProp.creator.name}'s profile`">
                <div class="d-flex align-items-center">
                  <img class="creator-img" :src="keepProp.creator.picture" :alt="keepProp.creator.name">
                  <p class="m-0 ms-2">{{ keepProp.creator.name }}</p>
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
</style>