<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import VaultCard from '@/components/VaultCard.vue';
import { accountService } from '@/services/AccountService.js';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, watch } from 'vue';
import { useRoute } from 'vue-router';

const profile = computed(() => AppState.profile);
const keeps = computed(() => AppState.keeps);
const vaults = computed(() => AppState.vaults);

const route = useRoute();
watch(() => route.params.profileId, () => {
  getUserProfile();
  getProfileVaults();
  getProfileKeeps();
}, { immediate: true })

async function getUserProfile() {
  try {
    await accountService.getUserProfile(route.params.profileId);
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function getProfileVaults() {
  try {
    await vaultsService.getProfileVaults(route.params.profileId);
  }
  catch (e) {
    Pop.error(e);
    logger.log(e);
  }
}

async function getProfileKeeps() {
  try {
    await keepsService.getProfileKeeps(route.params.profileId);
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}
</script>


<template>
  <section v-if="profile" class="container">
    <div class="row">
      <div class="col-md-7 mx-auto">
        <div>
          <img class="profile-coverImg rounded" :src="profile.coverImg" :alt="`${profile.name}'s cover image!'`">
          <div>
            <img class="profile-img" :src="profile.picture" alt="">
          </div>


          <div class="text-center">
            <h3>{{ profile.name }}</h3>
            <p>{{ vaults.length }} Vaults | {{ keeps.length }} Keeps </p>
          </div>
        </div>
      </div>
      <div v-if="vaults && keeps" class="col-md-9 mx-auto">
        <h3 class="ms-1">Vaults</h3>
        <VaultCard v-if="vaults" />

        <h3 class="ms-1 mt-4">Keeps</h3>
        <KeepCard v-if="keeps" />
      </div>
    </div>
  </section>
  <div v-else>
    <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
  </div>
</template>


<style lang="scss" scoped>
.profile-coverImg {
  width: 100%;
  height: 300px;
  object-fit: cover;
  object-position: center;
}

.profile-img {
  aspect-ratio: 1/1;
  height: 77px;
  border-radius: 50%;
  box-shadow: 1px 1px 9px #333;
  border: 1px solid azure;
  position: relative;
  top: -40px;
  left: 50%;
  transform: translate(-50%);
}
</style>