import Vue from 'vue'

Vue.filter('status', value => {
  switch (value) {
    case 0: return 'Private';
    case 1: return 'Public';
    case 2: return 'Active';
    case 3: return 'NotActive';
  }

  return status;
});
