<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { Chart } from 'chart.js/auto';

const props = defineProps<{
  income: number;
  expense: number;
}>();

const canvasRef = ref<HTMLCanvasElement | null>(null);
let chart: Chart | null = null;

const colors = ['rgba(100, 116, 139, 0.8)', 'rgba(71, 85, 105, 0.7)'];

const renderChart = () => {
  if (!canvasRef.value) return;

  if (chart) chart.destroy();

  chart = new Chart(canvasRef.value, {
    type: 'bar',
    data: {
      labels: ['Income', 'Expenses'],
      datasets: [
        {
          data: [props.income, props.expense],
          backgroundColor: colors,
        },
      ],
    },
    options: {
      plugins: {
        legend: { display: false },
      },
    },
  });
};

onMounted(renderChart);
watch(() => [props.income, props.expense], renderChart);
</script>

<template>
  <canvas ref="canvasRef"></canvas>
</template>
