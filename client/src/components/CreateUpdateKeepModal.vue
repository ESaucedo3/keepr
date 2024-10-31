<script setup>
import { Keep } from '@/models/Keep.js';
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { onMounted, ref, watch } from 'vue';

const props = defineProps({
  keepProp: { type: Keep }
})

const emit = defineEmits(['closed-edit-modal'])

const createKeepData = ref({
  name: '',
  img: '',
  description: ''
})

const updatableKeepData = ref({
  name: null,
  img: null,
  description: null
})

watch(() => props.keepProp, () => {
  setupKeepUpdateData();
}, { immediate: true })

onMounted(() => {
  const modalElem = document.getElementById("create-update-keep");
  modalElem.addEventListener('hide.bs.modal', () => {
    emit('closed-edit-modal')
  })
})


function setupKeepUpdateData() {
  updatableKeepData.value = {
    name: props.keepProp?.name,
    img: props.keepProp?.imgUrl,
    description: props.keepProp?.description
  }
}

async function createKeep() {
  try {
    await keepsService.createKeep(createKeepData.value);
    Pop.toast("Keep created successfully!", "success", "top");
    createKeepData.value = {
      name: '',
      img: '',
      description: ''
    }
    Modal.getOrCreateInstance('#create-update-keep').hide();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

async function updateKeep() {
  try {
    const confirmed = await Pop.confirm("Confirm Changes?");
    if (!confirmed) return;
    await keepsService.updateKeep(props.keepProp.id, updatableKeepData.value);
    Pop.toast("Keep updated successfully!", "success", "top");
    Modal.getOrCreateInstance('#create-update-keep').hide();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}

</script>


<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div class="text-end">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <form v-if="!keepProp" @submit.prevent="createKeep()">
          <h4 class="my-2">Add your keep</h4>
          <div v-if="createKeepData.img" class="mb-2 d-flex justify-content-center">
            <img class="preview-img" :src="createKeepData.img" alt="Image Preview">
          </div>
          <div class="mb-2">
            <input v-model="createKeepData.name" class="form-control" id="keepName" name="keepName"
              placeholder="Title..." maxlength="255" required>
          </div>
          <div class="mb-2">
            <input v-model="createKeepData.img" class="form-control" type="url" name="keepImgUrl" id="keepImgUrl"
              placeholder="Image URL..." maxlength="1000" required>
          </div>
          <div class="mb-2">
            <textarea v-model="createKeepData.description" class="form-control" name="keepDescription"
              id="keepDescription" placeholder="Keep Description..." rows="7" maxlength="1000" required></textarea>
          </div>
          <div class="text-end mb-2">
            <button class="btn btn-outline-dark rounded">Create</button>
          </div>
        </form>
        <form v-else class="mt-4" @submit.prevent="updateKeep()">
          <h4 class="my-2">Update your keep</h4>
          <div v-if="updatableKeepData.img" class="mb-2 d-flex justify-content-center">
            <img class="preview-img" :src="updatableKeepData.img" alt="Image Preview">
          </div>
          <div class="mb-2">
            <input v-model="updatableKeepData.name" class="form-control" id="keepName" name="keepName"
              placeholder="Title..." maxlength="255" required>
          </div>
          <div class="mb-2">
            <input v-model="updatableKeepData.img" class="form-control" type="url" name="keepImgUrl" id="keepImgUrl"
              placeholder="Image URL..." maxlength="1000" required>
          </div>
          <div class="mb-2">
            <textarea v-model="updatableKeepData.description" class="form-control" name="keepDescription"
              id="keepDescription" placeholder="Keep Description..." rows="7" maxlength="1000" required></textarea>
          </div>
          <div class="text-end mb-2">
            <button class="btn btn-outline-dark rounded">Update</button>
          </div>
        </form>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped>
textarea {
  resize: none;
}

.preview-img {
  aspect-ratio: 2/1;
  height: 300px;
  width: 40%;
  object-fit: cover;
  object-position: center;
  border-radius: 10px;
}
</style>