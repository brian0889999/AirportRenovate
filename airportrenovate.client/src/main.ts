import './assets/main.css';

import { createApp } from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import router from './router';
import axios from 'axios';
import vuetify from './plugins/vuetify';
import '@mdi/font/css/materialdesignicons.css'; // �ޤJ �r��ϼ�

// �ШD�d�I��
axios.interceptors.request.use(config => {
    const jwtToken = localStorage.getItem('jwtToken');
    if (jwtToken) {
        config.headers.Authorization = `Bearer ${jwtToken}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

// �^���d�I��
axios.interceptors.response.use(response => {
    return response;
}, error => {
    if (error.response && error.response.status === 401) {
        // �p�G���� 401 ���~�A�M�����a��JWT Token
        localStorage.removeItem('jwtToken');
        router.push({ name: 'login' });
    }
});

const app = createApp(App);
const pinia = createPinia();


app.use(pinia)
    .use(router)
    .use(vuetify)
    .mount('#app');
/*createApp(App).mount('#app')*/
