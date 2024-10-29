<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { computed, onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import ModalWrapper from './ModalWrapper.vue';
import KeepModal from './KeepModal.vue';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';

const route = useRoute();
const account = computed(() => AppState.account);
const keeps = computed(() => AppState.keeps);

onMounted(() => getKeeps());

async function getKeeps() {
  try {
    if (route.name === 'Home') {
      logger.log("Retreiving everyone's keeps");
      await keepsService.getAllKeeps();
    }
    else if (route.name === 'Profile') {
      logger.log("Retreiving specific user's keeps");
      await keepsService.getUserKeeps();
    }
    else if (route.name === 'Account') {
      logger.log("Retreiving your keeps");
      await keepsService.getAccountKeeps(account.value.id);
    }
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

const selectedKeep = ref(null);
const openKeepDetailsModal = (keep) => {
  selectedKeep.value = keep;
}
</script>


<template>
  <div class="masonry-design mb-3">
    <div v-for="keep in keeps" :key="keep.id" class="rounded shadow position-relative">
      <img class="rounded" :src="keep.imgUrl" :alt="keep.name">
      <div class="position-absolute start-0 end-0 bottom-0 d-flex justify-content-between align-items-end">
        <div class="mb-2 px-3">
          <h5 class="m-0 fw-bold fs-4 text-light">{{ keep.name }}</h5>
        </div>

        <div class="mb-2 px-3">
          <img :title="`${keep.creator.name}'s picture'`" class="user-img m-0" :src="keep.creator.picture"
            :alt="keep.creator.name">
        </div>
      </div>
      <button @click="openKeepDetailsModal(keep)" class="position-absolute top-0 start-0 w-100 h-100 special-keep-btn"
        type="button" data-bs-toggle="modal" data-bs-target="#keep-details"></button>
      <button v-if="keep.creatorId === account?.id" class="position-absolute update-keep-btn" type="button"><i
          class="fa-solid fa-pen" style="color: #1b96fa;"></i></button>
      <button @click="deleteKeep(keep.id)" v-if="keep.creatorId === account?.id"
        class="position-absolute delete-keep-btn" type="button"><i class="fa-solid fa-x"
          style="color: #ff3333;"></i></button>
    </div>
  </div>
  <ModalWrapper id="keep-details">
    <KeepModal v-if="selectedKeep" :keepProp="selectedKeep" :accountProp="account" />
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
</style>