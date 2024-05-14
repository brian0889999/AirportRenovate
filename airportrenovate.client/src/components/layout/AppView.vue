<template>
    <v-main>
        <v-container>
            <router-view v-slot="{ Component }">
                <component :is="Component" />
            </router-view>
        </v-container>
    </v-main>
</template>

<script setup lang="ts">

</script>

<style scoped>
</style>