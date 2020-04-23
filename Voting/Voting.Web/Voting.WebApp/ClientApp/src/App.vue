<template>
  <div id="app" class="app-container">
    <div class="user-info" v-if="isAuthorize"><div>{{user.firstName}} {{user.lastName}}</div></div>
    <div class="menu">
      <router-link to="/">Home</router-link>
      <router-link v-if="isAuthorize" to="/questions/my">My Questions</router-link>
      <router-link v-if="!isAuthorize" to="/signin">Login</router-link>
      <router-link v-if="!isAuthorize" to="/signup">Register</router-link>
      <logout v-if="isAuthorize" v-on:auth-change="onAuthChange" />
    </div>
    <div class="content-wrapper">
      <router-view v-on:auth-change="onAuthChange"></router-view>
    </div>
  </div>
</template>

<script>
  import logout from './components/auth/Logout'

  export default {
    name: 'app',
    components: {
      logout,
    },
    data() {
      return {
        user: null
      }
    },
    computed: {
      isAuthorize() {
        return this.user != null;
      }
    },
    methods: {
      onAuthChange(user) {
        /* eslint-disable no-console */
        //console.log(user);
        /* eslint-enable no-console */
        this.user = user;
        if (this.user != null) {
          localStorage.user = JSON.stringify(user);
        }
        else {
          localStorage.removeItem('user');
        }
      }
    },
    created() {
      if (localStorage.user) {
        this.user = JSON.parse(localStorage.user);
      }
    },
  }
</script>

<style>
  @import './assets/styles.css';
</style>
