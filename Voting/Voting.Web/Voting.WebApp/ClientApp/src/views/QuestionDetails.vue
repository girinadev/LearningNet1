<template>
  <div class="content">
    <div class="msg-error" v-if="showError">
      {{errorMessage}}
    </div>
    <div class="msg-loading" v-if="loading">Loading <font-awesome-icon icon="spinner"></font-awesome-icon></div>

    <div v-else>

      <div class="header with-info">
        <h2>{{question.text}}</h2>
        <div>Valid untill: {{ question.votingEndDate | date }}</div>
      </div>

      <results v-bind:question="question"
               class="aswers"></results>

      <div v-if="question.user.canVote === true" class="aswers">
        <h3>Answer options:</h3>
        <aswers v-bind:question="question"
                v-on:voted="onVoted"></aswers>
      </div>

    </div>
  </div>
</template>
<script>
  import axios from 'axios';
  import aswers from '../components/question/Answers.vue'
  import results from '../components/question/Results.vue'

  export default {
    name: 'question-details',
    components: {
      aswers,
      results
    },
    data() {
      return {
        loading: true,
        showError: false,
        errorMessage: 'Error while loading question.',
        question: {},
      };
    },
    methods: {
      async onVoted() {
        await this.fetchQuestion(this.question.id);
      },
      async fetchQuestion(id) {
        try {
          const response = await axios.get(`/api/questions/${id}`);
          this.question = response.data;

          if (this.question.user.canVote && this.question.status == 1 && this.question.type === 1) {
            for (var i = 0; i < this.question.maxAnswersCount; i++) {
              this.question.answers.push({ id: `_${i}`, text: '' });
            }
          }
        } catch (e) {

          if (e.response) {
            switch (e.response.status) {
              case 401:
              case 403:
                this.$router.push({ name: 'signin', params: { returnUrl: window.location.pathname } });
                break;
            }
          }
          else {
            this.showError = true;
            this.errorMessage = `Error while register: ${e.message}.`;
          }
        }

        this.loading = false;
      },
    },
    async created() {
      await this.fetchQuestion(this.$route.params.questionId);
    },
  }
</script>
