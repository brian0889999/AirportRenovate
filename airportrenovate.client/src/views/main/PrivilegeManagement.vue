<template>
    
    <v-container style="width:100%; display:flex;">
        <v-row>
            <v-col cols="12" sm="8" md="6">
                <v-btn v-if="!isEditing" @click="addItem" color="primary" class="mb-4">新增</v-btn>
                <v-data-table v-if="!isEditing"
                              :headers="authheaders"
                              :items="processedLists"
                              item-key="name"
                              items-per-page="12"
                              class="elevation-1"
                              style="width: 100%;">
                    <template v-slot:[`item.edit`]="{ item }">
                        <v-btn @click="editItem(item)" icon class="small-btn">
                            <v-icon>mdi-pencil</v-icon>
                        </v-btn>
                    </template>
                </v-data-table>

            </v-col>
            <v-col cols="12" sm="8" md="6">
                <!--寫死的Table-->
                <v-data-table v-if="!isEditing"
                              style="width:100%"
                              :headers="headers"
                              :items="items"
                              :items-per-page="-1"
                              class="elevation-1"
                              :hide-default-footer="true">
                </v-data-table>
            </v-col>
        </v-row>
    <div v-if="isEditing" style="width: 100%;">
        <v-form>
            <v-text-field v-model="currentItem.name" label="姓名" :readonly="isEditMode"></v-text-field>
            <v-select v-model="currentItem.status1"
                      :items="['A', 'B', 'C', 'D']"
                      label="權限"></v-select>
            <v-select v-model="currentItem.auth" 
                      :items="Object.keys(ReverseAuthMapping)" 
                      label="組室"></v-select>
            <v-select v-model="currentItem.status3"
                      :items="Object.keys(reverseStatusMapping)" 
                      label="系統"></v-select>
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
import { ref, computed, onMounted } from 'vue';
import type { UserDataModel } from '@/types/vueInterface';
import { AuthMapping, ReverseAuthMapping, statusMapping, reverseStatusMapping } from '@/utils/mappings'; // 對應狀態碼到中文

  
 
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
    const isEditMode = ref(true); // 用來區分新增或編輯資料
    const currentItem = ref<UserDataModel | null>(null);

    const fetchUsers = async () => {
        try {
            const url = '/api/Privilege'
            const response = await axios.get<UserDataModel[]>(url);
      /*      console.log(response.data);*/
            if (response) {
            lists.value = response.data;
            }
        }
        catch (error) {
            console.error(error);
        }
    }

    const processedLists = computed(() => {
        return lists.value.map(list => ({
            ...list,
            auth: AuthMapping[list.auth || ''] || list.auth,
            status3: statusMapping[list.status3 || ''] || list.status3
        }));
    });

    const editItem = (item: UserDataModel) => {
        // 加入編輯邏輯
        console.log('Edit item:', item);
        isEditMode.value = true;
        isEditing.value = true;
        currentItem.value = { ...item };
    };

    const addItem = () => {
        isEditMode.value = false;
        isEditing.value = true;
        currentItem.value = { name: '', account: '', password: '', auth: '', status1: '', status3: '' };
    }

    const saveItem = async () => {
        // 保存邏輯
        if (currentItem.value) {
            currentItem.value.auth = ReverseAuthMapping[currentItem.value.auth || ''] || currentItem.value.auth;
            currentItem.value.status3 = reverseStatusMapping[currentItem.value.status3 || ''] || currentItem.value.status3;
            const url = '/api/Privilege'
            const data: UserDataModel | null = currentItem.value;
            if (isEditMode.value) { // 如果是編輯用put,新增用post
                const response = await axios.put(url, data);
            } else {
                const response = await axios.post(url, data);
            }
        }
        /*console.log(currentItem.value);*/

        fetchUsers();
        isEditing.value = false;
    };

    const cancelEdit = () => {
        isEditing.value = false;
        currentItem.value = null;
    };
    onMounted(fetchUsers);
</script>

<style scoped>
 .small-btn {
     transform: scale(0.8);
 }
</style>