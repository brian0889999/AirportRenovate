<template>
    
    <v-container style="width:100%; display:flex;">
        <v-data-table :headers="authheaders"
                      :items="processedLists"
                      item-key="name"
                      items-per-page="-1"
                      hide-default-footer
                      style="width: 100%;">
        </v-data-table>
        <v-data-table style="width:100%" :headers="headers"
                      :items="items"
                      :items-per-page="5"
                      class="elevation-1"
                      :hide-default-footer="true">
        </v-data-table>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref, computed } from 'vue';
import type { UsersDataModel } from '@/types/vueInterface';

    const authheaders = ref([
        { title: '姓名', align: 'start', sortable: false, key: 'name', value: 'name' },
        { title: '權限', key: 'status1', value: 'status1' },
        { title: '系統', key: 'status3', value: 'status3' },
    ]);
 
    const headers = ref([
        { title: '權限', align: 'start', sortable: false, key: '權限' },
        { title: '功能', key: '功能' },
        { title: '備註', key: '備註' },
    ]);
    
    const items = ref([
        { 權限: 'A', 功能: '新增、編輯、刪除、檢視、勻出入、復原刪除', 備註: '管理者' },
        { 權限: 'B', 功能: '檢視', 備註: '各單位窗口' },
        { 權限: 'C', 功能: '新增、編輯、刪除、檢視、勻出入', 備註: '工務組承辦' },
        { 權限: 'D', 功能: '', 備註: '帳號不開放' },
    ]);
    
    const lists = ref<UsersDataModel[]>([]);

    const fetchUsers = async () => {
        try {
            const url = '/api/Privilege'
            const response = await axios.get<UsersDataModel[]>(url);
            console.log(response.data);
            lists.value = response.data;
        }
        catch (error) {
            console.error(error);
        }
    }
    fetchUsers();
    const statusMapping: { [key: string]: string } = {
        'A': '土木',
        'B': '水電',
        'C': '建築',
        'D': '綜合',
        'E': '機械'
    };

    const processedLists = computed(() => {
        return lists.value.map(list => ({
            ...list,
            status3: statusMapping[list.status3 || ''] || list.status3
        }));
    });
</script>

<style scoped>
 
</style>