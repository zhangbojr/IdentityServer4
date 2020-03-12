import Vue from "vue";
import Router from "vue-router";
import HelloWorld from "@/components/HelloWorld";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "HelloWorld",
      component: HelloWorld
    },
    {
      //主页
      path: "/pages/home",
      name: "home",
      component: resolve => require(["@/pages/home"], resolve)
    },
    {
      //主页
      path: "/pages/logout",
      name: "logout",
      component: resolve => require(["@/pages/logout"], resolve)
    }
  ]
});
