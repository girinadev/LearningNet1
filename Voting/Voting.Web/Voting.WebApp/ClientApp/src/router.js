import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('./views/Home.vue'),
    },
    {
      path: '/signin',
      name: 'signin',
      component: () => import('./views/SignIn.vue'),
    },
    {
      path: '/signup',
      name: 'signup',
      component: () => import('./views/SignUp.vue'),
    },
    {
      path: '/questions',
      name: 'questions',
      component: () => import('./views/Home.vue'),
    },
    {
      path: '/questions/my',
      name: 'question-my',
      component: () => import('./views/MyQuestions.vue'),
    },
    {
      path: '/questions/add',
      name: 'question-add',
      component: () => import('./views/QuestionManage.vue'),
    },
    {
      path: '/questions/:questionId',
      name: 'question-details',
      component: () => import('./views/QuestionDetails.vue'),
    },
    {
      path: '/questions/manage/:questionId',
      name: 'question-manage',
      component: () => import('./views/QuestionManage.vue'),
    },
  ],
});
//# sourceMappingURL=router.js.map