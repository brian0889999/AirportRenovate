<template>
    <v-container>
        <!-- 其他內容 -->
        <!--<v-btn @click="openEditDialog(item)">editItem</v-btn>
        <v-dialog v-model="editDialog" max-width="600px">-->
            <v-card>
                <v-card-title>
                    <span class="headline">編輯項目</span>
                </v-card-title>
                <v-card-text>
                    <v-form ref="editForm" v-model="isValid">
                        <v-row>
                            <v-col cols="12" sm="6">
                                <v-text-field v-model="formattedPurchasedate"
                                              label="請購日期"
                                              type="date"
                                              :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-select v-model="editedItem.Text"
                                          :items="categories"
                                          label="類別"
                                          :rules="[rules.required]"
                                          readonly></v-select>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field v-model="editedItem.Note"
                                              label="摘要"
                                              :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-text-field v-model="editedItem.PurchaseMoney"
                                              label="請購金額"
                                              type="number"
                                              :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-text-field v-model="formattedPayDate"
                                              label="支付日期"
                                              type="date"
                                              :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-text-field v-model="editedItem.PayMoney"
                                              label="實付金額"
                                              type="number"
                                              :rules="[rules.required]"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-select v-model="editedItem.People"
                                          :items="userNames"
                                          label="請購人"
                                          :rules="[rules.required]"></v-select>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-select v-model="editedItem.People1"
                                          :items="userNames"
                                          label="支付人"
                                          :rules="[rules.required]"></v-select>
                            </v-col>
                            <v-col cols="12">
                                <v-text-field v-model="editedItem.Remarks"
                                              label="備註"></v-text-field>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-checkbox v-model="editedItem.All"
                                            label="未稅"></v-checkbox>
                            </v-col>
                            <v-col cols="12" sm="6">
                                <v-checkbox v-model="editedItem.True"
                                            label="已對帳"></v-checkbox>
                            </v-col>
                            <v-col cols="12">
                                <v-select v-model="editedItem.Year1"
                                          :items="[111, 112, 113]"
                                          label="年度"
                                          :rules="[rules.required]"></v-select>
                            </v-col>
                        </v-row>
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn color="blue darken-1" text @click="canceledit">取消</v-btn>
                    <v-btn color="blue darken-1" text @click="submitform">儲存</v-btn>
                </v-card-actions>
            </v-card>
        <!--</v-dialog>-->
    </v-container>
</template>

<script setup lang="ts">
    import { ref, reactive, watch, type PropType, onMounted, computed } from 'vue';
    import { type ApiResponse, get } from '@/services/api';
    import { type UserDataModel } from '@/types/apiInterface';

    const Users = ref<UserDataModel[]>([]);
    const userNames = ref<string[]>([]);

    const fetchUsers = async () => {
        try {
            const url = 'api/Privilege';
            const response: ApiResponse<UserDataModel[]> = await get<UserDataModel[]>(url);
                if(response.StatusCode == 200) {
                    Users.value = response.Data!;
                    /*console.log('Users:', Users);*/
                     // 提取 Name 欄位，並在最前面加入 '無' 選項
                    userNames.value = ['無', ...Users.value.map(user => user.Name || '')];
                    //console.log('User Names:', userNames);
            }
        }
        catch (error) {
            console.error(error);
        }
    };
  
    const props = defineProps({
        item: {
            type: Object as PropType<any>,
            required: true
        }
    });

    const emit = defineEmits(['update', 'cancel']);

    const editedItem = ref({ ...props.item });

    //console.log('editedItem', editedItem);

    watch(() => props.item, (newItem) => {
        editedItem.value = { ...newItem };
    });

    const formattedPurchasedate = computed({
        get: () => editedItem.value.Purchasedate.split('T')[0],
        set: (value: string) => editedItem.value.Purchasedate = value + "T00:00:00"
    });

    const formattedPayDate = computed({
        get: () => editedItem.value.PayDate.split('T')[0],
        set: (value: string) => editedItem.value.PayDate = value + "T00:00:00"
    });

    const submitform = () => {
        emit('update', editedItem.value);
    };

    const canceledit = () => {
        emit('cancel');
    };

    //const editDialog = ref(false);
    const isValid = ref(false);
    //const editedItem = reactive({
    //    purchaseDate: '',
    //    category: '',
    //    summary: '',
    //    purchaseAmount: 0,
    //    paymentDate: '',
    //    actualPaymentAmount: 0,
    //    requestor: '',
    //    payer: '',
    //    remarks: '',
    //    excludingTax: false,
    //    reconciled: false,
    //    year: 111
    //});

    const categories = ['類別1', '類別2', '類別3'];
    const requestors = ['請購人1', '請購人2', '請購人3'];
    const payers = ['支付人1', '支付人2', '支付人3'];

    const rules = {
        required: (value: any) => !!value || '此欄位必填'
    };

    //const openEditDialog = (item: any) => {
    //    Object.assign(editedItem, item);
    //    editDialog.value = true;
    //};

    //const closeEditDialog = () => {
    //    editDialog.value = false;
    //};

    //const saveEdit = () => {
    //    if ((refs.editForm as any).validate()) {
    //        // 進行儲存操作
    //        closeEditDialog();
    //    }
    //};
    onMounted(fetchUsers);
</script>

<style scoped>
    /* 其他樣式 */
</style>
