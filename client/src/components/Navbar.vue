<script setup>
import Login from './Login.vue';

</script>

<template>
  <nav class="navbar navbar-expand-sm navbar-light bg-light justify-content-between px-5 py-2 shadow-sm mb-4">
    <div class="d-flex align-items-center">
      <div class="home-navlink px-3 py-1 bg-secondary rounded-pill">
        <router-link :to="{ name: 'Home' }">
          Home
        </router-link>
      </div>

      <div class="dropend">
        <button class="btn border-0 dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-expanded="false">
          Create
        </button>
        <ul class="dropdown-menu">
          <li><button class="dropdown-item" type="button" data-bs-toggle="modal" data-bs-target="#new-keep">New
              Keep</button>
          </li>
          <li><button class="dropdown-item" type="button" data-bs-toggle="modal" data-bs-target="#new-vault">New
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
  font-size: 1.5rem;
  color: #333;
}

@media (width <=576px) {
  nav {
    & .home-navlink {
      display: none;
    }
  }
}
</style>
