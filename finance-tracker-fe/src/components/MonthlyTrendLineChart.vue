<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { Chart } from 'chart.js/auto';

const props = defineProps<{
  thisMonth: number;
  lastMonth: number;
}>();

const canvasRef = ref<HTMLCanvasElement | null>(null);
let chart: Chart | null = null;

const color = 'rgba(51, 65, 85, 0.6)';

const renderChart = () => {
  if (!canvasRef.value) return;

  if (chart) chart.destroy();

  chart = new Chart(canvasRef.value, {
    type: 'line',
    data: {
      labels: ['Last Month', 'This Month'],
      datasets: [
        {
          label: 'Spending',
          data: [props.lastMonth, props.thisMonth],
          borderColor: color,
          backgroundColor: color,
          tension: 0.4,
          fill: true,
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
watch(() => [props.thisMonth, props.lastMonth], renderChart);
</script>

<template>
  <canvas ref="canvasRef"></canvas>
</template>
