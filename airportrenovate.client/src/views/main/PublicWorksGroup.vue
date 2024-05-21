<template>
    <!--<div>123</div>-->
    <v-container>
        <v-row>
            <v-col>
                <v-select label="Select"
                          :items="years"
                          v-model="searchYear"></v-select>
                <v-btn @click="searchMoneyDb">查詢</v-btn>
                <v-data-table :headers="headers"
                              :items="desserts"
                              item-key="name"
                              items-per-page="5"></v-data-table>
            </v-col>
        </v-row>
    </v-container>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import { post, type ApiResponse } from '@/services/api';
import type { VDataTable } from 'vuetify/components';

type ReadonlyHeaders = VDataTable['$props']['headers'];

const headers: ReadonlyHeaders = [
        { title: '預算名稱', key: '', value: '' },
        { title: '組室別', key: '', value: '' },
        { title: '科目(6級)', key: '', value: '' },
        { title: '科目(7級', key: '', value: '' },
        { title: '科目(8級)', key: '', value: '' },
        { title: '年度預算額度(1)', key: '', value: '' },
        { title: '併決算書(2)', key: '', value: '' },
        { title: '一般動支數(3)', key: '', value: '' },
        { title: '勻出數(4))', key: '', value: '' },
        { title: '可用預算餘額(5)=(1)+(2)-(3)-(4)', key: '', value: '' },
        { title: '勻入數(6)', key: '', value: '' },
        { title: '勻入實付數(7)', key: '', value: '' },
        { title: '勻入數餘額(8)=(6)-(7)', key: '', value: '' },
        { title: '本科目實付數(9)', key: '', value: '' }
    ]
    const items = {

    }
const years = [111, 112, 113]
const searchYear = ref<number>(113)
    //console.log('searchYear=', searchYear);
    //console.log('searchYear.value=', searchYear.value);
    const searchMoneyDb = async () => {

        const url = '/api/MoneyDb';
        const data = { Year: searchYear.value };
        try {

            const response = await axios.post(url, data);
            if (response) {
                console.log(response.data);
                const dbData = response.data;
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
</style>