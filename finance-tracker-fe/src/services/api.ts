const API_URL = 'http://localhost:5116/api';

export const api = {
  async getSummary() {
    const res = await fetch(`${API_URL}/dashboard/summary`);
    return res.json();
  },

  async getCategories() {
    const res = await fetch(`${API_URL}/dashboard/categories`);
    return res.json();
  },

  async getMonthlyComparison() {
    const res = await fetch(`${API_URL}/dashboard/monthly-comparison`);
    return res.json();
  },

  async getInsights() {
    const res = await fetch(`${API_URL}/dashboard/insights`);
    return res.json();
  },

  async getTransactions() {
    const res = await fetch(`${API_URL}/transactions`);
    return res.json();
  },

  async createTransaction(data: any) {
    const res = await fetch(`${API_URL}/transactions`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(data),
    });
    return res.json();
  },

  async deleteTransaction(id: number) {
    await fetch(`${API_URL}/transactions/${id}`, {
      method: 'DELETE',
    });
  },
};
