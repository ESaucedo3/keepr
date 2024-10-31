<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, watch } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const router = useRouter();
const route = useRoute();
const vault = computed(() => AppState.vault);
const vaultKeeps = computed(() => AppState.vaultKeeps);

watch(() => route.params.vaultId, () => {
  getSpecificVault();
  getVaultKeeps();
}, { immediate: true })

async function getSpecificVault() {
  try {
    await vaultsService.getSpecificVault(route.params.vaultId);
  }
  catch (e) {
    Pop.error(e);
    if (e.response.data === 'Vault is currently private only creator may gain access to it!') {
      router.push({ name: 'Home' })
    }
  }
}

async function getVaultKeeps() {
  try {
    await vaultKeepsService.getVaultKeeps(route.params.vaultId);
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

</script>

<template>
  <section v-if="vault && vaultKeeps" class="container">
    <div class="row">
      <div class="col-md-6 mx-auto">
        <div class="position-relative">
          <img class="vault-img rounded" :src="vault.imgUrl" :alt="vault.name">
          <div class="position-absolute bottom-50 start-0 end-0 text-center text-light">
            <h4 class="fw-bold">{{ vault.name }}</h4>
            <p class="fs-4">By {{ vault.creator.name }}</p>
          </div>
          <h4 class="text-center my-2">{{ vaultKeeps.length }} Keeps</h4>
        </div>
      </div>
      <div v-if="vaultKeeps" class="col-md-9 mx-auto">
        <KeepCard />
      </div>
      <div class="text-center" v-else>
        <h3>There are no vault keeps present...</h3>
        <p>Might be worth taking a look around</p>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
.vault-img {
  width: 100%;
  aspect-ratio: 2/1;
  height: 210px;
  object-fit: cover;
  object-position: center;
}

.masonry-design {
  max-width: 100%;
  columns: 4 300px;
  column-gap: 1.3em;

  & img {
    width: 100%;
    display: block;
    margin-bottom: 1em;
  }
}

@media (width <=576px) {
  .masonry-design {
    columns: 2 100px;
  }
}

.special-vault-keep-btn {
  border: none;
  background: none;
  cursor: pointer;
}
</style>