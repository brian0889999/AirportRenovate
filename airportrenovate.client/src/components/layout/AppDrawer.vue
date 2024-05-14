<template>

    <!--<v-app-bar color="primary"
               prominent>
        <v-app-bar-nav-icon variant="text" @click.stop="drawer = !drawer"></v-app-bar-nav-icon>

        <v-toolbar-title>My files</v-toolbar-title>

        <v-spacer></v-spacer>

        <template v-if="$vuetify.display.mdAndUp">
            <v-btn icon="mdi-magnify" variant="text"></v-btn>

            <v-btn icon="mdi-filter" variant="text"></v-btn>
        </template>

        <v-btn icon="mdi-dots-vertical" variant="text"></v-btn>
    </v-app-bar>-->
    <!--temporary-->
    <v-navigation-drawer v-model="drawer"
                         :location="$vuetify.display.mobile ? 'bottom' : undefined"
                         :rail="rail">
        <v-list :items="items"></v-list>
    </v-navigation-drawer>

    <!--<v-main style="height: 500px;">
        <v-card-text>
            The navigation drawer will appear from the bottom on smaller size screens.
        </v-card-text>
    </v-main>-->

</template>
<script setup lang="ts">
    import { ref, computed, watch } from 'vue';
    import { useStore } from '@/store/index';

/*const drawer = ref(false);*/

    const store = useStore();
const drawer = computed({
    get: () => store.drawer,
    set: (val: boolean) => store.setDrawer(val)
});
/*const group = ref<string | null>(null);*/
const items = [
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
];

//watch(group, () => {
//  drawer.value = false;
//});
</script>

<style scoped>
    .v-navigation-drawer__scrim {
        display: none;
    }
</style>
