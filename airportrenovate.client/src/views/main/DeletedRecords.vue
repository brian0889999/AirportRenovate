<template>
    <v-container class="no-margin" fluid>
        <v-row justify="start">
            <v-col cols="12" sm="8" md="6">
                <v-select label="查詢年度"
                          :items="years"
                          v-model="searchYear"
                          style="width: 100px;">
                </v-select>
                <v-btn @click="searchDeletedRecords"
                       color="primary"
                       class="mb-2">
                    查詢
                </v-btn>
            </v-col>
        </v-row>
        <v-data-table :headers="headers"
                      :items="items"
                      item-key="name"
                      items-per-page="12"
                      loading-text="讀取中請稍後..."
                      items-per-page-text="每頁筆數"
                      :loading="loading"
                      style="width: 100%;">
            <template v-slot:item.Purchasedate="{ item }">
                {{ formatDate(item.Purchasedate) }}
            </template>
            <template v-slot:item.PayDate="{ item }">
                {{ formatDate(item.PayDate) }}
            </template>
            <template v-slot:item.actions="{ item }">
               <v-btn @click="restoreData(item)"
                      color="primary">
                還原
               </v-btn>
            </template>
        </v-data-table>
       
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref, computed, onMounted } from 'vue';
import type { SoftDeleteViewModel } from '@/types/apiInterface';
//import { AuthMapping, ReverseAuthMapping, StatusMapping, ReverseStatusMapping } from '@/utils/mappings'; // 對應狀態碼到中文
import { get, put, type ApiResponse } from '@/services/api';
import type { VDataTable } from 'vuetify/components';
type ReadonlyHeaders = VDataTable['$props']['headers'];
import { formatDate } from '@/utils/functions';
  

const years = ref<number[]>([111, 112, 113]);
    const searchYear = ref<number>(113);
    const items = ref<SoftDeleteViewModel[]>([]);
    const headers: ReadonlyHeaders = [
        { title: '請購日期', key: 'Purchasedate' },
        { title: '類別', key: 'Text' },
        { title: '摘要', key: 'Note' },
        { title: '請購金額', key: 'PurchaseMoney' },
        { title: '支付日期', key: 'PayDate' },
        { title: '實付金額', key: 'PayMoney' },
        { title: '請購人', key: 'People' },
        { title: '支付人', key: 'People1' },
        { title: '備註', key: 'Remarks' },
        { title: '未稅', key: 'All' },
        { title: '已對帳', key: 'True' },
        { title: '', key: 'actions', sortable: false },
    ];
const searchDeletedRecords = async () => {
    const url = 'api/DeletedRecords';
    const data = { Year: searchYear.value };
    try {
        const response: ApiResponse<SoftDeleteViewModel[]> = await get<SoftDeleteViewModel[]>(url, data);
        //console.log(response.Message);
        if (response.StatusCode == 200) {
            items.value = response?.Data ?? []; 
            console.log(items);
        }
    }
    catch (error) {
        console.error(error);
        }
    };
    const restoreData = async (item: SoftDeleteViewModel) => {
        const isConfirmed = confirm('你確定要還原嗎？');
        const url = 'api/DeletedRecords/ByRestoreData'
        if (isConfirmed) {
            try {
                //console.log(item);
                const response: ApiResponse<any> = await put<SoftDeleteViewModel>(url, item);
                if (response.StatusCode == 200) { // 如果成功再叫一次資料
                    await searchDeletedRecords();
                }
            }
            catch (error) {
                console.error(error);
            }
        }
    };
</script>

<style scoped>
 
</style>