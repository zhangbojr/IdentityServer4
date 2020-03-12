import axios from "axios";
import { Toast, Indicator } from "mint-ui";
import Oidc from "oidc-client";
var config = {
  authority: "http://localhost:5000",
  client_id: "js",
  redirect_uri: "http://localhost:8083/#/pages/home",
  response_type: "code",
  scope: "webApi",
  post_logout_redirect_uri: "http://localhost:8083/#/pages/logout",
  revokeAccessTokenOnSignout: true
};
let mgr = new Oidc.UserManager(config);
export default mgr;

axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if (error.message.includes("timeout")) {
      // 判断请求异常信息中是否含有超时timeout字符串
      console.log("错误回调", error); //alert("网络超时");
      Toast({
        message: "请求超时",
        position: "center",
        duration: 2000
      });
      return Promise.reject(error); // reject这个错误信息
    }
    if (error.response) {
      switch (error.response.status) {
        case 401:
          Indicator.close();
          Toast({
            message: "401",
            duration: 1000
          });
          break;
        case 500:
          Indicator.close();
          Toast({
            message: "请求异常，请稍后再试",
            duration: 1000
          });
          break;
        case 503:
          Indicator.close();
          Toast({
            message: "服务器繁忙，请稍后再试",
            duration: 1000
          });
          break;
      }
    }
    return Promise.reject(error.response.data); // 返回接口返回的错误信息
  }
);
