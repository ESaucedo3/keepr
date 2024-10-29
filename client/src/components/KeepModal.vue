<script setup>
import { Account } from '@/models/Account.js';
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { ref, watch } from 'vue';

const props = defineProps({
  accountProp: { type: Account },
  keepProp: { type: Keep, required: true },
})

watch(() => props.keepProp, () => {
  incrementKeepViewCount();
}, { immediate: true });

const vaultSelected = ref('');

async function incrementKeepViewCount() {
  await keepsService.getSpecificKeep(props.keepProp.id);
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
            <form v-if="accountProp" class="align-self-end" @submit.prevent="">
              <select class="form-select" name="vaultSelect" id="vaultSelect">
                <option disabled selected value="">Select a vault</option>
                <option value=""></option>
              </select>
              <button v-if="vaultSelected !== ''" class="btn btn-outline-dark rounded">Save</button>
            </form>
            <div class="ms-auto">
              <RouterLink :to="{ name: 'Account' }" :title="`Go to ${keepProp.creator.name}'s profile`">
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