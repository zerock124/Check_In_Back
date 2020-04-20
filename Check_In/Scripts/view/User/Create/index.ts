import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import AuthorityCreateManagement from './UserCreateManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(AuthorityCreateManagement))
}).$mount('#v_app');