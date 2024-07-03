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


// �Ʀr�榡��
export const formatNumber = (value: number): string  => {
    if (!value) return '0';
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

