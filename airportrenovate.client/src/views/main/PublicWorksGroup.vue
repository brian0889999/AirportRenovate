<template>
    <!--<div>123</div>-->
    <v-container class="no-margin" fluid>
        <v-row justify="start" v-if="!isSelectedItem">
            <v-col cols="12" sm="8" md="6">
                <v-select label="查詢年度"
                          :items="years"
                          v-model="searchYear"
                          style="width: 100px;">
                </v-select>
                <v-btn @click="searchMoneyDb"
                       color="primary"
                       class="mb-2">
                    查詢
                </v-btn>
            </v-col>
        </v-row>
        <v-data-table v-if="!isSelectedItem"
                      :headers="headers"
                      :items="computedItems"
                      item-key="name"
                      items-per-page="12"
                      loading-text="讀取中請稍後..."
                      items-per-page-text="每頁筆數"
                      :loading="loading"
                      style="width: 100%;">
            <template #item.Budget="{ item }">
                <v-btn variant="flat"
                       class="mb-2"
                       @click="handleBudgetClick(item)">
                    {{ item.Budget }}
                </v-btn>
                <br />
                <v-btn @click="handleExcelClick(item.Budget)"
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
                       class="mb-2">
                    回上一頁
                </v-btn>
            </v-col>
        </v-row>
        <v-data-table v-if="isSelectedItem"
                      :headers="selectedHeaders"
                      :items="selectedItem"
                      hide-default-footer>
        </v-data-table>
        <v-data-table v-if="isSelectedItem"
                      :headers="selectedDetailHeaders"
                      :items="computedSelectedDetailItems">
            <template #item.Purchasedate="{ item }">
                {{ item.FormattedPurchasedate }}
            </template>
            <template #item.PayDate="{ item }">
                {{ item.FormattedPayDate }}
            </template>
        </v-data-table>
    </v-container>
</template>


<script setup lang="ts">
    import axios from 'axios';
    import { ref, computed } from 'vue';
    import { get, post, type ApiResponse } from '@/services/api';
    import type { VDataTable } from 'vuetify/components';
    import type { BudgetModel, SelectedBudgetModel, MoneyItem, MoneyRawData } from '@/types/apiInterface';
    import { formatDate, sumByCondition, groupBy } from '@/utils/functions'
    const group: string = "工務組";
    const loading = ref<boolean>(false);
    type ReadonlyHeaders = VDataTable['$props']['headers'];

    const headers: ReadonlyHeaders = [
        /*{ title: '預算名稱', key: '', value: 'budget' },*/
        { title: '預算名稱', key: 'Budget' },
        { title: '組室別', key: 'Group' },
        { title: '科目(6級)', key: 'Subject6' },
        { title: '科目(7級)', key: 'Subject7' },
        { title: '科目(8級)', key: 'Subject8' },
        { title: '年度預算額度(1)', key: 'BudgetYear' },
        { title: '併決算書(2)', key: 'Final' },
        { title: '一般動支數(3)', key: 'General' },
        { title: '勻出數(4)', key: 'Out' },
        { title: '可用預算餘額(5)=(1)+(2)-(3)-(4)', key: 'UseBudget' },
        { title: '勻入數(6)', key: 'In' },
        { title: '勻入實付數(7)', key: 'InActual' },
        { title: '勻入數餘額(8)=(6)-(7)', key: 'InBalance' },
        { title: '本科目實付數(9)', key: 'SubjectActual' },
    ];


    const items = ref<MoneyItem[]>([]);

    //isSelectedItem
    const isSelectedItem = ref<boolean>(false);

    const previousPage = () => isSelectedItem.value = false;

    const selectedHeaders: ReadonlyHeaders = [
        { title: '6級(科目)', key: 'Subject6' },
        { title: '7級(子目)', key: 'Subject7' },
        { title: '8級(細目)', key: 'Subject8' },
        { title: '年度預算額度(1)', key: 'BudgetYear' },
        { title: '併決算數額(2)', key: 'Final' },
        { title: '一般動支數(3)', key: 'General' },
        { title: '勻出數(4)', key: 'Out' },
        { title: '一般預算餘額(不含勻入)(5)=(1)+(2)-(3)-(4)', key: 'UseBudget' },
        { title: '勻入數額(6)累計(勻入)請購金額', key: 'In' },
        { title: '勻入實付數額(7)累計(勻入)實付金額', key: 'InActual' },
        { title: '勻入數餘額(8)=(6)-(7)', key: 'InBalance' },
        { title: '本科目實付數(9)累計(一般)及(勻入)實付金額', key: 'SubjectActual' },
        { title: '(含勻入)可用預算餘額(10)=(1)+(2)-(3)-(4)+(6)', key: 'End' }
    ];
    const selectedItem = ref<SelectedBudgetModel[]>([]);

    const selectedDetailHeaders: ReadonlyHeaders = [
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
    ];
    const selectedDetailItem = ref<MoneyRawData[]>([]);

    const years = [111, 112, 113]
    const searchYear = ref<number>(113)
    //console.log('searchYear=', searchYear);
    //console.log('searchYear.value=', searchYear.value);
    const searchMoneyDb = async () => {

        const url = '/api/MoneyDb/ByYear';
        //const data = { params: { Year: searchYear.value } }; 
        console.log(123);
        const data = { Year: searchYear.value, Group: group };  // 抓年度的值
        loading.value = true;
        try {

            /*const response = await axios.get<BudgetModel[]>(url, data);*/
            //if (response) {
            //    console.log(response.data);
            //    const dbData = response.data;
            //    items.value = dbData;  
            //}
            const response: ApiResponse<MoneyRawData[]> = await get<MoneyRawData[]>(url, data);
            if (response.StatusCode === 200) {
                console.log(response.StatusCode);
                console.log(response.Data);
                const dbData = response.Data?.map((item: MoneyRawData): MoneyItem => ({
                    Budget: item.MoneyDbModel.Budget,
                    Group: item.MoneyDbModel.Group,
                    Subject6: item.MoneyDbModel.Subject6,
                    Subject7: item.MoneyDbModel.Subject7,
                    Subject8: item.MoneyDbModel.Subject8,
                    BudgetYear: item.MoneyDbModel.BudgetYear,
                    Final: parseFloat(item.MoneyDbModel.Final ?? "") || 0,
                    Text: item.Text,
                    PurchaseMoney: item.PurchaseMoney,
                    PayMoney: item.PayMoney,
                    Purchasedate: item.Purchasedate || ""  // 確保不為 undefined
                })) ?? [];
                items.value = dbData;
                selectedDetailItem.value = response.Data ?? [];
                console.log('selectedDetailItem.value', selectedDetailItem.value);
            } else {
                console.log(response.Data ?? response.Message)
            }
        }
        catch (error: any) {
            console.error(error)
        }
        finally {
            loading.value = false;
            console.log('end');
        }
    };

    //// sumByCondition透過condition(篩選條件是item的Text欄位)找出那一欄位的field做總和
    //// groupBy用於分組
    const computedItems = computed(() => {
        const groupedItems = groupBy(items.value, ['Budget', 'Group', 'Subject6', 'Subject7', 'Subject8', 'BudgetYear', 'Final']);
        const sortedItems = Object.values(groupedItems).map(group => {
            const firstItem = group[0];
            const general = sumByCondition(group, '一般', 'PurchaseMoney');
            const out = sumByCondition(group, '勻出', 'PurchaseMoney');
            const inValue = sumByCondition(group, '勻入', 'PurchaseMoney');
            const inActual = sumByCondition(group, '勻入', 'PayMoney');
            const subjectActual = inActual + sumByCondition(group, '一般', 'PayMoney');
            const useBudget = (firstItem.BudgetYear || 0) - out - general + (firstItem.Final || 0);
            const end = (firstItem.BudgetYear || 0) + (firstItem.Final || 0) - general - out + inValue;
            const inBalance = inValue - inActual;

            return {
                ...firstItem,
                General: general,
                Out: out,
                In: inValue,
                InActual: inActual,
                UseBudget: useBudget,
                End: end,
                InBalance: inBalance,
                SubjectActual: subjectActual
            };
        });
        //按照日期排序
        return sortedItems.sort((a, b) => new Date(b.Purchasedate || "").getTime() - new Date(a.Purchasedate || "").getTime());
    });

    const computedSelectedDetailItems = computed(() => {
        return selectedDetailItem.value
            .map(item => ({
                ...item,
                FormattedPurchasedate: formatDate(item.Purchasedate || ""),
                FormattedPayDate: formatDate(item.PayDate || "")
            }))
            .sort((a, b) => new Date(b.Purchasedate || "").getTime() - new Date(a.Purchasedate || "").getTime());
    });

    const handleBudgetClick = async (budget: SelectedBudgetModel) => {
        isSelectedItem.value = true;
        const data = [{ ...budget }];
        console.log('Budget clicked:', data);
        selectedItem.value = data.map((v) => {
            const newData: SelectedBudgetModel = {
                Subject6: v.Subject6,
                Subject7: v.Subject7,
                Subject8: v.Subject8,
                BudgetYear: v.BudgetYear,
                Final: v.Final,
                General: v.General,
                Out: v.Out,
                UseBudget: v.UseBudget,
                In: v.In,
                InActual: v.InActual,
                InBalance: v.InBalance,
                SubjectActual: v.SubjectActual,
                End: v.End
            };
            return newData;
        });

        // 篩選 selectedDetailItem，選出 Budget 欄位與傳進來的 Budget 欄位值相同的資料
        const budgetValue = budget.Budget;
        selectedDetailItem.value = selectedDetailItem.value.filter(item => item.MoneyDbModel.Budget === budgetValue && item.Status !== 'C');
        console.log('Filtered selectedDetailItem:', selectedDetailItem.value);

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