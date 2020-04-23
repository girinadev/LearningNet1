<template>
  <div>
    <div class="msg-error" v-if="showError">
      {{errorMessage}}
    </div>
    <div class="msg-loading" v-if="loading">Loading <font-awesome-icon icon="spinner"></font-awesome-icon></div>

    <form v-else action="#" @submit.prevent="login">

      <div class="msg-error" v-if="errors.length">
        <b>Please fix errors:</b>
        <ul>
          <li v-for="error in errors" :key="error">{{ error }}</li>
        </ul>
      </div>
      
      <div class="input-container">        
        <input v-model="email" class="input-field" name="email" type="email" placeholder="E-mail" required />
      </div>

      <div class="input-container">
        <input v-model="password" class="input-field" name="password" type="password" placeholder="Password" required />
      </div>

      <div class="footer">
        <input @click.prevent="login" type="submit" value="Login" />
      </div>

    </form>
  </div>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'login',
    data() {
      return {
        loading: false,
        showError: false,
        errorMessage: 'Error while sign in.',
        errors: [],
        email: 'test@test.com',
        password: 'qwe123',
        self: null
      };
    },
    methods: {
      async login() {
        this.loading = false;
        this.errors = [];
        try {
          const requestData = {
            email: this.email,
            password: this.password
          };
          const response = await axios.post('/api/account/login', requestData);
          this.$emit('auth-change', response.data);
          if (this.$route.params.returnUrl)
            this.$router.push({ path: this.$route.params.returnUrl });
          else
            this.$router.push({ name: 'home' });
        } catch (e) {
          if (e.response) {
            switch (e.response.status) {
              case 400:
                if (e.response.data.errors) {
                  for (let errKey in e.response.data.errors) {
                    this.errors.push(e.response.data.errors[errKey]);
                  }
                } else {
                  this.errors.push(e.response.data);
                }
                break;
            }
          }
          else {
            this.showError = true;
            this.errorMessage = `Error while register: ${e.message}.`;
          }
        }

        this.loading = false;
      }
    }
  }
</script>
