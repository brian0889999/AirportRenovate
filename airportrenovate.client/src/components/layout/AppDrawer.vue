<template>
  <!--temporary-->
    <!--:location="$vuetify.display.mobile ? 'bottom' : undefined"-->
    <v-navigation-drawer v-model="drawer"
                         :rail="rail"
                          expand-on-hover>
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
    const rail = computed(() => store.rail);
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
