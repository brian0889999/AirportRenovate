<template>
    
    <v-container style="width:100%; display:flex;">
        <v-row>
            <v-col cols="12" sm="8" md="6">
                <v-btn v-if="!isEditing" @click="addItem" color="primary" class="mb-4">新增</v-btn>
                <v-data-table v-if="!isEditing"
                              :headers="authheaders"
                              :items="processedLists"
                              item-key="Name"
                              items-per-page="12"
                              class="elevation-1"
                              style="width: 100%;">
                    <template v-slot:[`item.Edit`]="{ item }">
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
            <v-text-field v-model="currentItem.Name" label="姓名" :readonly="isEditMode"></v-text-field>
            <v-select v-model="currentItem.Status1"
                      :items="['A', 'B', 'C', 'D']"
                      label="權限"></v-select>
            <v-select v-model="currentItem.Auth" 
                      :items="Object.keys(ReverseAuthMapping)" 
                      label="組室"></v-select>
            <v-select v-model="currentItem.Status3"
                      :items="Object.keys(ReverseStatusMapping)" 
                      label="系統"></v-select>
            <v-text-field v-model="currentItem.Account" label="帳號"></v-text-field>
            <v-text-field v-model="currentItem.Password" label="密碼"></v-text-field>
            <v-btn @click="saveItem" color="primary" class="mr-2" size="large">保存</v-btn>
            <v-btn @click="cancelEdit" color="primary" size="large">取消</v-btn>
        </v-form>
    </div>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref, computed, onMounted } from 'vue';
import type { UserDataModel } from '@/types/apiInterface';
import { AuthMapping, ReverseAuthMapping, StatusMapping, ReverseStatusMapping } from '@/utils/mappings'; // 對應狀態碼到中文
import { get } from '@/services/api';
  
 
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
        { title: '姓名', align: 'start', sortable: false, key: 'Name', value: 'Name' },
        { title: '權限', key: 'Status1', value: 'Status1' },
        { title: '系統', key: 'Status3', value: 'Status3' },
        { title: '編輯', key: 'Edit', value: 'Edit', sortable: false },
    ]);

    const lists = ref<UserDataModel[]>([]);
    const isEditing = ref<boolean>(false);
    const isEditMode = ref<boolean>(true); // 用來區分新增或編輯資料
    const currentItem = ref<UserDataModel | null>(null);

    const fetchUsers = async () => {
        try {
      //      const url = '/api/Privilege'
      //      const response = await axios.get<UserDataModel[]>(url);
      ///*      console.log(response.data);*/
      //      if (response) {
      //      lists.value = response.data;

      //      console.log("lists.value", lists.value);;
      //      }

            const url = '/api/Privilege'
      const response = await get<UserDataModel[]>(url);
/*      console.log(response.data);*/
       if (response && response.Data && response.Data) {
          lists.value = response.Data;
          //console.log("lists.value", response.Data);
        } else {
          console.error("Response data is null or undefined");
        }
        }
        catch (error) {
            console.error(error);
        }
    }

    const processedLists = computed(() => {
        return lists.value.map(list => ({
            ...list,
            Auth: AuthMapping[list.Auth || ''] || list.Auth,
            Status3: StatusMapping[list.Status3 || ''] || list.Status3
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
        currentItem.value = { Name: '', Account: '', Password: '', Auth: '', Status1: '', Status3: '' };
    }

    const saveItem = async () => {
        const regex: RegExp = /^(?!.*[^\x21-\x7e])(?=.*[\W])(?=.*[a-zA-Z])(?=.*\d).{8,20}$/;
        
        // 保存邏輯
        if (currentItem.value) {
            if (!regex.test(currentItem.value.Password ?? '')) {
                alert('請輸入 8 到 20 個字符的密碼，必須包含至少一個字母、一個數字和一個特殊字符。');
                return;
            }
            currentItem.value.Auth = ReverseAuthMapping[currentItem.value.Auth || ''] || currentItem.value.Auth;
            currentItem.value.Status3 = ReverseStatusMapping[currentItem.value.Status3 || ''] || currentItem.value.Status3;
           
            if (currentItem.value.Status3 === "無") {
                currentItem.value.Status3 = "";  // 將"無"轉換為空字串
            };

            const url = '/api/Privilege'
            const data: UserDataModel | null = currentItem.value;
            try {
                let response;
                if (isEditMode.value) { // 如果是編輯用put,新增用post
                     response = await axios.put(url, data);
                } else {
                     response = await axios.post(url, data);
                }
                // 這裡可以加入成功處理的邏輯
                console.log("操作成功", response.data);
            } catch (error) {
                // 處理錯誤
                console.error(error);
                // 這裡可以加入錯誤處理的邏輯,例如提示用戶或記錄錯誤
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