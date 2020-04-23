<template>
  <div>
    <div v-for="(answer) of question.answers"
         :key="answer.id"
         class="answer results">

      <div  v-if="!answer.id.startsWith('_')" class="result">
        <div>{{ answer.text }}</div>
        <div>
          <div class="progress-bar">
            <div class="progress-bar-value" v-bind:style="{ width: getPercent(answer.total) }"></div>
          </div>
          <div class="totals">
            <div>{{ answer.total }}</div>
            <div>{{ getPercent(answer.total) }}</div>
          </div>
        </div>
      </div>

      <div  v-if="!answer.id.startsWith('_')" class="votes">
        <div v-for="(vote) of answer.votes"
             :key="vote.id">
          <span>{{ vote.user.firstName }} {{vote.user.lastName}}</span>
        </div>
      </div>

    </div>

    <div v-if="question.user.canVote !== true" class="footer">
      <div>Total votes count: {{question.total}}</div>
    </div>
  </div>
</template>
<script>
  export default {
    name: 'question-results',
    props: {
      question: {}
    },
    methods: {
      getPercent(totalByAnswer) {
        const p = totalByAnswer * 100 / (this.question.total ? this.question.total : 1);
        return Math.round(p * 100) / 100 + '%';
      }
    }
  }
</script>
