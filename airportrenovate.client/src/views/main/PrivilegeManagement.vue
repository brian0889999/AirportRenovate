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
                      label="權限"
                      :rules="[rules.required]"></v-select>
            <v-select v-model="currentItem.Auth" 
                      :items="ReverseAuthMapping"  item-title="text" item-value="value"
                      label="組室"
                      :rules="[rules.required]"></v-select>
            <v-select v-model="currentItem.Status3"
                      :items="ReverseStatusMapping" item-title="text" item-value="value" 
                      label="系統"></v-select>
            <v-text-field v-model="currentItem.Account" label="帳號"
                          :rules="[rules.required]"></v-text-field>
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
import type { VDataTable } from 'vuetify/components';
import { AuthMapping, ReverseAuthMapping, Status3Mapping, ReverseStatusMapping } from '@/utils/mappings'; // 對應狀態碼到中文
import { get, put, post, type ApiResponse } from '@/services/api';
import { RULES } from '@/constants/constants';
import  AuthTable  from '@/components/modules/AuthTable.vue';
type ReadonlyHeaders = VDataTable['$props']['headers'];

    const authheaders: ReadonlyHeaders = [
        { title: '姓名', align: 'start', sortable: false, key: 'Name', value: 'Name' },
        { title: '權限', key: 'Status1', value: 'Status1' },
        { title: '系統', key: 'Status3', value: 'Status3' },
        { title: '編輯', key: 'Edit', value: 'Edit', sortable: false },
    ];

    const lists = ref<UserDataModel[]>([]);
    const isEditing = ref<boolean>(false);
    const isEditMode = ref<boolean>(true); // 用來區分新增或編輯資料
    //const currentItem = ref<UserDataModel | null>(null); // 表單的欄位資料
    const defaultCurrent: UserDataModel = {
        Name: '',
        Account: '',
        Password: '',
        Auth: '',
        Status1: '',
        Status2: '',
        Status3: ''
    };
    const currentItem = ref<UserDataModel>(defaultCurrent); // 表單的欄位資料
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
            const url = '/api/User'
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
        currentItem.value = defaultCurrent;
    }

    const rules = RULES;
    const saveItem = async () => {
        console.log(currentItem.value);
        const { valid } = await managementFormRef.value?.validate();
        if (!valid) return;
        // 保存邏輯
        if (currentItem.value) {
            //if (currentItem.value.Status3 === "無") {
            //    currentItem.value.Status3 = "";  // 將"無"轉換為空字串
            //};
            const url = '/api/User'
            const data: UserDataModel = currentItem.value;
            try {
                let response: ApiResponse<any>;
                if (isEditMode.value) { // 如果是編輯用put,新增用post
                         response = await put<any>(url, data);
                        console.log(response.Data);
                }
                else
                {
                    console.log(data);
                         response = await post<any>(url, data);
                        console.log(response.Data);
                };
                // 這裡可以加入成功處理的邏輯
                console.log("操作成功");
                await fetchUsers();
            } catch (error) {
                // 處理錯誤
                console.error(error);
                // 這裡可以加入錯誤處理的邏輯,例如提示用戶或記錄錯誤
            } finally {
                isEditing.value = false;
            }
        }
        /*console.log(currentItem.value);*/
    };

    const cancelEdit = () => {
        isEditing.value = false;
        currentItem.value = defaultCurrent;
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