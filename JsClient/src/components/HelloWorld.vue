<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <ul>
      <li>
        <a href="#" @click="toHome">登录</a>
      </li>
    </ul>
  </div>
</template>

<script>
import Vue from "Vue";
import mgr from "@/config/api";
import { Toast, Indicator } from "mint-ui";
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome to Your  App"
    };
  },
  methods: {
    toHome() {
      let _this = this;
      mgr.signinRedirect().catch(error => {
        Toast(error);
      });
    }
  },
  beforeCreate() {}
};
</script>
<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
