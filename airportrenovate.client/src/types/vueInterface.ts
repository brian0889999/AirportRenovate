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

export interface UserDataModel {
    no?: number;
    name?: string;
    account?: string;
    password?: string;
    status1?: string;
    status2?: string;
    status3?: string;
}

export interface Crumb {
    title: string;
    icon?: string;
    href: string;
    childs?: Crumb[];
}

export interface Breadcrumb {
    title?: string;
    disabled: boolean;
    href: string;
}

export interface RouteItemMeta {
    title?: string;
    icon?: string;
    disabled?: string;
}