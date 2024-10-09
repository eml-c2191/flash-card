using FlashCard.Abstract.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCard.Abstract.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T?> GetObjectAsync<T>(this HttpClient client, string route)
        {
            HttpResponseMessage response = await client.GetAsync(route);

            if (response.IsSuccessStatusCode)
            {
                string stringData = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<T>(stringData);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                string stringData = await response.Content.ReadAsStringAsync();
                BadRequestResponse? badRequestResponse = JsonConvert.DeserializeObject<BadRequestResponse>(stringData);

                throw new InvalidRequestException(badRequestResponse?.Detail ?? "Invalid request.");
            }

            return default;
        }

        /// <summary>
        /// Post with object return
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="client"></param>
        /// <param name="route"></param>
        /// <param name="request"></param>]
        /// <returns></returns>
        public static async Task<TResponse?> PostWithObjectReturnAsync<TResponse, TRequest>
        (
            this HttpClient client,
            string route,
            TRequest request
        )
        {
            StringContent stringContent = request.ConvertToStringContent();

            HttpResponseMessage response = await client.PostAsync(route, stringContent);

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                try
                {
                    TResponse? responseData = JsonConvert.DeserializeObject<TResponse>(data);
                    return responseData;
                }
                catch
                {
                    return default;
                }
            }

            return default;
        }

        /// <summary>
        /// Post with object return
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="client"></param>
        /// <param name="route"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static async Task<TResponse?> PostWithFormUrlEncodedAsync<TResponse>
        (
            this HttpClient client,
            string route,
            IDictionary<string, string> body
        )
        {
            var formData = new FormUrlEncodedContent(body);

            // Set the Content-Type header to application/x-www-form-urlencoded
            formData.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");

            // Send POST request with the form data
            HttpResponseMessage response = await client.PostAsync(route, formData);

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                // Handle successful response
                string responseContent = await response.Content.ReadAsStringAsync();
                TResponse? responseData = JsonConvert.DeserializeObject<TResponse>(responseContent);
                return responseData;
            }

            return default;
        }

        /// <summary>
        /// Post with object return
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="client"></param>
        /// <param name="route"></param>
        /// <param name="formData"></param>
        /// <returns></returns>
        public static async Task<TResponse?> PostWithFormDataAsync<TResponse>
        (
            this HttpClient client,
            string route,
            MultipartFormDataContent formData
        )
        {
            // Send the request
            HttpResponseMessage response = await client.PostAsync(route, formData);

            // Check the response
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                TResponse? responseData = JsonConvert.DeserializeObject<TResponse>(responseBody);
                return responseData;
            }

            return default;
        }

        /// <summary>
        /// Convert to string json content
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static StringContent ConvertToStringContent<TRequest>(this TRequest request)
        {
            string jsonString = JsonConvert.SerializeObject(request);
            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }

    /// <summary>
    /// Bad request response
    /// </summary>

    public record BadRequestResponse
    {
        /// <summary>
        /// Detail
        /// </summary>
        public string? Detail { get; set; }
    }

}
