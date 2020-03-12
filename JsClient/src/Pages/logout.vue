<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
  </div>
</template>

<script>
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "再见！"
    };
  },
  methods() {},
  mounted() {
    console.log("页面加载完成");
  },
  created() {
    console.log("页面初始化");
  },
  destroyed() {
    console.log("页面销毁");
  }
};
</script>
