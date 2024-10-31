<script setup>
import { useRoute } from 'vue-router';
import Login from './Login.vue';
import { computed } from 'vue';
import { AppState } from '@/AppState.js';
import { Modal } from 'bootstrap';
const route = useRoute();
const account = computed(() => AppState.account);

function openCreateKeepModal() {
  Modal.getOrCreateInstance("#create-update-keep").show();
}

function openUpdateVaultModal() {
  Modal.getOrCreateInstance("#create-update-vault").show();
}
</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-light bg-light justify-content-between px-5 py-2 shadow-sm mb-4">
    <div class="d-flex align-items-center">
      <div class="home-navlink px-3 py-1 bg-secondary rounded-pill">
        <router-link :to="{ name: 'Home' }">
          Home
        </router-link>
      </div>

      <div v-if="account" class="dropend">
        <button v-if="route.name !== 'Profile'" class="btn border-0 dropdown-toggle" type="button"
          data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li v-if="route.name === 'Account' || route.name === 'Home'"><button class="dropdown-item" type="button"
              @click="openCreateKeepModal()">New
              Keep</button>
          </li>
          <li v-if="route.name === 'Account'"><button class="dropdown-item" type="button"
              @click="openUpdateVaultModal()">New
              Vault</button></li>
        </ul>
      </div>
    </div>

    <router-link class="logo" :to="{ name: 'Home' }">
      Keepr
    </router-link>

    <div>
      <Login />
    </div>
  </nav>
</template>

<style scoped>
nav {
  position: sticky;
  top: 0;

  & a {
    text-decoration: none;
    color: azure;
  }
}

.logo {
  font-weight: 370;
  font-size: 2.1rem;
  background: linear-gradient(to right, #5850A5 30%, #3AB5CB 60%, #692DB4 90%);
  background-clip: text;
  color: transparent;
  background-size: 500% auto;
  animation: logoShine 5s ease-in-out infinite alternate;
}

@keyframes logoShine {
  0% {
    background-position: 0% 50%;
  }

  100% {
    background-position: 100% 50%;
  }
}

@media (width <=576px) {
  nav {
    & .home-navlink {
      display: none;
    }
  }
}
</style>
