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
			{ find: '@utils', replacement: fileURLToPath(new URL('./src/lib/utils', import.meta.url)) },
			{ find: '@api', replacement: fileURLToPath(new URL('./src/lib/api', import.meta.url)) },
			{ find: '@stores', replacement: fileURLToPath(new URL('./src/lib/stores', import.meta.url)) }
		]
	}
});
