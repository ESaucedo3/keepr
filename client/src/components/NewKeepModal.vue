<script setup>
import { ref } from 'vue';

const editableKeepData = ref({

})

async function createKeep() {

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
        <form class="mt-4" @submit.prevent="">
          <div class="mb-2">
            <input class="form-control" placeholder="Title..." required>
          </div>
          <div class="mb-2">
            <input class="form-control" type="url" name="keepImgUrl" id="keepImgUrl" placeholder="Image URL..."
              required>
          </div>
          <div class="mb-2">
            <textarea class="form-control" name="keepDescription" id="keepDescription" placeholder="Keep Description..."
              rows="7" required></textarea>
          </div>
          <div class="text-end">
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