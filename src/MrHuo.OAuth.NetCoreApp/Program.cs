using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MrHuo.OAuth.NetCoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SetCertificatePolicy();
            CreateHostBuilder(args).Build().Run();
        }
        public static void SetCertificatePolicy()
        {
            //����������п����������ʣ���code�г��ִ���ʱ��������fiddle�����������ʵ�SSl������֤�İ汾��
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:53000");
                });
    }
}
