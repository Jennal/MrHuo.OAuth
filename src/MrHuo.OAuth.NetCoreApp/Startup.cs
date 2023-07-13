using System;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MrHuo.OAuth.NetCoreApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddSession();
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                //options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            }); ;

            HttpRequestApi.EnableDebugLog = true;

            //����������¼���ע���ȥ
            services.AddSingleton(new Baidu.BaiduOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:baidu")));
            services.AddSingleton(new Wechat.WechatOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:wechat")));
            services.AddSingleton(new Gitlab.GitlabOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:gitlab")));
            services.AddSingleton(new Gitee.GiteeOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:gitee")));
            services.AddSingleton(new Github.GithubOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:github")));
            services.AddSingleton(new Huawei.HuaweiOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:huawei")));
            services.AddSingleton(new Coding.CodingOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:coding"), Configuration["oauth:coding:team"]));
            services.AddSingleton(new SinaWeibo.SinaWeiboOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:sinaweibo")));
            services.AddSingleton(new Alipay.AlipayOAuth(
                OAuthConfig.LoadFrom(Configuration, "oauth:alipay"),
                Configuration["oauth:alipay:private_key"],
                Configuration["oauth:alipay:public_key"],
                Configuration["oauth:alipay:encrypt_key"]
            ));
            services.AddSingleton(new QQ.QQOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:qq")));
            services.AddSingleton(new OSChina.OSChinaOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:oschina")));
            services.AddSingleton(new DouYin.DouYinOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:douyin")));
            services.AddSingleton(new WechatOpen.WechatOpenOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:wechatopen")));
            services.AddSingleton(new MeiTuan.MeiTuanOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:meituan")));
            services.AddSingleton(new XunLei.XunLeiOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:xunlei")));
            services.AddSingleton(new DingTalk.DingTalkOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:dingtalk")));
            services.AddSingleton(new DingTalkQrcode.DingTalkQrcodeOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:dingtalkqrcode")));
            services.AddSingleton(new Microsoft.MicrosoftOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:microsoft")));
            services.AddSingleton(new Mi.MiOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:mi")));
            // services.AddSingleton(new StackOverflow.StackOverflowOAuth(
            //     OAuthConfig.LoadFrom(Configuration, "oauth:stackoverflow"),
            //     Configuration["oauth:stackoverflow:api_key"])
            // );
            services.AddSingleton(new Facebook.FacebookOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:facebook")));
            services.AddSingleton(new Google.GoogleOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:google")));
            services.AddSingleton(new LinkedIn.LinkedInOAuth(OAuthConfig.LoadFrom(Configuration, "oauth:linkedin")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
