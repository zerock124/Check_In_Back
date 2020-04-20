import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import TotalRecordManagement from './TotalRecordManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(TotalRecordManagement))
}).$mount('#v_app');