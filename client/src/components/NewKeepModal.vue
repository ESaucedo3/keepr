<script setup>
import { keepsService } from '@/services/KeepsService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

// Left off here
// TODO Find a way to reuse NewKeepModal & NewVaultModal to create keeps & vaults but ALSO UPDATE keeps & vaults they do the same exact thing just different methods literally

const editableKeepData = ref({
  name: '',
  img: '',
  description: ''
})

async function createKeep() {
  try {
    await keepsService.createKeep(editableKeepData.value);
    Pop.toast("Keep created successfully!", "success", "top");
    editableKeepData.value = {
      name: '',
      img: '',
      description: ''
    }
    Modal.getInstance('#new-keep').hide();
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
        <h4>Add your keep</h4>
        <form class="mt-4" @submit.prevent="createKeep()">
          <div class="mb-2">
            <input v-model="editableKeepData.name" class="form-control" id="keepName" name="keepName"
              placeholder="Title..." maxlength="255" required>
          </div>
          <div class="mb-2">
            <input v-model="editableKeepData.img" class="form-control" type="url" name="keepImgUrl" id="keepImgUrl"
              placeholder="Image URL..." maxlength="1000" required>
          </div>
          <div class="mb-2">
            <textarea v-model="editableKeepData.description" class="form-control" name="keepDescription"
              id="keepDescription" placeholder="Keep Description..." rows="7" maxlength="1000" required></textarea>
          </div>
          <div class="text-end mb-2">
            <button class="btn btn-outline-dark rounded">Create</button>
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
</style>