<template>
  <!--temporary-->
    <!--:location="$vuetify.display.mobile ? 'bottom' : undefined"-->
    <v-navigation-drawer v-model="drawer"
                         :rail="rail"
                         expand-on-hover>
        <v-list density="compact" nav>
            <AppDrawerItem v-for="item in drawerItems"
                           :item="item" />
        </v-list>
    </v-navigation-drawer>
</template>
<script setup lang="ts">
    import { ref, computed, watch } from 'vue';
    import { useStore } from '@/store/index';
    import { useRouter, type RouteRecordRaw } from 'vue-router';
    import type { Crumb } from '@/types/vueInterface';
    import AppDrawerItem from './AppDrawerItem.vue';

    const store = useStore();
    const router = useRouter();

    const rail = computed(() => store.rail);
const drawer = computed({
    get: () => store.drawer,
    set: (val: boolean) => store.setDrawer(val)
});

    function convertRoutesToSidebarItems(routes: any[], parentPath: string): Crumb[] {
        return routes.map(route => {
            const item: Crumb = {
                title: route.meta?.title || '',
                href: `${parentPath}/${route.path}` || '',
            };

            if (route.children) {
                item.childs = convertRoutesToSidebarItems(route.children, `${parentPath}/${route.path}`);
            }
            return item;
        })
    }

    const mainRoute: RouteRecordRaw | undefined = router.options.routes.find(route => route.path === '/main');
    const sidebarItems: Crumb[] = mainRoute ? convertRoutesToSidebarItems(mainRoute.children || [], '/main') : [];

    const drawerItems = ref(sidebarItems);
</script>

<style scoped>
    .v-navigation-drawer__scrim {
        display: none;
    }
</style>
