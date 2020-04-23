<template>
  <div class="content small">
    <div class="header">
      <h2>Sign In</h2>
    </div>
    <login v-on:auth-change="onAuthChange" />
  </div>
</template>

<script>
  import login from '../components/auth/Login.vue'

  export default {
    name: 'sign-in',
    components: {
      login
    },
    methods: {
      onAuthChange(user) {
        this.$emit('auth-change', user);
      }
    }
  }
</script>
