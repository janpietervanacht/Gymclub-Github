import App from './App.vue';
import { createApp } from 'vue';
import router from './router.js';
import BaseButton from './components/ui/BaseButton.vue';
import BusyLoading from './components/ui/BusyLoading.vue';
import store from './store/index.js';  // main store met state, mutations, actions en getters
import globalMixinnie from './generalMixins/globalMixins.js';

const app = createApp(App);
app.mixin(globalMixinnie);
app.use(router);
app.use(store);

app.component('base-button', BaseButton);
app.component('busy-loading', BusyLoading);

app.mount('#app');
