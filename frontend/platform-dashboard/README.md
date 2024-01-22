# create-svelte

Everything you need to build a Svelte project, powered
by [`create-svelte`](https://github.com/sveltejs/kit/tree/main/packages/create-svelte).

## Creating a project

If you're seeing this, you've probably already done this step. Congrats!

```bash
# create a new project in the current directory
npm create svelte@latest

# create a new project in my-app
npm create svelte@latest my-app
```

## Developing

Once you've created a project and installed dependencies with `npm install` (or `pnpm install` or `yarn`), start a
development server:

```bash
npm run dev

# or start the server and open the app in a new browser tab
npm run dev -- --open
```

## Building

To create a production version of your app:

```bash
npm run build
```

You can preview the production build with `npm run preview`.

> To deploy your app, you may need to install an [adapter](https://kit.svelte.dev/docs/adapters) for your target
> environment.

## Unique Views

as part of the system there what called views that linkined to what user can see that is a list is have direct connect
to UUID that on the database on the view table (ConfigurableUserViews Entity).

on the env file is the direct link to the view that the user will see.

the pattern is ``PUBLIC_VIEWS_{VIEW_NAME}={{uuid}}`` and the value is the UUID of the view.

for example if we want to see the view of the user list we need to add to the env file and the value is the UUID of the
view.

```dotenv
PUBLIC_VIEWS_USER_ACTIVITY=6769476B-82BE-45BE-AD50-94CC94478590
```

the uuid it represent the column of clientUniqueKey on the ConfigurableUserViews Entity.
for every view that we want to see we need to add the env variable.
that is the based view of that view that we will see later user able to play with it.
note. also base view can be share as it base entity and not actual view.

