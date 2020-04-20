import Vue from 'vue';
import BootstrapVue from 'bootstrap-vue';
import CheckRecordManagement from './CheckRecordManagement';

Vue.use(BootstrapVue);

new Vue({
    render: (h => h(CheckRecordManagement))
}).$mount('#v_app');