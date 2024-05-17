<template>
    <v-container style="width:100%; display:flex;" class=".custom-container">
        <!--<v-data-table style="width:100%" :headers="headers"
                      :items="lists"
                      :items-per-page="5"
                      class="elevation-1"
                      :hide-default-footer="true">
        </v-data-table>-->
        <v-data-table style="width:100%" :headers="headers"
                      :items="items"
                      :items-per-page="5"
                      class="elevation-1"
                      :hide-default-footer="true">
        </v-data-table>
    </v-container>
</template>


<script setup lang="ts">
//import axios from 'axios';
import { ref } from 'vue';

    const headers = [
        { title: '權限', align: 'start', sortable: false, value: '6級(科目)' },
        { title: '功能', value: '功能' },
        { title: '備註', value: '備註' },
    ];
    
    const items = ref([
        { 權限: 'A', 功能: '新增、編輯、刪除、檢視、勻出入、復原刪除', 備註: '管理者' },
        { 權限: 'B', 功能: '檢視', 備註: '各單位窗口' },
        { 權限: 'C', 功能: '新增、編輯、刪除、檢視、勻出入', 備註: '工務組承辦' },
        { 權限: 'D', 功能: '', 備註: '帳號不開放' },
    ]);
    const authheaders = [
        { title: '姓名', align: 'start', sortable: false, value: 'name' },
        { title: '權限', value: 'status1' },
        { title: '系統', value: 'status3'},
    ]
    const lists = ref([{}]);
</script>

<style scoped>
 
</style>