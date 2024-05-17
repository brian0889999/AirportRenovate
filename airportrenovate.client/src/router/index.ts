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
        redirect: { name: 'PrivilegeManagement' },
        children: [
            {
                path: 'PrivilegeManagement',
                name: 'PrivilegeManagement',
                component: () => import(/*webpackChunkName "PrivilegeManagement" */ '@/views/main/PrivilegeManagement.vue')
            }, {
                path: 'Test',
                name: 'Test',
                component: () => import(/*webpackChunkName "Test" */ '@/views/main/Test.vue')
            }]
    },
   
];

const router = createRouter({
    /* history: createWebHistory(),*/
    history: createWebHashHistory(),
    routes
});

export default router;
