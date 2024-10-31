<script setup>
import { AppState } from '@/AppState.js';
import KeepCard from '@/components/KeepCard.vue';
import { keepsService } from '@/services/KeepsService.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted, watch } from 'vue';

const account = computed(() => AppState.account);
const keeps = computed(() => AppState.keeps);

onMounted(() => getAllKeeps());

watch(() => account.value, (loggedIn) => {
  if (loggedIn) {
    getAccountVaults();
  }
})

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function getAccountVaults() {
  try {
    await vaultsService.getAccountVaults();
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
      <div class="col-md-10 mx-auto">
        <KeepCard v-if="keeps" />
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss"></style>
