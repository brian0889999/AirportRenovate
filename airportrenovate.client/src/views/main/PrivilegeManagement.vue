<template>
    
    <v-container style="width:100%; display:flex;">
        <v-row>
            <v-col cols="12" sm="8" md="6">
                <v-btn v-if="!isEditing" @click="addItem" color="primary" class="mb-4">新增帳號</v-btn>
                <v-data-table v-if="!isEditing"
                              :headers="authheaders"
                              :items="paginatedItems"
                              item-key="Name"
                              items-per-page="12"
                              class="elevation-1"
                              style="width: 100%;"
                              :footer-props="{
                itemsPerPageOptions: [12],
                showFirstLastPage: true,
                showCurrentPage: true
              }"
                              hide-default-footer> <!-- 這邊items-per-page要跟itemsPerPage一樣-->
                    <template v-slot:top>
                        <v-pagination v-model="page"
                                      :length="pageCount"
                                      @input="updatePage"
                                      class="mb-4"></v-pagination>
                    </template>
                    <template #item.Status3="{ item }">
                        {{ translateStatus3(item.Status3) }}
                    </template>
                    <template v-slot:[`item.Edit`]="{ item }">
                        <v-btn @click="editItem(item)" icon class="small-btn">
                            <v-icon>mdi-pencil</v-icon>
                        </v-btn>
                    </template>
                </v-data-table>

            </v-col>
            <v-col cols="12" sm="8" md="6">
                <!--寫死的Table-->
                <AuthTable v-if="!isEditing" />
            </v-col>
        </v-row>
    <div v-if="isEditing" style="width: 100%;">
        <v-form ref="managementFormRef">
            <v-text-field v-model="currentItem.Name" label="姓名" :readonly="isEditMode"
                          :bg-color="nameColumn"
                          :rules="[rules.required]"></v-text-field>
            <v-select v-model="currentItem.Status1"
                      :items="status1Items"
                      label="權限"></v-select>
            <v-select v-model="currentItem.Auth" 
                      :items="ReverseAuthMapping"  item-title="text" item-value="value"
                      label="組室"></v-select>
            <v-select v-model="currentItem.Status3"
                      :items="ReverseStatusMapping" item-title="text" item-value="value" 
                      label="系統"></v-select>
            <v-text-field v-model="currentItem.Account" label="帳號"></v-text-field>
            <v-text-field v-model="currentItem.Password" label="密碼" 
                          :append-inner-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
                          :type="showPassword ? 'text' : 'password' "
                          @click:append-inner="showPassword = !showPassword"
                          :rules="[rules.passwordFormat]"></v-text-field>
            <v-btn @click="saveItem" color="primary" class="mr-2" size="large">{{ saveBtn }}</v-btn>
            <v-btn @click="cancelEdit" color="primary" size="large">取消</v-btn>
        </v-form>
    </div>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref, computed, onMounted } from 'vue';
import type { UserDataModel } from '@/types/apiInterface';
import { AuthMapping, ReverseAuthMapping, Status3Mapping, ReverseStatusMapping } from '@/utils/mappings'; // 對應狀態碼到中文
import { get, put, post, type ApiResponse } from '@/services/api';
import { RULES } from '@/constants/constants';
import  AuthTable  from '@/components/modules/AuthTable.vue';

 //寫死的Table
    //const headers = ref([
    //    { title: '權限', align: 'start', sortable: false, key: '權限' },
    //    { title: '功能', key: '功能' },
    //    { title: '備註', key: '備註' },
    //]);
    
    //const items = ref([
    //    { 權限: 'A', 功能: '新增、編輯、刪除、檢視、勻出入、復原刪除', 備註: '管理者' },
    //    { 權限: 'B', 功能: '檢視', 備註: '各單位窗口' },
    //    { 權限: 'C', 功能: '新增、編輯、刪除、檢視、勻出入', 備註: '工務組承辦' },
    //    { 權限: 'D', 功能: '', 備註: '帳號不開放' },
    //]);

    const authheaders = ref([
        { title: '姓名', align: 'start', sortable: false, key: 'Name', value: 'Name' },
        { title: '權限', key: 'Status1', value: 'Status1' },
        { title: '系統', key: 'Status3', value: 'Status3' },
        { title: '編輯', key: 'Edit', value: 'Edit', sortable: false },
    ]);

    const lists = ref<UserDataModel[]>([]);
    const isEditing = ref<boolean>(false);
    const isEditMode = ref<boolean>(true); // 用來區分新增或編輯資料
    //const currentItem = ref<UserDataModel | null>(null); // 表單的欄位資料
    const currentItem = ref<UserDataModel | null>({
        Name: '',
        Account: '',
        Password: '',
        Auth: '',
        Status1: '',
        Status2: '',
        Status3: ''
    }); // 表單的欄位資料
    const status1Items = ref<Array<string>>(['A', 'B', 'C', 'D'])
    const showPassword = ref<boolean>(false);
    const managementFormRef = ref<HTMLFormElement | null>(null);
    // pagination
    const page = ref<number>(1);
    const itemsPerPage = ref<number>(12);
    const pageCount = computed(() => Math.ceil(lists.value.length / itemsPerPage.value));
    const paginatedItems = computed(() => {
        const start = (page.value - 1) * itemsPerPage.value;
        const end = start + itemsPerPage.value;
        return lists.value.slice(start, end);
    });
    const updatePage = (newPage: number) => {
        page.value = newPage;
    };



    const saveBtn = computed(() => isEditMode.value ? "保存" : "新增");
    const nameColumn = computed(() => isEditMode.value ? "grey-lighten-1" : "");
    const translateStatus3 = (status3: string | undefined): string => {
        return status3 ? Status3Mapping[status3] || status3 : '';
    };
    //取得其他使用者
    const fetchUsers = async () => {
        try {
            const url = '/api/Privilege'
            const response: ApiResponse<UserDataModel[]> = await get<UserDataModel[]>(url);
            /*console.log(response.Data);*/
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

    const rules = RULES;
    const saveItem = async () => {
        console.log(currentItem.value);
        const { valid } = await managementFormRef.value?.validate();
        if (!valid) return;

        const regex: RegExp = /^(?!.*[^\x21-\x7e])(?=.*[\W])(?=.*[a-zA-Z])(?=.*\d).{8,20}$/;
        
        // 保存邏輯
        if (currentItem.value) {
            //if (!regex.test(currentItem.value.Password ?? '')) {
            //    alert('請輸入 8 到 20 個字符的密碼，必須包含至少一個字母、一個數字和一個特殊字符。');
            //    return;
            //}
            //currentItem.value.Auth = ReverseAuthMapping[currentItem.value.Auth || ''] || currentItem.value.Auth;
            //currentItem.value.Status3 = ReverseStatusMapping[currentItem.value.Status3 || ''] || currentItem.value.Status3;
           
            //if (currentItem.value.Status3 === "無") {
            //    currentItem.value.Status3 = "";  // 將"無"轉換為空字串
            //};

            const url = '/api/Privilege'
            const data: UserDataModel | null = currentItem.value;
            try {
                //let response;
                if (isEditMode.value) { // 如果是編輯用put,新增用post
                    try {
                        const response: ApiResponse<any> = await put<any>(url, data);
                        console.log(response.Data);
                    }
                    catch (error) {
                        console.error(error);
                    };
                } else {
                    try {
                        const response: ApiResponse<any> = await post<any>(url, data);
                        console.log(response.Data);                        
                    }
                    catch (error) {
                        console.error(error);
                    } 
                };
                // 這裡可以加入成功處理的邏輯
                console.log("操作成功");
                fetchUsers();
            } catch (error) {
                // 處理錯誤
                console.error(error);
                // 這裡可以加入錯誤處理的邏輯,例如提示用戶或記錄錯誤
            }
        }
        /*console.log(currentItem.value);*/
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

    /*.custom-background .v-input__control {
        background-color: #e0e0e0;*/ /* 灰色背景 */
    /*}

    .custom-background input {
        pointer-events: none;*/ /* 禁止點擊 */
    /*}*/
</style>