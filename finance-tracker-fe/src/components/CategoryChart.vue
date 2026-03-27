<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import { Chart } from 'chart.js/auto';

const props = defineProps<{
  data: { category: string; total: number }[];
}>();

const canvasRef = ref<HTMLCanvasElement | null>(null);
let chart: Chart | null = null;

const renderChart = () => {
  if (!canvasRef.value) return;

  if (chart) {
    chart.destroy();
  }

  chart = new Chart(canvasRef.value, {
    type: 'doughnut',
    data: {
      labels: props.data.map((c) => c.category),
      datasets: [
        {
          data: props.data.map((c) => c.total),
          backgroundColor: [
            'rgba(100, 116, 139, 0.8)',
            'rgba(71, 85, 105, 0.7)',
            'rgba(51, 65, 85, 0.6)',
            'rgba(30, 41, 59, 0.5)',
            'rgba(15, 23, 42, 0.4)',
          ],
        },
      ],
    },
    options: {
      backgroundColor: 'transparent',
      plugins: {
        legend: {
          position: 'bottom',
          labels: {
            color: '#f1f5f9',
          },
        },
      },
    },
  });
};

onMounted(renderChart);
watch(() => props.data, renderChart);
</script>

<template>
  <canvas ref="canvasRef" class="bg-transparent"></canvas>
</template>
