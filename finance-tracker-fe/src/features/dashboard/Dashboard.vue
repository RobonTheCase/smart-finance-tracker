<script setup lang="ts">
import CategoryChart from '../../components/CategoryChart.vue';
import IncomeExpenseChart from '../../components/IncomeExpesneChart.vue';
import MonthlyTrendChart from '../../components/MonthlyTrendLineChart.vue';
defineProps<{
  summary: any;
  categories: any[];
  comparison: any;
  insights: string[];
  loading: boolean;
}>();
</script>

<template>
  <div
    class="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 p-6 text-slate-100"
  >
    <div v-if="loading" class="text-slate-300">Loading...</div>

    <div v-else class="space-y-6">
      <div class="grid grid-cols-5 gap-4">
        <div
          class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-4 transition hover:scale-[1.02]"
        >
          <p class="text-sm text-slate-400">Income</p>
          <p class="text-2xl font-semibold text-green-400">
            {{ summary?.totalIncome.toFixed(2) }}
          </p>
        </div>

        <div
          class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-4 transition hover:scale-[1.02]"
        >
          <p class="text-sm text-slate-400">Expenses</p>
          <p class="text-2xl font-semibold text-red-400">
            {{ summary?.totalExpense.toFixed(2) }}
          </p>
        </div>

        <div
          class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-4 transition hover:scale-[1.02]"
        >
          <p class="text-sm text-slate-400">Balance</p>
          <p class="text-2xl font-semibold text-blue-400">
            {{ summary?.balance.toFixed(2) }}
          </p>
        </div>
        <div
          class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-4 transition hover:scale-[1.02]"
        >
          <p class="text-sm text-gray-500">Savings</p>
          <p class="text-2xl font-semibold text-purple-500">
            {{ summary?.savings?.toFixed(2) || '0.00' }}
          </p>
        </div>
        <div
          class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-4 transition hover:scale-[1.02]"
        >
          <p class="text-sm text-gray-500">Invested</p>
          <p class="text-2xl font-semibold text-indigo-500">
            {{ summary?.investments?.toFixed(2) || '0.00' }}
          </p>
        </div>
      </div>

      <div
        class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5"
      >
        <h2 class="font-semibold mb-3 text-slate-200">Top Categories</h2>

        <div
          v-for="c in categories"
          :key="c.category"
          class="flex justify-between py-2 border-b border-slate-600 last:border-none"
        >
          <span class="text-slate-300">{{ c.category }}</span>
          <span class="font-medium text-slate-100">{{
            c.total.toFixed(2)
          }}</span>
        </div>
      </div>

      <div
        class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5"
      >
        <h2 class="font-semibold mb-3 text-slate-200">Monthly Comparison</h2>

        <p class="text-slate-300">
          This Month: {{ comparison?.thisMonth.toFixed(2) }}
        </p>
        <p class="text-slate-300">
          Last Month: {{ comparison?.lastMonth.toFixed(2) }}
        </p>

        <p
          class="mt-2 font-semibold"
          :class="
            comparison?.difference > 0 ? 'text-red-400' : 'text-green-400'
          "
        >
          {{ comparison?.difference > 0 ? '↑ Spending up' : '↓ Spending down' }}
          ({{ comparison?.difference.toFixed(2) }})
        </p>
      </div>

      <div
        class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5"
      >
        <h2 class="font-semibold mb-3 text-slate-200">Insights</h2>

        <ul class="space-y-2">
          <li
            v-for="i in insights"
            :key="i"
            class="bg-slate-700/50 rounded-lg px-3 py-2 text-sm shadow-sm text-slate-200"
          >
            {{ i }}
          </li>
        </ul>
      </div>
    </div>
    <div
      class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5 mt-5"
    >
      <h2 class="font-semibold mb-5 text-slate-200">Spending Breakdown</h2>

      <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
        <div class="h-[260px] flex items-center justify-center">
          <CategoryChart :data="categories.slice(0, 5)" />
        </div>

        <div class="h-[260px] flex items-center justify-center">
          <IncomeExpenseChart
            :income="summary?.totalIncome || 0"
            :expense="summary?.totalExpense || 0"
          />
        </div>

        <div class="h-[260px] flex items-center justify-center">
          <MonthlyTrendChart
            :thisMonth="comparison?.thisMonth || 0"
            :lastMonth="comparison?.lastMonth || 0"
          />
        </div>
      </div>
    </div>
  </div>
</template>
