export interface Summary {
  totalIncome: number;
  totalExpense: number;
  balance: number;
  savings?: number;
  investments?: number;
}

export interface Category {
  category: string;
  total: number;
}

export interface MonthlyComparison {
  thisMonth: number;
  lastMonth: number;
  difference: number;
}
