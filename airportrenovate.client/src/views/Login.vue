<template>
    <div>
        <h2>登入</h2>
        <form @submit.prevent="login">
            <div>
                <label for="account">帳號：</label>
                <input type="text" name="account" id="account" v-model="loginData.account" placeholder="請輸入帳號" />
            </div>
            <div>
                <label for="password">密碼：</label>
                <input type="password" name="password" id="password" v-model="loginData.password" placeholder="請輸入密碼" />
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
    account: '',
    password: ''
})
const login = async () => {
    try {
        const response = await axios.post('/login', loginData);
        console.log(response.data); // 登入成功後的回傳資料
    } catch (error) {
        console.error('登入失敗:', error); // 處理登入失敗的情況
    }
}


</script>

<style scoped>

</style>