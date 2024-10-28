<script setup>
import { AppState } from '@/AppState.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { computed, onMounted } from 'vue';

onMounted(() => getAllKeeps());

const keeps = computed(() => AppState.keeps);

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps();
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
        <div class="masonry-design">
          <div v-for="keep in keeps" :key="keep.id" class="rounded shadow position-relative">
            <img :src="keep.imgUrl" :alt="keep.name">
            <div class="position-absolute start-0 end-0 bottom-0 d-flex justify-content-between align-items-end">
              <div class="mb-2 px-3">
                <h5 class="m-0 fw-bold fs-4 text-light">{{ keep.name }}</h5>
              </div>

              <div class="mb-2 px-3">
                <img class="user-img m-0" :src="keep.creator.picture" :alt="keep.creator.name">
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<style scoped lang="scss">
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
</style>
