<template>
  <div class="content">
    <div class="header">
      <h2>My Questions</h2>
    </div>
    <questions isByUser="true" />
    <div class="footer">
      <router-link to="/questions/add">Add Question</router-link>
    </div>
  </div>
</template>

<script>
  import questions from '../components/question/List.vue'

  export default {
    name: 'my-questions',
    components: {
      questions
    }
  }
</script>
