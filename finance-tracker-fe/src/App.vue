<script setup lang="ts">
import { ref, onMounted } from 'vue';
import Dashboard from './features/dashboard/Dashboard.vue';
import Transactions from './features/transactions/Transactions.vue';
import { api } from './services/api';

const summary = ref(null);
const categories = ref([]);
const comparison = ref(null);
const insights = ref([]);

const loading = ref(true);
const view = ref<'dashboard' | 'transactions'>('dashboard');

const loadDashboard = async () => {
  loading.value = true;

  const [s, c, m, i] = await Promise.all([
    api.getSummary(),
    api.getCategories(),
    api.getMonthlyComparison(),
    api.getInsights(),
  ]);

  summary.value = s;
  categories.value = c;
  comparison.value = m;
  insights.value = i;

  loading.value = false;
};

onMounted(loadDashboard);
</script>

<template>
  <div>
    <div class="p-4 flex gap-4 bg-slate-900/80 backdrop-blur-lg border-b border-slate-700/50">
      <button
        @click="view = 'dashboard'"
        class="px-4 py-2 rounded-lg bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl text-slate-100 hover:bg-slate-700/60 transition"
        :class="{ 'bg-slate-700/60': view === 'dashboard' }"
      >
        Dashboard
      </button>
      <button
        @click="view = 'transactions'"
        class="px-4 py-2 rounded-lg bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl text-slate-100 hover:bg-slate-700/60 transition"
        :class="{ 'bg-slate-700/60': view === 'transactions' }"
      >
        Transactions
      </button>
    </div>

    <Dashboard
      v-if="view === 'dashboard'"
      :summary="summary"
      :categories="categories"
      :comparison="comparison"
      :insights="insights"
      :loading="loading"
    />

    <Transactions
      v-if="view === 'transactions'"
      :refreshDashboard="loadDashboard"
    />
  </div>
</template>
