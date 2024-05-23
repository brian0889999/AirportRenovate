<template>
    
    <v-container style="width:100%; display:flex;">
        <v-data-table v-if="!isEditing"
                      :headers="authheaders"
                      :items="processedLists"
                      item-key="name"
                      items-per-page="-1"
                      hide-default-footer
                      style="width: 100%;">
            <template v-slot:[`item.edit`]="{ item }">
                <v-btn @click="editItem(item)" icon class="small-btn">
                    <v-icon>mdi-pencil</v-icon>
                </v-btn>
            </template>
        </v-data-table>

        <v-data-table v-if="!isEditing"
                      style="width:100%" 
                      :headers="headers"
                      :items="items"
                      :items-per-page="5"
                      class="elevation-1"
                      :hide-default-footer="true">
        </v-data-table>

    <div v-if="isEditing" style="width: 100%;">
        <v-form>
            <v-text-field v-model="currentItem.name" label="姓名"></v-text-field>
            <v-select v-model="currentItem.status1" :items="Object.keys(statusMapping)" label="權限"></v-select>
            <v-select v-model="currentItem.status3" :items="Object.keys(statusMapping)" label="系統"></v-select>
            <v-text-field v-model="currentItem.account" label="帳號"></v-text-field>
            <v-text-field v-model="currentItem.password" label="密碼"></v-text-field>
            <v-btn @click="saveItem" color="primary" class="mr-2" size="large">保存</v-btn>
            <v-btn @click="cancelEdit" color="primary" size="large">取消</v-btn>
        </v-form>
    </div>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref, computed } from 'vue';
import type { UserDataModel } from '@/types/vueInterface';

  
 
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

    const authheaders = ref([
        { title: '姓名', align: 'start', sortable: false, key: 'name', value: 'name' },
        { title: '權限', key: 'status1', value: 'status1' },
        { title: '系統', key: 'status3', value: 'status3' },
        { title: '編輯', key: 'edit', value: 'edit', sortable: false },
    ]);

    const lists = ref<UserDataModel[]>([]);
    const isEditing = ref(false);
    const currentItem = ref<UserDataModel | null>(null);

    const fetchUsers = async () => {
        try {
            const url = '/api/Privilege'
            const response = await axios.get<UserDataModel[]>(url);
      /*      console.log(response.data);*/
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

    const editItem = (item: UserDataModel) => {
        // 加入編輯邏輯
        console.log('Edit item:', item);
        isEditing.value = true;
        currentItem.value = { ...item };
    };

    const saveItem = () => {
        // 保存邏輯

        fetchUsers();
        isEditing.value = false;
    };

    const cancelEdit = () => {
        isEditing.value = false;
        currentItem.value = null;
    };
</script>

<style scoped>
 .small-btn {
     transform: scale(0.8);
 }
</style>