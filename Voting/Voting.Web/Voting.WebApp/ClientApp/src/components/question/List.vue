<template>
  <div v-if="showError">
    <p>We're sorry, we're not able to retrieve this information at the moment, please try back later. {{errorMessage}}</p>
  </div>
  <div v-else>
    <div class="filter">
      <div v-if="isByUser === 'true'" class="input-container">
        <select v-model="searchFilter.status" @change="fetchQuestions($event)" class="input-field">
          <option value="">Any</option>
          <option value="0">Private</option>
          <option value="1">Visible for all</option>
          <option value="2">Active</option>
          <option value="3">Not Active</option>
        </select>
      </div>
      <div v-if="isByUser !== 'true'" class="input-container">
        <select v-model="searchFilter.isVoted" @change="fetchQuestions($event)" class="input-field">
          <option value="">Any</option>
          <option value="false">Not Voted</option>
          <option value="true">Voted</option>
        </select>
      </div>
      <div class="input-container">
        <input v-model="searchFilter.votingEndDate" @change="fetchQuestions($event)" class="input-field" name="searchFilter.votingEndDate" type="date" placeholder="Voting end date" />
      </div>
    </div>

    <div class="questions">
      <div v-if="loading" class="msg-loading">Loading <font-awesome-icon icon="spinner"></font-awesome-icon></div>
      <div v-else>
        <div v-for="(question) of questions"
             :key="question.id"
             class="question">

          <router-link class="text" :to="{ name: 'question-details', params: { questionId: question.id }}">{{ question.text }}</router-link>

          <div v-if="isByUser === 'true' && question.status === 0" class="edit-icons">
            <router-link :to="{ name: 'question-manage', params: { questionId: question.id }}" alt="Manage"><font-awesome-icon icon="edit"></font-awesome-icon></router-link>
            | <a href="" @click.prevent="deleteQuestion(question.id)" alt="Delete"><font-awesome-icon icon="trash"></font-awesome-icon></a>
          </div>

          <div v-if="isByUser !== 'true'">
            {{ question.user.firstName  }} {{ question.user.lastName }}
          </div>

          <div v-if="question.votingEndDate">{{ question.votingEndDate | date }}</div>
          <div v-else-if="question.updatedDate">{{ question.updatedDate | date }}</div>
          <div v-else>{{ question.createdDate | date }}</div>

        </div>
        <div class="question" v-if="questions.length === 0">No results</div>
      </div>
    </div>

  </div>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'questions',
    props: {
      isByUser: String,
    },
    data() {
      return {
        loading: true,
        showError: false,
        errorMessage: 'Error while loading questions.',
        questions: {},
        searchFilter: {
          status: '',
          votingEndDate: '',
          isVoted: ''
        },
      };
    },
    methods: {
      async fetchQuestions() {
        try {
          let route = this.isByUser === 'true' ? '/my' : '';

          let query = '';
          if (this.searchFilter.status)
            query += `?status=${this.searchFilter.status}`;
          if (this.searchFilter.votingEndDate)
            query += (query === '' ? '?' : '&') + `votingEndDate=${this.searchFilter.votingEndDate}`;
          if (this.searchFilter.isVoted)
            query += (query === '' ? '?' : '&') + `isVoted=${this.searchFilter.isVoted}`;

          const response = await axios.get(`/api/questions${route}${query}`);
          this.questions = response.data;
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
      async deleteQuestion(id) {
        try {
          if (!confirm('Delete question?'))
            return;
          await axios.delete(`/api/questions/${id}`);
          await this.fetchQuestions();
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
      }
    },
    async created() {
      if (this.isByUser !== 'true') {
        this.searchFilter.status = 1;
      }

      await this.fetchQuestions();
    }
  }
</script>
