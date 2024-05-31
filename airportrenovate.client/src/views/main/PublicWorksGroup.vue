<template>
    <!--<div>123</div>-->
    <v-container class="no-margin" fluid>
        <v-row justify="start" v-if="!isSelectedItem">
            <v-col cols="12" sm="8" md="6">
                <v-select label="查詢年度"
                          :items="years"
                          v-model="searchYear"
                          style="width: 100px;"></v-select>
                <v-btn @click="searchMoneyDb"
                       color="primary"
                       class="mb-2">查詢</v-btn>

            </v-col>
        </v-row>
        <v-data-table v-if="!isSelectedItem"
                      :headers="headers"
                      :items="items"
                      item-key="name"
                      items-per-page="10"
                      loading-text="讀取中請稍後..."
                      items-per-page-text="每頁筆數"
                      :loading="loading"
                      style="width: 100%;">
            <template #item.budget="{ item }">
                <v-btn variant="flat"
                       class="mb-2"
                       @click="handleBudgetClick(item)">{{ item.budget }}</v-btn>
                <br />
                <v-btn @click="handleExcelClick(item.budget)"
                       color="primary">
                    EXCEL
                </v-btn>
            </template>
        </v-data-table>
        <!-- isSelectedItem -->
        <v-row justify="start" v-if="isSelectedItem">
            <v-col cols="12" sm="8" md="6">
                <v-btn @click="previousPage"
                       color="primary"
                       class="mb-2">回上一頁</v-btn>

            </v-col>
        </v-row>
        <v-data-table v-if="isSelectedItem"
                      :headers="selectedHeaders"
                      hide-default-footer></v-data-table>
        <v-data-table v-if="isSelectedItem"
                      :headers="selectedDetailHeaders"></v-data-table>
    </v-container>
</template>


<script setup lang="ts">
    import axios from 'axios';
    import { ref } from 'vue';
    import { get, post, type ApiResponse } from '@/services/api';
    import type { VDataTable } from 'vuetify/components';
    import type { BudgetDataModel, SelectedBudgetDataModel } from '@/types/vueInterface';


    type ReadonlyHeaders = VDataTable['$props']['headers'];

    const headers: ReadonlyHeaders = [
        /*{ title: '預算名稱', key: '', value: 'budget' },*/
        { title: '預算名稱', key: 'budget' },
        { title: '組室別', key: 'group' },
        { title: '科目(6級)', key: 'subject6' },
        { title: '科目(7級)', key: 'subject7' },
        { title: '科目(8級)', key: 'subject8' },
        { title: '年度預算額度(1)', key: 'budgetYear' },
        { title: '併決算書(2)', key: 'final' },
        { title: '一般動支數(3)', key: 'general' },
        { title: '勻出數(4)', key: 'out' },
        { title: '可用預算餘額(5)=(1)+(2)-(3)-(4)', key: 'useBudget' },
        { title: '勻入數(6)', key: 'in' },
        { title: '勻入實付數(7)', key: 'inActual' },
        { title: '勻入數餘額(8)=(6)-(7)', key: 'inBalance' },
        { title: '本科目實付數(9)', key: 'subjectActual' }
    ]


    const items = ref<BudgetDataModel[]>([]);

    //isSelectedItem
    const isSelectedItem = ref<boolean>(false);
    const previousPage = () => isSelectedItem.value = false;
    const selectedHeaders: ReadonlyHeaders = [
        { title: '6級(科目)', key: 'subject6' },
        { title: '7級(子目)', key: 'subject7' },
        { title: '8級(細目)', key: 'subject8' },
        { title: '年度預算額度(1)', key: 'budgetYear' },
        { title: '併決算數額(2)', key: 'final' },
        { title: '一般動支數(3)', key: 'general' },
        { title: '勻出數(4)', key: 'out' },
        { title: '一般預算餘額(不含勻入)(5)=(1)+(2)-(3)-(4)', key: 'useBudget' },
        { title: '勻入數額(6)累計(勻入)請購金額', key: 'in' },
        { title: '勻入實付數額(7)累計(勻入)實付金額', key: 'inActual' },
        { title: '勻入數餘額(8)=(6)-(7)', key: 'inBalance' },
        { title: '本科目實付數(9)累計(一般)及(勻入)實付金額', key: 'subjectActual' },
        { title: '(含勻入)可用預算餘額(10)=(1)+(2)-(3)-(4)+(6)', key: 'End' }
    ]
    const selectedItems = ref<SelectedBudgetDataModel[]>([]);

    const selectedDetailHeaders: ReadonlyHeaders = [
        { title: '請購日期', key: '' },
        { title: '類別', key: '' },
        { title: '摘要', key: '' },
        { title: '請購金額', key: '' },
        { title: '支付日期', key: '' },
        { title: '實付金額', key: '' },
        { title: '請購人', key: '' },
        { title: '支付人', key: '' },
        { title: '備註', key: '' },
        { title: '未稅', key: '' },
        { title: '已對帳', key: '' },
    ]


    const years = [111, 112, 113]
    const searchYear = ref<number>(113)
    //console.log('searchYear=', searchYear);
    //console.log('searchYear.value=', searchYear.value);
    const searchMoneyDb = async () => {

        const url = '/api/MoneyDb/ByYear';
        //const data = { params: { Year: searchYear.value } }; 
        console.log(123);
        const data = { Year: searchYear.value } ;  // 抓年度的值
        try {

            /*const response = await axios.get<BudgetDataModel[]>(url, data);*/
            //if (response) {
            //    console.log(response.data);
            //    const dbData = response.data;
            //    items.value = dbData
            //}
            const response: ApiResponse<any> = await get<any>(url, data);
            if (response.statusCode === 200) {
                console.log(response.data);
                const dbData = response.data;
                items.value = dbData
            } else {
                console.log(response.Data ?? response.Message)
            }
        }
        catch (error: any) {
            console.error(error)
        }
        finally {
            console.log('end');
        }
    }

    const handleBudgetClick = async (budget: object) => {
        isSelectedItem.value = true;
        const data = { ...budget };
        console.log('Budget clicked:', data);

        //try {
        //    const url = '/api/MoneyDb/ByBudget'
        //    const response = await axios.get(url, { params: { budget } })
        //    console.log(response.data);
        //} catch (error) {
        //    console.error(error);
        //}
    };

    const handleExcelClick = async (budget: string) => {
        try {
            const response = await axios.get(`/api/MoneyDb/ExportToExcel?budget=${budget}`, {
                responseType: 'blob'
            });
            const url = window.URL.createObjectURL(new Blob([response.data]));
            const link = document.createElement('a');
            link.href = url;
            link.setAttribute('download', `${budget}.xlsx`);
            document.body.appendChild(link);
            link.click();
        } catch (error) {
            console.error(error);
        }
    };

</script>

<style scoped>
    .no-margin {
        margin: 0;
    }
    /*.link-style {
        color: #007bff;
        text-decoration: underline;
        cursor: pointer;
    }*/
</style>