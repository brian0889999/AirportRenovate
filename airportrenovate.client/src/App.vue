<template>
    <v-app>  <!-- vuetify最重要的組件 v-app 所有組件及功能的進入點,v-app限制只能被渲染一次,必須是所有組件的祖先節點 -->
        <router-view />
    </v-app>
</template>


<style>
    html{
        overflow:auto !important;
    }
</style>