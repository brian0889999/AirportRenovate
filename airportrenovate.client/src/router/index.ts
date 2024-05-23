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
        component: () => import(/*webpackChunkName: "login" */ '@/views/Login.vue'),
        meta: { title: '登入' },
    },
    {
        path: '/main',
        name: 'main',
        redirect: { name: 'PublicWorksGroup' },
        component: () => import(/*webpackChunkName: "MainLayout" */ '@/layouts/MainLayout.vue'),   
        meta: { title: '首頁' },
        children: [
            {
                path: 'PublicWorksGroup',
                name: 'PublicWorksGroup',
                component: () => import(/*webpackChunkName "PublicWorksGroup" */ '@/views/main/PublicWorksGroup.vue'),
                meta: { title: '工務組' },
            }, 
            {
                path: 'PrivilegeManagement',
                name: 'PrivilegeManagement',
                component: () => import(/*webpackChunkName "PrivilegeManagement" */ '@/views/main/PrivilegeManagement.vue'),
                meta: { title: '權限管理' },
            },  {
                path: 'Test2',
                name: 'Test2',
                component: () => import(/*webpackChunkName "Test2" */ '@/views/main/Test2.vue'),
                meta: { title: 'Test2' },
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
