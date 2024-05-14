<template>
    <v-navigation-drawer app
                         clipped>
        <v-list-item title="My Application" subtitle="Vuetify"></v-list-item>
        <v-divider></v-divider>
        <v-list-item link title="List Item 1"></v-list-item>
        <v-list-item link title="List Item 2"></v-list-item>
        <v-list-item link title="List Item 3"></v-list-item>
    </v-navigation-drawer>
</template>

<script>
    export default {
        data: () => ({
            drawer: false,
            group: null,
            items: [
                {
                    title: 'Foo',
                    value: 'foo',
                },
                {
                    title: 'Bar',
                    value: 'bar',
                },
                {
                    title: 'Fizz',
                    value: 'fizz',
                },
                {
                    title: 'Buzz',
                    value: 'buzz',
                },
            ],
        }),

        watch: {
            group() {
                this.drawer = false
            },
        },
    }
</script>

<!--<script setup lang="ts">

</script>-->

<style scoped>
</style>