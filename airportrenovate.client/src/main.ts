import './assets/main.css';

import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
/*import axios from 'axios';*/
import vuetify from './plugins/vuetify';




createApp(App).use(router).use(vuetify).mount('#app');
/*createApp(App).mount('#app')*/
