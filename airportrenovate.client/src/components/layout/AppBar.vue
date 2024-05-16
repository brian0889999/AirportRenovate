<template>
    <v-app-bar color="primary"
               prominent
               elevation="0">
        <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
        <v-spacer />
        <v-spacer />
        <v-spacer />
        <v-spacer />
        <v-spacer />
        <v-spacer />
        <v-toolbar-title>經費管理/歡迎</v-toolbar-title>



        <v-btn>首頁</v-btn>
        <v-btn @click="logout">登出</v-btn>

    </v-app-bar>
</template>

<script setup lang="ts">
    import { computed, onMounted } from 'vue';
    import { useStore } from '@/store/index';
    import { useRouter } from 'vue-router';
    const router = useRouter();
    const logout = () => {

        if (confirm("確定要登出嗎?")) {
            router.push('/login');
        } else {
            return false;
        }

    }
    const store = useStore();

    const drawer = computed(() => {
        return store.drawer;
    })

    const rail = computed(() => {
        return store.rail;
    })
</script>

<style scoped>

</style>