using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LiepaLimited.Test.ConsoleApp.Dto;

namespace LiepaLimited.Test.ConsoleApp
{
    class Program
    {
        private const string User = "user";
        private const string Password = "password";
        private const string Url = "https://localhost:5001";
        private const string CreatePath = "/Auth/CreateUser";
        private const string RemovePath = "/Auth/RemoveUser";
        private const string SetStatusPath = "/Auth/SetStatus";
        private const string GetUserInfoPath = "/Public/UserInfo";

        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await Menu();
        }

        private static async Task Menu()
        {
            Console.WriteLine("Enter command id (1, 2, 3 or 4) or any key for exit:");
            Console.WriteLine("1 - CreateUser");
            Console.WriteLine("2 - RemoveUser");
            Console.WriteLine("3 - SetStatus");
            Console.WriteLine("4 - UserInfo");
            Console.WriteLine("0 - Exit");
            var input = Console.ReadLine();
            int commandId;
            var result = int.TryParse(input, out commandId);
            if (!result || commandId is < 1 or > 4)
            {
                Console.WriteLine("Incorrect command id. Try again.");
                Menu();
            }

            try
            {
                switch (commandId)
                {
                    case 1:
                        await CreateUser();
                        break;
                    case 2:
                        await RemoveUser();
                        break;
                    case 3:
                        await SetStatus();
                        break;
                    case 4:
                        await UserInfo();
                        break;
                    default:
                        Console.WriteLine("Have a nice day!");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Menu();
            }
           
        }


        private static async Task CreateUser()
        {
            Console.WriteLine("Enter user id:");
            var idString = Console.ReadLine();
            int id;
            var result = int.TryParse(idString, out id);
            if (!result)
            {
                Console.WriteLine("Incorrect user Id");
                await CreateUser();
            }
            Console.WriteLine("Enter user name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter user status (New, Active, Blocked or Deleted)");
            var status = Console.ReadLine();
            await CreateUserApi(id, name, status);
        }

        private static async Task CreateUserApi(int id, string name, string status)
        {
            var url = new Uri(Url + CreatePath);
            var request = new CreateUserRequestDto(id, name, status);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));
            AddAuthenticationHeader();
            var result = await client.PostAsXmlWithSerializerAsync(url, request);
            await PrintResult(result);
        }

        private static async Task RemoveUser()
        {
            Console.WriteLine("Enter user id:");
            var idString = Console.ReadLine();
            int id;
            var result = int.TryParse(idString, out id);
            if (!result)
            {
                Console.WriteLine("Incorrect user Id");
                await RemoveUser();
            }

            await RemoveUserApi(id);
        }

        private static async Task RemoveUserApi(int id)
        {
            var url = new Uri(Url + RemovePath);
            var request = new RemoveUserRequestDto(id);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            AddAuthenticationHeader();
            var result = await client.PostAsJsonAsync(url, request);
            await PrintResult(result);


        }

        private static async Task PrintResult(HttpResponseMessage result)
        {
            Console.WriteLine(result.StatusCode);
            Console.WriteLine(await result.Content.ReadAsStringAsync());
            await Menu();
        }

        private static async Task SetStatus()
        {
            Console.WriteLine("Enter user id:");
            var idString = Console.ReadLine();
            int id;
            var result = int.TryParse(idString, out id);
            if (!result)
            {
                Console.WriteLine("Incorrect user Id");
                await SetStatus();
            }
            Console.WriteLine("Enter user status (New, Active, Blocked or Deleted)");
            var status = Console.ReadLine();
            await SetStatusApi(id, status);
        }

        private static async Task SetStatusApi(int id, string status)
        {
            var nvc = new List<KeyValuePair<string, string>>();
            nvc.Add(new KeyValuePair<string, string>("Id", id.ToString()));
            nvc.Add(new KeyValuePair<string, string>("NewStatus", status));
            var url = new Uri(Url + SetStatusPath);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            AddAuthenticationHeader();
            var req = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(nvc) };
            var result = await client.SendAsync(req);
            await PrintResult(result);
        }

        private static async Task UserInfo()
        {
            Console.WriteLine("Enter user id:");
            var idString = Console.ReadLine();
            int id;
            var result = int.TryParse(idString, out id);
            if (!result)
            {
                Console.WriteLine("Incorrect user Id");
                await UserInfo();
            }
            OpenBrowser(Url + GetUserInfoPath + "?Id=" + id);
            await Menu();
        }

        private static void AddAuthenticationHeader()
        {
            var value = User + ":" + Password;
            var credential = Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);
        }


        private static void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
