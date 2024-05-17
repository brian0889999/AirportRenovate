<template>
    <v-container fluid fill-height >
        <v-row  justify="center" align="center">
            <v-col>
                <h2>登入</h2>
                <form @submit.prevent="login" enctype="application/x-www-form-urlencoded">
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
            </v-col>
        </v-row>
    </v-container>
    <!--<v-container fill-height fluid>
        <v-row justify="center" align="center" style="width: 100%; height: 100vh;">
            <v-col  align="center" cols="12" sm="8" md="6">
                <v-card max-width="500">
                    <v-card-title class="text-center">
                        <h2>登入</h2>
                    </v-card-title>
                    <v-card-text>
                        <v-form @submit.prevent="login" enctype="application/x-www-form-urlencoded" >
                            <v-text-field v-model="loginData.Account" label="帳號" outlined ></v-text-field>
                            <v-text-field v-model="loginData.Password" label="密碼" outlined type="password" ></v-text-field>
                            <v-btn type="submit" color="primary" block>登入</v-btn>
                        </v-form>
                    </v-card-text>
                </v-card>
            </v-col>
        </v-row>
    </v-container>-->

</template>


<script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import type { LoginViewModel } from '@/types/apiInterface';
import { useRouter } from 'vue-router';

const router = useRouter();

const loginData = ref<LoginViewModel>({
    Account: '',
    Password: '',
})
    const login = async () => {
        const url = '/api/Login';
        const data = loginData.value;
    try {
        const response = await axios.post(url, data);
        router.push('/main')
        console.log(response.data ? response.data :'沒有資料'); // 登入成功後的回傳資料
    } catch (error) {
        console.error('登入失敗:', error); // 處理登入失敗的情況
    }
}


</script>

<style scoped>

</style>