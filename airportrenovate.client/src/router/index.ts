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
        redirect: { name: 'PublicWorksGroup' },
        children: [
            {
                path: 'PublicWorksGroup',
                name: 'PublicWorksGroup',
                component: () => import(/*webpackChunkName "PublicWorksGroup" */ '@/views/main/PublicWorksGroup.vue')
            }, 
            {
                path: 'PrivilegeManagement',
                name: 'PrivilegeManagement',
                component: () => import(/*webpackChunkName "PrivilegeManagement" */ '@/views/main/PrivilegeManagement.vue')
            },  {
                path: 'Test2',
                name: 'Test2',
                component: () => import(/*webpackChunkName "Test2" */ '@/views/main/Test2.vue')
            },
        ]
    },
   
];

const router = createRouter({
    /* history: createWebHistory(),*/
    history: createWebHashHistory(),
    routes
});

export default router;
