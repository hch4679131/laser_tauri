// 使用 Vuetify
Vue.use(Vuetify);

// 创建 Vuetify 实例
const vuetify = new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#1976D2', // 设置 primary 颜色
      },
    },
  },
});

new Vue({
  el: '#app', // 使用 el 挂载
  vuetify, // 注入 Vuetify
  data: {
    message: 'Hello Vue 2 with Tauri!',
  },
  template: `
    <div id="app">
      <h1>{{ message }}</h1>
      <p>Vuetify 2 已添加，现在使用 Vuetify 按钮。</p>
      <v-btn color="primary">
        这是一个 Vuetify 按钮
      </v-btn>
    </div>
  `,
});