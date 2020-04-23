<template>
  <div class="content">
    <div class="header">
      <h2>Questions</h2>
    </div>
    <questions isByUser="false" />
    <div class="footer">
    </div>
  </div>
</template>

<script>
  import questions from '../components/question/List.vue'

  export default {
    name: 'home',
    components: {
      questions
    }
  }
</script>
