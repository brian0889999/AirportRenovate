<!--<template>
    <v-app-bar :elevation="2">
        <template v-slot:prepend>
            <v-app-bar-nav-icon ></v-app-bar-nav-icon>
        </template>

        <v-app-bar-title>Application Bar</v-app-bar-title>
        <v-btn>首頁</v-btn>
        <v-btn @click="logout">登出</v-btn>
    </v-app-bar>
</template>

<script setup lang="ts">
    import { useRouter } from 'vue-router';
    const router = useRouter();
    const logout = () => {
   
    if( confirm("確定要登出嗎?")) {
    router.push('/login');
    } else {
    return false;
    }
    
    }
</script>

<style scoped>

</style>-->