import adapter from '@sveltejs/adapter-auto';
import { vitePreprocess } from '@sveltejs/vite-plugin-svelte';
import path from 'path';

/** @type {import('@sveltejs/kit').Config} */
const config = {
	kit: {
		adapter: adapter(),
		alias: {
			'@components': path.resolve('./src/lib/components'),
			'@stores': path.resolve('./src/lib/stores'),
			'@api': path.resolve('./src/lib/api'),
			'@utility': path.resolve('./src/lib/utility')
		},
		prerender: {
			crawl: true
		}
	},

	preprocess: vitePreprocess()
};

export default config;
