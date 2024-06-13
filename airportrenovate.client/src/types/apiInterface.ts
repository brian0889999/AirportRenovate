export interface LoginViewModel {
    Account: string,
    Password:string
}

export interface LoginUserModel {
    No?: number;
    Name?: string;
    Account?: string;
    Password?: string;
    Email?: string;
    Unit_No?: string;
    Auth?: string;
    Account_Open?: string;
    Reason?: string;
    Count?: number;
    Time?: Date;
    Time1?: Date;
    Status1?: string;
    Status2?: string;
    MEMO?: string;
    Status3?: string;
}

export interface BudgetModel {
    budget?: string | null;
    group?: string | null;
    subject6?: string | null;
    subject7?: string | null;
    subject8?: string | null;
    budgetYear?: number | null;
    final?: number | null;
    general?: number | null;
    out?: number | null;
    useBudget?: number | null;
    in?: number | null;
    inActual?: number | null;
    inBalance?: number | null;
    subjectActual?: number | null;
}

export interface SelectedBudgetModel {
    Budget?: string;
    Group?: string;
    Subject6?: string | null;
    Subject7?: string | null;
    Subject8?: string | null;
    BudgetYear?: number | null;
    Final?: number | null;
    General?: number | null;
    Out?: number | null;
    UseBudget?: number | null;
    In?: number | null;
    InActual?: number | null;
    InBalance?: number | null;
    SubjectActual?: number | null;
    End?: number | null;
    Text?: string;
    PurchaseMoney?: number;
    PayMoney?: number;
}
export interface UserDataModel {
    No?: number;
    Name?: string;
    Account?: string;
    Password?: string;
    Auth?: string;
    Status1?: string;
    Status2?: string;
    Status3?: string;
}

export interface MoneyItem {
    Budget?: string;
    Group?: string;
    Subject6?: string;
    Subject7?: string;
    Subject8?: string;
    BudgetYear?: number;
    Final?: number;
    Text?: string;
    PurchaseMoney?: number;
    PayMoney?: number;
    Purchasedate?: string;
}

export interface MoneyRawData {
    ID: number;
    Purchasedate?: string;
    Text?: string;
    Note?: string;
    PurchaseMoney?: number;
    PayDate?: string;
    PayMoney?: number;
    People?: string;
    Name?: string;
    Remarks?: string;
    People1?: string;
    ID1?: number;
    Status?: string;
    Group1?: string;
    Year?: number;
    Year1?: string;
    All?: string;
    True?: string;
    MoneyDbModel: {
        ID: number;
        Budget?: string;
        Group?: string;
        Subject6?: string;
        Subject7?: string;
        Subject8?: string;
        BudgetYear?: number;
        Final?: string;
        Year?: number;
    };
}