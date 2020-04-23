<template>
  <form action="#" @submit.prevent="addVote">

    <div v-for="(answer) of question.answers"
         :key="answer.id"
         class="answer">

      <input v-if="question.maxVoteCount === 1" type="radio"
             name="sectedAnswers"
             :id="answer.id"
             :value="answer"
             :disabled="question.status !== 1"
             v-model="sectedAnswers" />

      <input v-else type="checkbox"
             name="sectedAnswers"
             :id="answer.id"
             :value="answer"
             :disabled="question.status !== 1"
             v-model="sectedAnswers" />

      <label v-if="!answer.id.startsWith('_')">{{ answer.text }}</label>

      <div v-else class="input-wrapper">
        <input v-model="answer.text" class="input-field" name="answerText" type="text" placeholder="Own answer" />
        <label>{{answer.error}}</label>
      </div>

    </div>
    <div class="footer">
      <input v-if="question.status === 1 && question.user.canVote === true" @click.prevent="addVote" type="submit" value="Vote" />
    </div>
  </form>
</template>
<script>
  import axios from 'axios';

  export default {
    name: 'question-aswers',
    props: {
      question: {}
    },
    data() {
      return {
        sectedAnswers: []
      }
    },
    methods: {
      async addVote() {
        try {

          if (!this.sectedAnswers.length)
            this.sectedAnswers = [this.sectedAnswers];

          let isValid = true;
          if (this.sectedAnswers.length < 0)
            return;

          let requestDataArr = [];
          for (let answer of this.sectedAnswers) {
            let requestData = { QuestionId: this.question.id };

            if (!answer.id.startsWith('_')) {
              requestData.answerId = answer.id;
              requestDataArr.push(requestData);
            }
            else {
              if (answer.text.trim() === '') {
                this.question.answers.find(a => a.id === answer.id).error = 'Text is required';
                var t = this.question.answers;
                this.question.answers = [];
                this.question.answers = t;
                isValid = false;
              }
              else {
                requestData.answerText = answer.text;
                requestDataArr.push(requestData);
              }
            }
          }

          if (isValid) {
            for (let requestData of requestDataArr) {
              await axios.post('/api/votes', requestData, { withCredentials: true });
            }

            this.$emit('voted');
          }
        } catch (e) {
          if (e.response) {
            switch (e.response.status) {
              case 400:
                this.showError = true;
                this.errorMessage = e.response.data;
                break;
              case 401:
              case 403:
                this.$router.push({ name: 'login' });
                break;
            }
          }
          else {
            this.showError = true;
            this.errorMessage = `Error while add vote: ${e.message}.`;
          }
        }
        this.loading = false;
      }
    }
  }
</script>
