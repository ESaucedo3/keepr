<script setup>
import ModalWrapper from './components/ModalWrapper.vue';
import Navbar from './components/Navbar.vue';
import NewKeepModal from './components/NewKeepModal.vue';
import NewVaultModal from './components/NewVaultModal.vue';

</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <ModalWrapper id="new-keep">
    <NewKeepModal />
  </ModalWrapper>
  <ModalWrapper id="new-vault">
    <NewVaultModal />
  </ModalWrapper>
</template>

<style lang="scss">
@import '@/assets/scss/main.scss';

:root {
  --main-height: calc(100vh - 32px - 64px);
}
</style>
