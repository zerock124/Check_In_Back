import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import UserEditManagement from './UserEditManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(UserEditManagement))
}).$mount('#v_app');