<script setup>
import { accountService } from '@/services/AccountService.js';
import { logger } from '@/utils/Logger.js';
import Pop from '@/utils/Pop.js';
import { Modal } from 'bootstrap';
import { ref } from 'vue';

const editableAccountData = ref({
  name: '',
  coverImg: '',
  picture: ''
})

async function updateAccount() {
  try {
    await accountService.updateAccount(editableAccountData.value);
    Pop.toast("Account updated successfully", "success", "top");
    editableAccountData.value = {
      name: '',
      coverImg: '',
      picture: ''
    }
    Modal.getInstance("#update-account").hide();
  }
  catch (e) {
    Pop.error(e);
    logger.error(e);
  }
}
</script>


<template>
  <section class="container">
    <h3 class="text-center mb-2">Updating Account</h3>
    <form class="row" @submit.prevent="updateAccount()">
      <div class="col-md-6">
        <input v-model="editableAccountData.name" class="form-control" type="text" placeholder="Name" required>
      </div>
      <div class="col-md-6">
        <input v-model="editableAccountData.picture" type="url" class="form-control" placeholder="Picture" required>
      </div>
      <div class="col-12 my-3">
        <input v-model="editableAccountData.coverImg" class="form-control" type="url" name="accountCoverImg"
          id="accountCoverImg" placeholder="Cover Image" required>
      </div>
      <div class="text-end">
        <button class="btn btn-outline-dark">Update</button>
      </div>
    </form>
  </section>
</template>


<style lang="scss" scoped></style>