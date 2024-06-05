export interface LoginViewModel {
    Account: string,
    Password:string
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



export interface SelectedBudgetDataModel {
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
    End?: number | null;
}


export interface BudgetDataModel {
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


export interface LoginUserModel {
    no?: number;
    name?: string;
    account?: string;
    password?: string;
    email?: string;
    unit_No?: string;
    auth?: string;
    account_open?: string;
    reason?: string;
    count?: number;
    time?: Date;
    time1?: Date;
    status1?: string;
    status2?: string;
    memo?: string;
    status3?: string;
}