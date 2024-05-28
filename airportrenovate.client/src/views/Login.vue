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
import type { LoginUserModel } from '@/types/vueInterface';
import { useRouter } from 'vue-router';
import { useCookies } from 'vue3-cookies';
    import { post, type ApiResponse } from '../services/api';

const router = useRouter();

const { cookies } = useCookies(); // 初始化 vue3-cookies

const loginData = ref<LoginViewModel>({
    Account: '',
    Password: '',
})
    const login = async () => {
        const url = '/api/Login';
        const data = loginData.value;
        const regex: RegExp = /^(?!.*[^\x21-\x7e])(?=.*[\W])(?=.*[a-zA-Z])(?=.*\d).{8,20}$/;
        if (!regex.test(loginData.value.Password)) {
            alert('請輸入 8 到 20 個字符的密碼，必須包含至少一個字母、一個數字和一個特殊字符。');
            return;
        }
    try {
        const response = await axios.post(url, data);
        /*const response: ApiResponse<any> = await post<any>(url, data);*/
        if (response) {
            const userData: LoginUserModel = response.data;
            for (let key in userData) {
                if (key === 'status1' || key === 'status2' || key === 'status3') {
                    userData[key] = userData[key].trim(); // 清除資料多餘空格
                }
            }
            setUserData(userData);
         /*   console.log(userData ? userData : '沒有資料'); // 登入成功後的回傳資料*/
        }
        router.push('/main')
       
    } catch (error) {
        console.error('登入失敗:', error); // 處理登入失敗的情況
    }
}

    // 設定使用者資料的函數
    const setUserData = (userData: LoginUserModel) => {
        // 將使用者資料存入 Cookie，設置為一天後過期
        cookies.set('userData', JSON.stringify(userData), { expires: '1d' });
    };

    // 獲取使用者資料的函數
    const getUserData = () => {
        const userData = cookies.get('userData');
        if (userData) {
            console.log(JSON.parse(userData)); // 輸出從 Cookie 中讀取的使用者資料
        } else {
            console.log('No user data found');
        }
    };
</script>

<style scoped>

</style>