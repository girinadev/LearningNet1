<template>
  <a href="" @click.prevent="logout">Logout</a>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'logout',
    data() {
      return {};
    },
    methods: {
      async logout() {
        try {
          await axios.post('/api/account/logout');
          this.$emit('auth-change', null);
          this.$router.push({ name: 'signin' });
        } catch (e) {
          if (e.response) {
            switch (e.response.status) {
              case 401:
              case 403:
                this.$emit('auth-change', null);
                this.$router.push({ name: 'signin' });
                break;
            }
          }
          else {
            this.showError = true;
            this.errorMessage = `Error while register: ${e.message}.`;
          }
        }
      }
    }
  }
</script>
