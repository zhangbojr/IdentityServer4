<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p><a href="#" @click="webApi">接口测试</a></p>
    <p><a href="#" @click="logout">退出</a></p>
    <p v-for="(item, index) in list" :key="index">{{ item }}</p>
  </div>
</template>

<script>
import Oidc from "oidc-client";
import mgr from "@/config/api";
import axios from "axios";
import { Toast, Indicator } from "mint-ui";
export default {
  name: "HelloWorld",
  data() {
    return {
      msg: "Welcome",
      list: null
    };
  },
  methods: {
    webApi() {
      var _this = this;
      Indicator.open();
      mgr.getUser().then(function(user) {
        if (user) {
          console.log("User logged in" + user.profile);

          let options = {};
          options.headers = {
            Authorization: "Bearer " + user.access_token
          };
          axios
            .get("http://localhost:5002/api/Identity", options)
            .then(res => {
              Indicator.close();
              Toast({
                message: "请求成功",
                position: "bottom",
                duration: 1000,
                className: "toasts"
              });
              _this.list = res.data;
            })
            .catch(error => {
              console.log(error);
            });
        } else {
          console.log("User not logged in");
        }
      });
    },
    logout() {
      let _this = this;
      mgr.signoutRedirect();
    }
  },
  mounted() {
    var _this = this;
    new Oidc.UserManager({ response_mode: "query" })
      .signinRedirectCallback()
      .then(function() {
        window.location = "/#/pages/Home";
      })
      .catch(function(e) {
        console.error(e);
        window.location = "http://localhost:8083/#/";
      });
  }
};
</script>
