<template>
    <!--<div>123</div>-->
    <v-container class="no-margin" fluid>
        <v-row justify="start">
            <v-col cols="12" sm="8" md="6">
                <v-select label="查詢年度"
                          :items="years"
                          v-model="searchYear"
                          style="width: 100px;"
                         ></v-select>
                <v-btn @click="searchMoneyDb">查詢</v-btn>

            </v-col>
        </v-row>
        <v-data-table :headers="headers"
                      :items="items"
                      item-key="name"
                      items-per-page="-1"
                      hide-default-footer
                      style="width: 100%;">
        </v-data-table>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import { post, type ApiResponse } from '@/services/api';
import type { VDataTable } from 'vuetify/components';
    import type { BudgetDataModel } from '@/types/vueInterface';


type ReadonlyHeaders = VDataTable['$props']['headers'];

const headers: ReadonlyHeaders = [
    /*{ title: '預算名稱', key: '', value: 'budget' },*/
    { title: '預算名稱', key:'budget' },
    { title: '組室別', key:'group' },
    { title: '科目(6級)', key:'subject6' },
    { title: '科目(7級', key:'subject7' },
    { title: '科目(8級)', key:'subject8' },
    { title: '年度預算額度(1)', key:'budgetYear' },
    { title: '併決算書(2)', key:'final' },
    { title: '一般動支數(3)', key:'general' },
    { title: '勻出數(4))', key:'out' },
    { title: '可用預算餘額(5)=(1)+(2)-(3)-(4)', key:'useBudget' },
    { title: '勻入數(6)', key:'in' },
    { title: '勻入實付數(7)', key:'inActual' },
    { title: '勻入數餘額(8)=(6)-(7)', key:'inBalance' },
    { title: '本科目實付數(9)', key:'subjectActual' }
    ]
const items = ref<BudgetDataModel[]>([]);


const years = [111, 112, 113]
const searchYear = ref<number>(113)
    //console.log('searchYear=', searchYear);
    //console.log('searchYear.value=', searchYear.value);
    const searchMoneyDb = async () => {

        const url = '/api/MoneyDb';
        const data = { Year: searchYear.value };  // 抓年度的值
        try {

            const response = await axios.post<BudgetDataModel[]>(url, data);
            if (response) {
                console.log(response.data);
                const dbData = response.data;
                items.value = dbData
            }
            //const response: ApiResponse<any> = await post<any>(url, data);
            //if (response.StatusCode === 200) {
            //    console.log(response.Data);
            //} else {
            //    console.log(response.Data ?? response.Message)
            //}
        }
        catch (error: any) {
            console.error(error)
        }
        finally {
            console.log('end');
        }
    }




</script>

<style scoped>
    .no-margin {
        margin: 0;
    }
</style>