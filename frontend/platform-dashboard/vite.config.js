import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';

export default defineConfig({
	plugins: [sveltekit()],
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	},
	resolve: {
		alias: {
			'@components': '/src/lib/components',
			'@utils': '/src/lib/utils',
			'@api': '/src/lib/api',
			'@stores': '/src/lib/stores'
		}
	}
});
