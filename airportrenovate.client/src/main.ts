import './assets/main.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import router from './router';
/*import axios from 'axios';*/
import vuetify from './plugins/vuetify';
/*import VueCookies from 'vue3-cookies';*/

const app = createApp(App)
const pinia = createPinia();


app.use(pinia)
    .use(router)
    .use(vuetify)
/*    .use(VueCookies)*/
    .mount('#app');
/*createApp(App).mount('#app')*/
