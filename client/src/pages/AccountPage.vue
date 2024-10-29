<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import KeepsMasonry from '@/components/KeepsMasonry.vue';

const account = computed(() => AppState.account);
const accountVaults = computed(() => AppState.accountVaults);

onMounted(() => getAccountVaults());

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
  <section v-if="account" class="container">
    <div class="row">
      <div class="col-md-7 mx-auto">
        <div>
          <img class="account-coverImg rounded" :src="account.coverImg" :alt="`${account.name}'s cover image!'`">
          <div class="d-flex justify-content-between">
            <div></div>

            <div>
              <img class="account-img" :src="account.picture" alt="">
            </div>

            <div>
              <button title="Edit Account" class="special-btn mt-1" type="button"><i class="fa-solid fa-pen fa-lg"
                  style="color: #44a9fb;"></i></button>
            </div>
          </div>

          <div class="text-center">
            <h3>{{ account.name }}</h3>
            <p>2 vaults | 2 keeps</p>
          </div>
        </div>

      </div>
      <div v-if="accountVaults" class="col-md-9 mx-auto">
        <h3 class="ms-1">Vaults</h3>
        <div class="row gy-3">
          <div v-for="accountVault in accountVaults" :key="accountVault.id" class="col-md-3">
            <img class="vault-img" :src="accountVault.imgUrl" alt="">
          </div>
        </div>
      </div>
      <KeepsMasonry />
    </div>
  </section>
  <div v-else>
    <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
  </div>
</template>

<style scoped lang="scss">
.account-coverImg {
  width: 100%;
  height: 300px;
  object-fit: cover;
  object-position: center;
}

.account-img {
  aspect-ratio: 1/1;
  height: 77px;
  border-radius: 50%;
  box-shadow: 1px 1px 9px #333;
  border: 1px solid azure;
  position: relative;
  top: -40px;
  left: 17px;
}

.special-btn {
  background-color: lightblue;
  border-radius: 50%;
  border: 2px solid midnightblue;
  padding: .2rem .5rem .2rem .5rem;
}

.vault-img {
  aspect-ratio: 1/1;
  width: 100%;
  height: 150px;
  object-fit: cover;
  object-position: center;
}

.masonry-design {
  max-width: 100%;
  columns: 4 300px;
  column-gap: 1em;

  & img {
    width: 100%;
    display: block;
    margin-bottom: 1em;
  }
}
</style>
