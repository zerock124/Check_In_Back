var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
define(["require", "exports", "vue", "bootstrap-vue", "./UserCreateManagement"], function (require, exports, vue_1, bootstrap_vue_1, UserCreateManagement_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    vue_1 = __importDefault(vue_1);
    bootstrap_vue_1 = __importDefault(bootstrap_vue_1);
    UserCreateManagement_1 = __importDefault(UserCreateManagement_1);
    vue_1.default.use(bootstrap_vue_1.default);
    new vue_1.default({
        render: (function (h) { return h(UserCreateManagement_1.default); })
    }).$mount('#v_app');
});
//# sourceMappingURL=index.js.map