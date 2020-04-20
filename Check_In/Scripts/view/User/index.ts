import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import UserManagement from './UserManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(UserManagement))
}).$mount('#v_app');