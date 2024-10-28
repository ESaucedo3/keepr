<script setup>

</script>


<template>
  <section class="container">
    <div class="row">
      <div class="col-12">
        <div class="text-end">
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <h4>Add your vault</h4>
        <form class="mt-4" @submit.prevent="">
          <div class="mb-2">
            <input class="form-control" placeholder="Title..." required>
          </div>
          <div class="mb-2">
            <input class="form-control" type="url" name="keepImgUrl" id="keepImgUrl" placeholder="Image URL..."
              required>
          </div>
          <div class="mb-2">
            <div class="d-flex flex-column">
              <p>Private Vaults can only be seen by you</p>
              <div class="d-flex">
                <input class="form-check-input" type="checkbox" name="vaultPrivate" id="vaultPrivate">
                <label class="form-label m-0 ms-1" for="vaultPrivate">Make Vault Private?</label>
              </div>
            </div>
          </div>
          <div class="text-end">
            <button class="btn btn-outline-dark rounded">Create Vault</button>
          </div>
        </form>
      </div>
    </div>
  </section>
</template>


<style lang="scss" scoped></style>