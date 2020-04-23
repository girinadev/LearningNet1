<template>
  <div>
    <div class="msg-error" v-if="showError">
      {{errorMessage}}
    </div>
    <div class="msg-loading" v-if="loading">Loading <font-awesome-icon icon="spinner"></font-awesome-icon></div>

    <form v-else action="#" @submit.prevent="register">
     
      <div class="msg-error" v-if="errors.length">
        <b>Please fix errors:</b>
        <ul>
          <li v-for="error in errors" :key="error">{{ error }}</li>
        </ul>
      </div>

      <div class="input-container">
        <input v-model="firstName" class="input-field" name="firstName" type="text" placeholder="First name" required />
      </div>
      <div class="input-container">
        <input v-model="lastName" class="input-field" name="lastName" type="text" placeholder="Last name" required />
      </div>
      <div class="input-container">
        <input v-model="email" class="input-field" name="email" type="email" placeholder="E-mail" required />
      </div>
      <div class="input-container">
        <input v-model="password" class="input-field" name="password" type="password" placeholder="Password" required />
      </div>
      <div class="input-container">
        <input v-model="confirmPassword" class="input-field" name="confirmPassword" type="password" placeholder="Confirm password" required />
      </div>

      <div class="footer">
        <input @click.prevent="register" type="submit" value="Register" />
      </div>

    </form>

  </div>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'register',
    data() {
      return {
        loading: false,
        showError: false,
        errorMessage: 'Error while sign up.',
        errors: [],
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        confirmPassword: ''
      };
    },
    methods: {
      async register() {
        this.loading = true;
        this.errors = [];
        try {
          const requestData = {
            firstName: this.firstName,
            lastName: this.lastName,
            email: this.email,
            password: this.password,
            confirmPassword: this.confirmPassword
          };

          await axios.post('/api/account/register', requestData);          
          this.$router.push({ name: 'signin' });
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
    },
    async created() {
    },
  }
</script>
