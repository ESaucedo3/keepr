<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { computed, ref } from 'vue';
import ModalWrapper from './ModalWrapper.vue';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import CreateUpdateKeepModal from './CreateUpdateKeepModal.vue';
import { useRoute } from 'vue-router';
import { vaultKeepsService } from '@/services/VaultKeepsService.js';
import KeepModal from './KeepModal.vue';
import { Modal } from 'bootstrap';

const route = useRoute();
const account = computed(() => AppState.account);
const keeps = computed(() => AppState.keeps);
const vaultKeeps = computed(() => AppState.vaultKeeps);

async function getSpecificKeep(keepId) {
  try {
    await keepsService.getSpecificKeep(keepId);
    Modal.getOrCreateInstance("#keep-details").show();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function getSpecificVaultKeep(vaultKeep) {
  try {
    await vaultKeepsService.getSpecificVaultKeep(vaultKeep);
    Modal.getOrCreateInstance("#keep-details").show();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function deleteKeep(keepId) {
  try {
    const confirmed = await Pop.confirm("Confirm Deletion?");
    if (!confirmed) return
    await keepsService.deleteKeep(keepId);
    Pop.toast("Keep successfully deleted", 'success', 'top');
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

const keepToUpdate = ref(null);
const openUpdateKeepModal = (keep) => {
  keepToUpdate.value = keep;
  Modal.getOrCreateInstance("#create-update-keep").show();
}
</script>


<template>
  <div v-if="route.name !== 'Vault'" class="masonry-design mb-3">
    <div v-for="keep in keeps" :key="keep.id" class="rounded shadow position-relative">
      <img class="rounded" :src="keep.imgUrl" :alt="keep.name">
      <div class="position-absolute start-0 end-0 bottom-0 d-flex justify-content-between align-items-end">
        <div class="mb-2 px-3">
          <h5 class="m-0 fw-bold fs-4 text-light keep-name-design">{{ keep.name }}</h5>
        </div>

        <div class="mb-2 px-3">
          <img :title="`${keep.creator.name}'s picture'`" class="user-img m-0" :src="keep.creator.picture"
            :alt="keep.creator.name">
        </div>
      </div>


      <button class="position-absolute top-0 start-0 w-100 h-100 special-keep-btn" @click="getSpecificKeep(keep.id)"
        type="button"></button>
      <button @click="openUpdateKeepModal(keep)" v-if="keep.creatorId === account?.id"
        class="position-absolute update-keep-btn" type="button"><i class="fa-solid fa-pen"
          style="color: #1b96fa;"></i></button>
      <button @click="deleteKeep(keep.id)" v-if="keep.creatorId === account?.id"
        class="position-absolute delete-keep-btn" type="button"><i class="fa-solid fa-x"
          style="color: #ff3333;"></i></button>
    </div>
  </div>
  <div v-else class="masonry-design mb-3">
    <div v-for="vaultKeep in vaultKeeps" :key="vaultKeep.id" class="rounded shadow position-relative">
      <img class="rounded" :src="vaultKeep.imgUrl" :alt="vaultKeep.name">
      <div class="position-absolute start-0 end-0 bottom-0 d-flex justify-content-between align-items-end">
        <div class="mb-2 px-3">
          <h5 class="m-0 fw-bold fs-4 text-light">{{ vaultKeep.name }}</h5>
        </div>

        <div class="mb-2 px-3">
          <img :title="`${vaultKeep.creator.name}'s picture'`" class="user-img m-0" :src="vaultKeep.creator.picture"
            :alt="vaultKeep.creator.name">
        </div>
      </div>

      <button class="position-absolute top-0 start-0 w-100 h-100 special-keep-btn"
        @click="getSpecificVaultKeep(vaultKeep)" type="button"></button>
    </div>
  </div>
  <ModalWrapper id="create-update-keep">
    <CreateUpdateKeepModal :keepProp="keepToUpdate" @closed-edit-modal="keepToUpdate = null" />
  </ModalWrapper>
  <ModalWrapper id="keep-details">
    <KeepModal />
  </ModalWrapper>
</template>


<style lang="scss" scoped>
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

.user-img {
  height: 70px;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
  object-position: center;
}

.special-keep-btn {
  border: none;
  background: none;
  cursor: pointer;
}

.update-keep-btn,
.delete-keep-btn {
  border-radius: 50%;
  background-color: rgb(0, 0, 0, 40%);
  padding: .2rem .3rem .2rem .3rem;
  top: 0;
}

.update-keep-btn {
  margin-left: .3rem;
  margin-top: .3rem;
  left: 0;
}

.delete-keep-btn {
  margin-right: .3rem;
  margin-top: .3rem;
  right: 0;
}

.keep-name-design {
  text-shadow: 1px 1px 9px #333;
}
</style>