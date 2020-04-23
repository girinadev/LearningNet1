import Vue from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import 'core-js/stable';
import router from './router';
import dateFilter from './filters/date.filter';
import statusFilter from './filters/status.filter';
import { library } from '@fortawesome/fontawesome-svg-core'
import { faSpinner, faTrash, faEdit } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.config.productionTip = false;

Vue.use(VueAxios, axios);

library.add(faSpinner, faTrash, faEdit);
Vue.component('font-awesome-icon', FontAwesomeIcon);

new Vue({
  dateFilter,
  statusFilter,
  router,
  axios,
  render: h => h(App),
}).$mount('#app')
