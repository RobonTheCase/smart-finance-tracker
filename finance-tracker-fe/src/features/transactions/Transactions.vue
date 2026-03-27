<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { api } from '../../services/api';

const transactions = ref<any[]>([]);

const form = ref({
  type: 'expense',
  amount: 0,
  category: '',
  notes: '',
  isRecurring: false,
  subType: '',
});

const loading = ref(false);

const loadTransactions = async () => {
  transactions.value = await api.getTransactions();
};

const submit = async () => {
  loading.value = true;

  await api.createTransaction(form.value);

  form.value = {
    type: 'expense',
    amount: 0,
    category: '',
    notes: '',
    isRecurring: false,
    subType: '',
  };

  await loadTransactions();
  await props.refreshDashboard();
  loading.value = false;
};

const remove = async (id: number) => {
  await api.deleteTransaction(id);
  await loadTransactions();
  await props.refreshDashboard();
};
const props = defineProps<{
  refreshDashboard: () => Promise<void>;
}>();

onMounted(loadTransactions);
</script>

<template>
  <div
    class="min-h-screen bg-gradient-to-br from-slate-900 via-slate-800 to-slate-900 p-6 text-slate-100 space-y-6"
  >
    <!-- Form -->
    <div
      class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5"
    >
      <h2 class="font-semibold mb-4 text-slate-200">Add Transaction</h2>

      <div class="grid grid-cols-2 gap-4">
        <select v-model="form.type" class="p-2 rounded-lg border">
          <option value="expense">Expense</option>
          <option value="income">Income</option>
          <option value="transfer">Transfer</option>
        </select>

        <select v-model="form.subType" class="p-2 rounded-lg border">
          <option value="">None</option>
          <option value="savings">Savings</option>
          <option value="investment">Investment</option>
        </select>

        <input
          v-model="form.amount"
          type="number"
          placeholder="Amount"
          class="p-2 rounded-lg border border-slate-600 bg-slate-700 text-slate-100 placeholder-slate-400"
        />

        <input
          v-model="form.category"
          placeholder="Category"
          class="p-2 rounded-lg border border-slate-600 bg-slate-700 text-slate-100 placeholder-slate-400"
        />

        <input
          v-model="form.notes"
          placeholder="Notes"
          class="p-2 rounded-lg border border-slate-600 bg-slate-700 text-slate-100 placeholder-slate-400"
        />

        <label class="flex items-center gap-2 col-span-2 text-slate-300">
          <input
            type="checkbox"
            v-model="form.isRecurring"
            class="accent-slate-500"
          />
          Recurring
        </label>

        <button
          @click="submit"
          class="col-span-2 bg-slate-600 text-slate-100 py-2 rounded-lg hover:bg-slate-500 transition"
        >
          {{ loading ? 'Adding...' : 'Add Transaction' }}
        </button>
      </div>
    </div>

    <!-- List -->
    <div
      class="bg-slate-800/60 backdrop-blur-lg border border-slate-700/50 shadow-xl rounded-2xl p-5"
    >
      <h2 class="font-semibold mb-4 text-slate-200">Transactions</h2>

      <div
        v-for="t in transactions"
        :key="t.id"
        class="flex justify-between items-center py-2 border-b border-slate-600 last:border-none"
      >
        <div>
          <p class="font-medium text-slate-100">{{ t.category }}</p>
          <p class="text-sm text-slate-400">{{ t.notes }}</p>
        </div>

        <div class="flex items-center gap-4">
          <span
            :class="t.type === 'expense' ? 'text-red-400' : 'text-green-400'"
            class="font-semibold"
          >
            {{ t.amount.toFixed(2) }}
          </span>

          <button
            @click="remove(t.id)"
            class="text-sm text-red-400 hover:text-red-300"
          >
            Delete
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
