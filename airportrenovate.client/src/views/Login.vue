<template>
    <div>
        <h2>登入</h2>
        <form  @submit.prevent="login" enctype="application/x-www-form-urlencoded">
            <div>
                <label for="account">帳號：</label>
                <input type="text" name="Account" id="account" v-model="loginData.Account" placeholder="請輸入帳號" />
            </div>
            <div>
                <label for="password">密碼：</label>
                <input type="password" name="Password" id="password" v-model="loginData.Password" placeholder="請輸入密碼" />
            </div>
            <div>
                <button type="submit">登入</button>
            </div>
        </form>
    </div>
</template>


<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import type { LoginViewModel } from '@/types/apiInterface';

const loginData = ref<LoginViewModel>({
    Account: '',
    Password: '',
})
    const login = async () => {
        const url = '/api/Login';
        const data = loginData.value;
    try {
        const response = await axios.post(url, data);

        console.log(response.data); // 登入成功後的回傳資料
    } catch (error) {
        console.error('登入失敗:', error); // 處理登入失敗的情況
    }
}


</script>

<style scoped>

</style>