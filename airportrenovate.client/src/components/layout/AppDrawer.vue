<template>
  <!--temporary-->
    <!--:location="$vuetify.display.mobile ? 'bottom' : undefined"-->
    <v-navigation-drawer v-model="drawer"
                         :rail="rail"
                         expand-on-hover>
        <v-list density="compact" nav>
            <AppDrawerItem v-for="item in drawerItems"
                           :key="item.href"
                           :item="item" />
        </v-list>
    </v-navigation-drawer>
</template>
<script setup lang="ts">
    import { ref, computed, watch, onMounted } from 'vue';
    import { useStore } from '@/store/index';
    import { useRouter, type RouteRecordRaw } from 'vue-router';
    import type { Crumb } from '@/types/vueInterface';
    import AppDrawerItem from './AppDrawerItem.vue';
    import type { UserViewModel } from '@/types/apiInterface';
    import { type ApiResponse, get } from '@/services/api';

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

    const defaultUser: UserViewModel = {
        No: 0,
        Name: '',
        Account: '',
        Password: '',
        Email: '',
        Unit_No: '',
        Auth: '',
        Account_Open: '',
        Reason: '',
        Count: 0,
        Time: new Date(),
        Time1: new Date(),
        Status1: '',
        Status2: '',
        MEMO: '',
        Status3: '',
    };
    const user = ref<UserViewModel>(defaultUser); 
    const getCurrentUser = async () => {
        const url = '/api/User/Current';
        try {
            const response: ApiResponse<UserViewModel> = await get<UserViewModel>(url);
            if (response.StatusCode === 200) {
                const data = response.Data;
                user.value = data ? data : defaultUser;
                if (user.value.Status1 === 'A') {
                    drawerItems.value = sidebarItems;
                } else {
                    drawerItems.value = sidebarItems.filter(item => item.href === '/main/PublicWorksGroup');
                }
            }
            else {
                console.error(response.Data ?? response.Message);
            }
        }
        catch (error: any) {
            console.error(error.message);
        }
    };

    const filteredDrawerItems = computed(() => {
        if (user.value.Status1 !== 'A') {
            return [{ title: 'ВеЋЧ', href: '/main/PublicWorksGroup' }];
        }
        return drawerItems.value;
    });

    onMounted(async () => {
        await getCurrentUser();
    });
</script>

<style scoped>
    .v-navigation-drawer__scrim {
        display: none;
    }
</style>
