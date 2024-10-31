<script setup>
import { Vault } from '@/models/Vault.js';
import { vaultsService } from '@/services/VaultsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { onMounted, ref, watch } from 'vue';

const props = defineProps({
  vaultProp: { type: Vault }
})

const emit = defineEmits(['closed-update-modal']);

const createVaultData = ref({
  name: '',
  description: '',
  isPrivate: false,
  img: ''
});

const updateVaultData = ref({
  name: null,
  description: null,
  isPrivate: null,
  img: null,
});

watch(() => props.vaultProp, () => {
  setupVaultUpdateData();
}, { immediate: true })

onMounted(() => {
  const modalElem = document.getElementById("create-update-vault");
  modalElem.addEventListener('hide.bs.modal', () => {
    emit('closed-update-modal');
  })
})

function setupVaultUpdateData() {
  updateVaultData.value = {
    name: props.vaultProp?.name,
    description: props.vaultProp?.description,
    isPrivate: props.vaultProp?.isPrivate,
    img: props.vaultProp?.imgUrl,
  }
}

async function createVault() {
  try {
    await vaultsService.createVault(createVaultData.value);
    Pop.toast("Vault created successfully", "success", "top");
    createVaultData.value = {
      name: '',
      description: '',
      isPrivate: false,
      img: ''
    }
    Modal.getOrCreateInstance("#create-update-vault").hide();
  }
  catch (e) {
    Pop.error(e);
    logger.log(e);
  }
}

async function updateVault() {
  try {
    const confirmed = await Pop.confirm("Confirm Changes?");
    if (!confirmed) return;
    Modal.getOrCreateInstance('#create-update-vault').hide();
    await vaultsService.updateVault(props.vaultProp.id, updateVaultData.value);
    Pop.toast("Vault updated successfully!", "success", "top");
  }
  catch (e) {
    Pop.error(e);
    logger.log(e);
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
        <form v-if="!vaultProp" @submit.prevent="createVault()">
          <h4 class="my-2">Add your vault</h4>
          <div v-if="createVaultData.img" class="mb-2 d-flex justify-content-center">
            <img class="preview-img" :src="createVaultData.img" alt="Image Preview">
          </div>
          <div class="mb-3">
            <input v-model="createVaultData.name" class="form-control" placeholder="Title..." maxlength="255" required>
          </div>
          <div class="mb-3">
            <textarea v-model="createVaultData.description" class="form-control" placeholder="Description..." rows="7"
              maxlength="1000" required></textarea>
          </div>
          <div class="mb-3">
            <input v-model="createVaultData.img" class="form-control" type="url" name="keepImgUrl" id="keepImgUrl"
              placeholder="Image URL..." maxlength="1000" required>
          </div>
          <div class="mb-3">
            <div class="d-flex flex-column align-items-end">
              <p class="m-0">Private Vaults can only be seen by you</p>
              <div class="d-flex">
                <input v-model="createVaultData.isPrivate" class="form-check-input selectable" type="checkbox"
                  name="vaultPrivate" id="vaultPrivate">
                <label class="form-label m-0 ms-1" for="vaultPrivate">Make Vault Private?</label>
              </div>
            </div>
          </div>
          <div class="text-end mb-3">
            <button class="btn btn-outline-dark rounded">Create Vault</button>
          </div>
        </form>
        <form v-else class="mt-4" @submit.prevent="updateVault()">
          <h4 class="my-2">Update your vault</h4>
          <div v-if="updateVaultData.img" class="mb-2 d-flex justify-content-center">
            <img class="preview-img" :src="updateVaultData.img" alt="Image Preview">
          </div>
          <div class="mb-3">
            <input v-model="updateVaultData.name" class="form-control" placeholder="Title..." maxlength="255" required>
          </div>
          <div class="mb-3">
            <textarea v-model="updateVaultData.description" class="form-control" placeholder="Description..." rows="7"
              maxlength="1000" required></textarea>
          </div>
          <div class="mb-3">
            <input v-model="updateVaultData.img" class="form-control" type="url" name="keepImgUrl" id="keepImgUrl"
              placeholder="Image URL..." maxlength="1000" required>
          </div>
          <div class="mb-3">
            <div class="d-flex flex-column align-items-end">
              <p class="m-0">Remember Private Vaults can only be seen by you</p>
              <div class="d-flex">
                <input v-model="updateVaultData.isPrivate" class="form-check-input selectable" type="checkbox"
                  name="vaultPrivate" id="vaultPrivate">
                <label class="form-label m-0 ms-1" for="vaultPrivate">Keep Vault Private?</label>
              </div>
            </div>
          </div>
          <div class="text-end mb-3">
            <button class="btn btn-outline-dark rounded">Update Vault</button>
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