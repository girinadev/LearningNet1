<template>

  <div>
    <div class="msg-error" v-if="showError">
      {{errorMessage}}
    </div>
    <div class="msg-loading" v-if="loading">Loading <font-awesome-icon icon="spinner"></font-awesome-icon></div>

    <form v-else action="#" @submit.prevent="saveQuestion">

      <div class="msg-error" v-if="errors.length">
        <b>Please fix errors:</b>
        <ul>
          <li v-for="error in errors" :key="error">{{ error }}</li>
        </ul>
      </div>

      <div>
        <div class="input-container">
          <textarea v-model="question.text" class="input-field" name="text" placeholder="Question text" rows="3"></textarea>
        </div>

        <div v-for="(answer, index) of question.answers"
             :key="answer.id"
             class="answer">
          <div class="input-container">
            <input v-model="question.answers[index].text" class="input-field" name="Answer" type="text" placeholder="Answer text" required />
            <input class="input-field" @click.prevent="deleteAnswer(answer)" type="button" value="Delete" />
          </div>
        </div>
        <div class="input-container">
          <input class="input-field" @click.prevent="addAnswer" type="button" value="Add Answer" />
        </div>


        <div class="input-container">
          <input v-model="question.type" type="radio" name="type" value="0" />
          <label>User can't add own answers</label>
          <input v-model="question.type" type="radio" name="type" value="1" />
          <label>User can add own answers</label>
        </div>

        <div class="input-container small">
          <label>Max votes count:</label>
          <input v-model="question.maxVoteCount" class="input-field" name="maxVoteCount" type="number" placeholder="Max votes count" required />
        </div>

        <div v-if="Number(question.type) === 1" class="input-container small">
          <label>Max answers count:</label>
          <input v-model="question.maxAnswersCount" class="input-field" name="maxAnswersCount" type="number" placeholder="Max answers count" />
        </div>

        <div class="input-container small">
          <label>Voting end date:</label>
          <input v-model="question.votingEndDate" class="input-field" name="votingEndDate" type="date" placeholder="Voting end date" required />
        </div>

        <div class="input-container">
          <input v-model="question.status" type="radio" name="status" value="0" />
          <label>Private</label>
          <input v-model="question.status" type="radio" name="status" value="1" />
          <label>Visible for all</label>
        </div>

      </div>

      <div class="footer">
        <input @click.prevent="saveQuestion" type="submit" value="Save" />
      </div>
    </form>
  </div>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'manage',
    props: {
      question: {}
    },
    data() {
      return {
        loading: true,
        showError: false,
        errorMessage: '',
        errors: []
      };
    },
    methods: {
      async fetchQuestion(id) {
        try {
          const response = await axios.get(`/api/questions/${id}`, { withCredentials: true });
          this.question = response.data;
        } catch (e) {
          if (e.response) {
            switch (e.response.status) {
              case 401:
              case 403:
                this.$router.push({ name: 'login' });
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
      async saveQuestion(e) {
        try {

          if (!this.checkForm(e)) return;

          this.question.type = Number(this.question.type);
          this.question.status = Number(this.question.status);
          this.question.maxVoteCount = Number(this.question.maxVoteCount);
          this.question.maxAnswersCount = Number(this.question.maxAnswersCount);
          this.question.votingEndDate = this.question.votingEndDate === '' ? null : this.question.votingEndDate;

          const response = this.question.id === null
            ? await axios.post('/api/questions', this.question, { withCredentials: true })
            : await axios.put(`/api/questions/${this.question.id}`, this.question, { withCredentials: true });
          this.question = response.data;

          this.$router.push({ name: 'question-details', params: { questionId: this.question.id } });
        } catch (e) {
          if (e.response) {
            switch (e.response.status) {
              case 400:
                for (let errKey in e.response.data) {
                  this.errors.push(e.response.data[errKey]);
                }
                break;
              case 401:
              case 403:
                this.$router.push({ name: 'login' });
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
      checkForm: function (e) {

        this.errors = [];

        if (!this.question.text) {
          this.errors.push('Question text is required.');
        }

        for (var answer of this.question.answers) {
          if (answer.text.trim() === '') {
            this.errors.push('Answer text is required.');
            break;
          }
        }        

        if (Number(this.question.status) === 1) {

          if (Number(this.question.maxVoteCount) <= 0) {
            this.errors.push('Max votes count must be greate than 0.');
          }

          if (this.question.answers.length < 2) {
            this.errors.push('Add at least two answers.');
          }

          if (Number(this.question.maxVoteCount) > this.question.answers.length) {
            this.errors.push('Max votes count must be less or equals answers count.');
          }

          if (!this.question.votingEndDate) {
            this.errors.push('Voting end date is required.');
          }

          if (Number(this.question.type) === 1) {
            if (Number(this.question.maxAnswersCount) <= 0) {
              this.errors.push('Max own answers count must be greate than 0.');
            }
            if (Number(this.question.maxAnswersCount) > Number(this.question.maxVoteCount)) {
              this.errors.push('Max own answers count must less or equals than max votes count.');
            }
          }
          else {
            this.question.maxAnswersCount = 0;
          }
        }        

        if (!this.errors.length) {
          return true;
        }

        e.preventDefault();
      },
      addAnswer: function () {
        this.question.answers.push({ text: '' });
      },
      deleteAnswer: function (answer) {
        const index = this.question.answers.indexOf(answer);
        if (index > -1) {
          this.question.answers.splice(index, 1);
        }
      }
    },
    async created() {
      if (this.$route.params.questionId)
        await this.fetchQuestion(this.$route.params.questionId);
      else {
        this.question = {
          id: null,
          text: '',
          status: 0,
          type: 0,
          maxVoteCount: 0,
          maxAnswersCount: 0,
          votingEndDate: null,
          answers: []
        };
        this.loading = false;
      }
    },
  }
</script>
