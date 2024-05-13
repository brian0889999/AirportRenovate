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
        path: '/main',
        name: 'main',
        component: () => import(/*webpackChunkName: "MainLayout" */ '@/layouts/MainLayout.vue'),   
        redirect: { name: 'EngineeringSearch' },
        children: [
            {
                path: 'EngineeringSearch',
                name: 'EngineeringSearch',
                component: () => import(/*webpackChunkName "EngineeringSearch" */ '@/views/main/EngineeringSearch.vue')
            }]
    },
   
];

const router = createRouter({
    /* history: createWebHistory(),*/
    history: createWebHashHistory(),
    routes
});

export default router;
