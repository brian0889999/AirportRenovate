// router.ts

import { createRouter, createWebHashHistory, type RouteRecordRaw } from 'vue-router';



const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        redirect: {name: 'login'},
    },
    {
        path: '/login',
        name: 'login',
        component: () => import(/*webpackChunkName: "login" */ '@/views/Login.vue')
    },
    {
        path: '/HelloWorld',
        name: 'HelloWorld',
        component: () => import(/*webpackChunkName */ '@/components/HelloWorld.vue')
    }
];

const router = createRouter({
    /* history: createWebHistory(),*/
    history: createWebHashHistory(),
    routes
});

export default router;
