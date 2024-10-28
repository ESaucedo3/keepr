<script setup>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import { AuthService } from '../services/AuthService.js';

const identity = computed(() => AppState.identity)
const account = computed(() => AppState.account)
async function login() {
  AuthService.loginWithPopup()
}
async function logout() {
  AuthService.logout()
}

</script>

<template>
  <button class="btn btn-outline-dark rounded-pill" @click="login" v-if="!identity">
    Login
  </button>
  <div v-else>
    <div class="dropstart">
      <div type="button" class="border-0" data-bs-toggle="dropdown" aria-expanded="false">
        <div v-if="account?.picture || identity?.picture">
          <img class="account-img selectable" :src="account?.picture || identity?.picture" alt="account photo"
            height="66" />
        </div>
      </div>
      <div class="dropdown-menu dropdown-menu-sm-end dropdown-menu-start p-0" aria-labelledby="authDropdown">
        <div class="list-group">
          <router-link :to="{ name: 'Account' }">
            <div class="list-group-item dropdown-item list-group-item-action">
              Manage Account
            </div>
          </router-link>
          <div class="list-group-item dropdown-item list-group-item-action text-danger selectable" @click="logout">
            <i class="mdi mdi-logout"></i>
            logout
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="scss" scoped>
.account-img {
  aspect-ratio: 1/1;
  border-radius: 50%;
  box-shadow: 1px 1px 4px #333;
}
</style>
