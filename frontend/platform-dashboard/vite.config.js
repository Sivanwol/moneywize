import { sveltekit } from '@sveltejs/kit/vite';
import { defineConfig } from 'vitest/config';
import { fileURLToPath, URL } from 'url';

export default defineConfig({
	plugins: [sveltekit()],
	test: {
		include: ['src/**/*.{test,spec}.{js,ts}']
	},
	resolve: {
		alias: [
			{ find: '@components', replacement: fileURLToPath(new URL('./src/lib/components', import.meta.url)) },
			{ find: '@utility', replacement: fileURLToPath(new URL('./src/lib/utility', import.meta.url)) },
			{ find: '@api', replacement: fileURLToPath(new URL('./src/lib/api', import.meta.url)) },
			{ find: '@stores', replacement: fileURLToPath(new URL('./src/lib/stores', import.meta.url)) },
			{ find: '@models', replacement: fileURLToPath(new URL('./src/lib/models', import.meta.url)) },
			{ find: '@translations', replacement: fileURLToPath(new URL('./src/lib/translations', import.meta.url)) },
			{ find: '@data', replacement: fileURLToPath(new URL('./src/data', import.meta.url)) }
		]
	},
	build: {
		sourcemap: true
	}
});
