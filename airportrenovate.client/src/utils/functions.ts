import type { MoneyItem, MoneyRawData } from '@/types/apiInterface';
import { type ApiResponse, get } from '@/services/api';
import axios from 'axios';

// �z�Lcondition(�z�����Oitem��Text���)��X���@��쪺field���`�M
export const sumByCondition = (items: MoneyItem[], condition: string, field: keyof MoneyItem) => {
    return items
        .filter(item => item.Text === condition)
        .reduce((sum, item) => sum + (item[field] as number || 0), 0);
};

// �Ω����
export const groupBy = (array: MoneyItem[], keys: (keyof MoneyItem)[]) => {
    return array.reduce((result, currentValue) => {
        const key = keys.map(k => currentValue[k]).join('-');
        if (!result[key]) {
            result[key] = [];
        }
        result[key].push(currentValue);
        return result;
    }, {} as Record<string, MoneyItem[]>);
};

// ����~�ഫ�u����
export const formatDate = (dateString: string): string => {
    if (!dateString) return "";  // �B�z�ŭ�
    const date = new Date(dateString);
    if (isNaN(date.getTime())) return "";  // �B�z�L�Ĥ��
    const year = date.getFullYear() - 1911;
    const month = (date.getMonth() + 1).toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    return `${year}/${month}/${day}`;
};

export const searchMoneyDb = async (searchYear: number, items: any, selectedDetailItem: any, loading: any) => {
    const url = '/api/MoneyDb/ByYear';
    const data = { Year: searchYear };  // ��~�ת���
    loading.value = true;
    try {
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
                Purchasedate: item.Purchasedate || ""  // �T�O���� undefined
            })) ?? [];
            items.value = dbData;
            selectedDetailItem.value = response.Data ?? [];
            console.log('selectedDetailItem.value', selectedDetailItem.value);
        } else {
            console.log(response.Data ?? response.Message);
        }
    }
    catch (error: any) {
        console.error(error);
    }
    finally {
        loading.value = false;
        console.log('end');
    }
};

export const handleExcelClick = async (budget: string) => {
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
