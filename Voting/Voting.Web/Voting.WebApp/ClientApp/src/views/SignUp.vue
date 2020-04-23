<template>
  <div class="content small">
    <div class="header">
      <h2>Sign Up</h2>
    </div>
    <register />
  </div>
</template>

<script>
  import register from '../components/auth/Register.vue'

  export default {
    name: 'sign-up',
    components: {
      register
    }
  }
</script>
