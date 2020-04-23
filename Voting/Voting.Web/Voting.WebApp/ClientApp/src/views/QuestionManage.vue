<template>
  <div class="content">

    <div class="header">
      <h2>Manage Question</h2>
    </div>
    
    <manage></manage>
  </div>
</template>
<script>
  import manage from '../components/question/Manage.vue'

  export default {
    name: 'question-manage',
    components: {
      manage
    }
  }
</script>
