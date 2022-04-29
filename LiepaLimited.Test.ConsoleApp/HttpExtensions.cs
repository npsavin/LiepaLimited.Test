using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace LiepaLimited.Test.ConsoleApp
{
    internal static class HttpExtensions
    {
        public static Task<HttpResponseMessage> PostAsXmlWithSerializerAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return client.PostAsync(requestUri, value,
                new XmlMediaTypeFormatter { UseXmlSerializer = true });
        }
    }
}
