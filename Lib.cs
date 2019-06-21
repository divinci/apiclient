using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Globalization;
using Converters = Newtonsoft.Json.Converters;
using System.Linq;
using System.Runtime.Serialization;

namespace blaze.api
{
    public partial class Client
    {
        private string _baseUrl = "";
        private HttpClient _httpClient;
        private Lazy<JsonSerializerSettings> _settings;

        public Client(string baseUrl, HttpClient httpClient)
        {
            BaseUrl = baseUrl;
            _httpClient = httpClient;
            Settings = new Lazy<JsonSerializerSettings>(() =>
            {
                var settings = new JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }

        public string BaseUrl
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }

        protected JsonSerializerSettings JsonSerializerSettings { get { return Settings.Value; } }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings);
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(HttpClient client, HttpResponseMessage response);

        /// <summary>Creates a Company Account Holder</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAccountHolder> CreateCompanyAccountHolderAsync(CompanyAccountHolder body)
        {
            return CreateCompanyAccountHolderAsync(body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Creates a Company Account Holder</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAccountHolder> CreateCompanyAccountHolderAsync(CompanyAccountHolder body, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Company Account Holders in your scope.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<CompanyAccountHolder>> GetCompanyAccountHoldersAsync()
        {
            return GetCompanyAccountHoldersAsync(CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Company Account Holders in your scope.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<CompanyAccountHolder>> GetCompanyAccountHoldersAsync(CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<CompanyAccountHolder>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAccountHolder> GetCompanyAccountHolderAsync(Guid companyAccountHolderUid)
        {
            return GetCompanyAccountHolderAsync(companyAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAccountHolder> GetCompanyAccountHolderAsync(Guid companyAccountHolderUid, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Updates a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAccountHolder> UpdateCompanyAccountHolderAsync(Guid companyAccountHolderUid, CompanyAccountHolder body)
        {
            return UpdateCompanyAccountHolderAsync(companyAccountHolderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Updates a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAccountHolder> UpdateCompanyAccountHolderAsync(Guid companyAccountHolderUid, CompanyAccountHolder body, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("PUT");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds an address to a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAddress> CreateCompanyAccountHolderAddressAsync(Guid companyAccountHolderUid, CompanyAddress body)
        {
            return CreateCompanyAccountHolderAddressAsync(companyAccountHolderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds an address to a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAddress> CreateCompanyAccountHolderAddressAsync(Guid companyAccountHolderUid, CompanyAddress body, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}/addresses");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAddress>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all addresses of a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The Addresses belonging to the Company Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<CompanyAddress>> GetCompanyAccountHolderAddressesAsync(Guid companyAccountHolderUid)
        {
            return GetCompanyAccountHolderAddressesAsync(companyAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all addresses of a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The Addresses belonging to the Company Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<CompanyAddress>> GetCompanyAccountHolderAddressesAsync(Guid companyAccountHolderUid, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}/addresses");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<CompanyAddress>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets an address of a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Company Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAddress> GetCompanyAccountHolderAddressAsync(Guid companyAccountHolderUid, Guid addressUid)
        {
            return GetCompanyAccountHolderAddressAsync(companyAccountHolderUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets an address of a Company Account Holder</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Company Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAddress> GetCompanyAccountHolderAddressAsync(Guid companyAccountHolderUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}/addresses/{addressUid}");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAddress>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Submits the Company Account Holder onboarding questionnaire</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The created CompanyQuestionnaire with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyQuestionnaire> CreateCompanyAccountHolderQuestionnaireAsync(Guid companyAccountHolderUid, CompanyQuestionnaire body)
        {
            return CreateCompanyAccountHolderQuestionnaireAsync(companyAccountHolderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Submits the Company Account Holder onboarding questionnaire</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The created CompanyQuestionnaire with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyQuestionnaire> CreateCompanyAccountHolderQuestionnaireAsync(Guid companyAccountHolderUid, CompanyQuestionnaire body, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}/questionnaire");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("PUT");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyQuestionnaire>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets the Company Account Holder onboarding questionnaire.</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> GetCompanyAccountHolderQuestionnaireAsync(Guid companyAccountHolderUid)
        {
            return GetCompanyAccountHolderQuestionnaireAsync(companyAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets the Company Account Holder onboarding questionnaire.</summary>
        /// <param name="companyAccountHolderUid">The Uid of the Company Account Holder</param>
        /// <returns>The Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> GetCompanyAccountHolderQuestionnaireAsync(Guid companyAccountHolderUid, CancellationToken cancellationToken)
        {
            if (companyAccountHolderUid == null)
                throw new ArgumentNullException("companyAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/company/{companyAccountHolderUid}/questionnaire");
            urlBuilder_.Replace("{companyAccountHolderUid}", Uri.EscapeDataString(ConvertToString(companyAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Creates a Personal Account Holder</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<PersonalAccountHolder> CreatePersonalAccountHolderAsync(PersonalAccountHolder body)
        {
            return CreatePersonalAccountHolderAsync(body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Creates a Personal Account Holder</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<PersonalAccountHolder> CreatePersonalAccountHolderAsync(PersonalAccountHolder body, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<PersonalAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Personal Account Holders in your scope.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<PersonalAccountHolder>> GetPersonalAccountHoldersAsync()
        {
            return GetPersonalAccountHoldersAsync(CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Personal Account Holders in your scope.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<PersonalAccountHolder>> GetPersonalAccountHoldersAsync(CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<PersonalAccountHolder>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Updates a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<PersonalAccountHolder> UpdatePersonalAccountHolderAsync(Guid personalAccountHolderUid, PersonalAccountHolder body)
        {
            return UpdatePersonalAccountHolderAsync(personalAccountHolderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Updates a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<PersonalAccountHolder> UpdatePersonalAccountHolderAsync(Guid personalAccountHolderUid, PersonalAccountHolder body, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("PUT");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<PersonalAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<PersonalAccountHolder> GetPersonalAccountHolderAsync(Guid personalAccountHolderUid)
        {
            return GetPersonalAccountHolderAsync(personalAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<PersonalAccountHolder> GetPersonalAccountHolderAsync(Guid personalAccountHolderUid, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<PersonalAccountHolder>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds an address to a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> CreatePersonalAccountHolderAddressAsync(Guid personalAccountHolderUid, Address body)
        {
            return CreatePersonalAccountHolderAddressAsync(personalAccountHolderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds an address to a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> CreatePersonalAccountHolderAddressAsync(Guid personalAccountHolderUid, Address body, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}/addresses");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all addresses of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The Addresses belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<Address>> GetPersonalAccountHolderAddressesAsync(Guid personalAccountHolderUid)
        {
            return GetPersonalAccountHolderAddressesAsync(personalAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all addresses of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The Addresses belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<Address>> GetPersonalAccountHolderAddressesAsync(Guid personalAccountHolderUid, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}/addresses");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<Address>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets an address of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> GetPersonalAccountHolderAddressAsync(Guid personalAccountHolderUid, Guid addressUid)
        {
            return GetPersonalAccountHolderAddressAsync(personalAccountHolderUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets an address of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> GetPersonalAccountHolderAddressAsync(Guid personalAccountHolderUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}/addresses/{addressUid}");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Sets an address of a Personal Account Holder as Current</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address has been set as Current</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task SetPersonalAccountHolderAddressAsCurrentAsync(Guid personalAccountHolderUid, Guid addressUid)
        {
            return SetPersonalAccountHolderAddressAsCurrentAsync(personalAccountHolderUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Sets an address of a Personal Account Holder as Current</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address has been set as Current</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task SetPersonalAccountHolderAddressAsCurrentAsync(Guid personalAccountHolderUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}/addresses/{addressUid}/setascurrent");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new HttpMethod("POST");

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            return;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets the current address of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The Current Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> GetCurrentPersonalAccountHolderAddressAsync(Guid personalAccountHolderUid)
        {
            return GetCurrentPersonalAccountHolderAddressAsync(personalAccountHolderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets the current address of a Personal Account Holder</summary>
        /// <param name="personalAccountHolderUid">The Uid of the Personal Account Holder</param>
        /// <returns>The Current Address belonging to the Personal Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> GetCurrentPersonalAccountHolderAddressAsync(Guid personalAccountHolderUid, CancellationToken cancellationToken)
        {
            if (personalAccountHolderUid == null)
                throw new ArgumentNullException("personalAccountHolderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/personal/{personalAccountHolderUid}/addresses/current");
            urlBuilder_.Replace("{personalAccountHolderUid}", Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Creates a new Ledger for an Account Holder with the specified Currency or Money Type.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Ledger> CreateAccountHolderLedgerAsync(Guid holderUid, Ledger body)
        {
            return CreateAccountHolderLedgerAsync(holderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Creates a new Ledger for an Account Holder with the specified Currency or Money Type.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Ledger> CreateAccountHolderLedgerAsync(Guid holderUid, Ledger body, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Ledger>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Ledgers for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<Ledger>> GetAccountHolderLedgersAsync(Guid holderUid)
        {
            return GetAccountHolderLedgersAsync(holderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Ledgers for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<Ledger>> GetAccountHolderLedgersAsync(Guid holderUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<Ledger>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Ledger for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Ledger> GetAccountHolderLedgerAsync(Guid holderUid, Guid ledgerUid)
        {
            return GetAccountHolderLedgerAsync(holderUid, ledgerUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Ledger for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Ledger> GetAccountHolderLedgerAsync(Guid holderUid, Guid ledgerUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Ledger>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Ledger Bank Accounts for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<BankAccount>> GetLedgerBankAccountsAsync(Guid holderUid, Guid ledgerUid)
        {
            return GetLedgerBankAccountsAsync(holderUid, ledgerUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Ledger Bank Accounts for an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<BankAccount>> GetLedgerBankAccountsAsync(Guid holderUid, Guid ledgerUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/bankaccounts");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<BankAccount>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Ledger's Bank Account</summary>
        /// <param name="holderUid">The Uid of the Company Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <param name="uid">The Uid of the Bank Account</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<BankAccount> GetLedgerBankAccountAsync(Guid holderUid, Guid ledgerUid, Guid uid)
        {
            return GetLedgerBankAccountAsync(holderUid, ledgerUid, uid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Ledger's Bank Account</summary>
        /// <param name="holderUid">The Uid of the Company Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <param name="uid">The Uid of the Bank Account</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<BankAccount> GetLedgerBankAccountAsync(Guid holderUid, Guid ledgerUid, Guid uid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            if (uid == null)
                throw new ArgumentNullException("uid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/bankaccounts/{uid}");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{uid}", Uri.EscapeDataString(ConvertToString(uid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<BankAccount>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Creates a new Bank Account for an Account Holder's Ledger</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<BankAccount> CreateLedgerBankAccountAsync(Guid holderUid, Guid ledgerUid)
        {
            return CreateLedgerBankAccountAsync(holderUid, ledgerUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Creates a new Bank Account for an Account Holder's Ledger</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<BankAccount> CreateLedgerBankAccountAsync(Guid holderUid, Guid ledgerUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/bankaccounts/create");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<BankAccount>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Initialises a new Transaction for either internal ledger transfer or external beneficiary payments.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Transaction> CreateTransactionAsync(Transaction body)
        {
            return CreateTransactionAsync(body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Initialises a new Transaction for either internal ledger transfer or external beneficiary payments.</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Transaction> CreateTransactionAsync(Transaction body, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/transactions");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Transaction>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Creates a Associate</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Associate> CreateAssociateAsync(Associate body)
        {
            return CreateAssociateAsync(body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Creates a Associate</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Associate> CreateAssociateAsync(Associate body, CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Associate>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Associate> GetAssociateAsync(Guid associateUid)
        {
            return GetAssociateAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Associate> GetAssociateAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Associate>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds an address to a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> CreateAssociateIndividualAddressAsync(Guid associateUid, Address body)
        {
            return CreateAssociateIndividualAddressAsync(associateUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds an address to a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> CreateAssociateIndividualAddressAsync(Guid associateUid, Address body, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/addresses");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all addresses of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Addresses belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<Address>> GetAssociateIndividualAddressesAsync(Guid associateUid)
        {
            return GetAssociateIndividualAddressesAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all addresses of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Addresses belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<Address>> GetAssociateIndividualAddressesAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/addresses");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<Address>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets an address of a Associate Individual</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> GetPersonalAssociateAddressAsync(Guid associateUid, Guid addressUid)
        {
            return GetPersonalAssociateAddressAsync(associateUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets an address of a Associate Individual</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> GetPersonalAssociateAddressAsync(Guid associateUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/addresses/{addressUid}");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Sets an address of an Associate Individual as Current</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address has been set as Current</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task SetAssociateIndividualAddressAsCurrentAsync(Guid associateUid, Guid addressUid)
        {
            return SetAssociateIndividualAddressAsCurrentAsync(associateUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Sets an address of an Associate Individual as Current</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Address has been set as Current</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task SetAssociateIndividualAddressAsCurrentAsync(Guid associateUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/addresses/{addressUid}/setascurrent");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Content = new StringContent(string.Empty, System.Text.Encoding.UTF8, "application/json");
                    request_.Method = new HttpMethod("POST");

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            return;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets the current address of a Associate Individual</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Current Address belonging to the Associate Individual</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Address> GetCurrentPersonalAssociateAddressAsync(Guid associateUid)
        {
            return GetCurrentPersonalAssociateAddressAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets the current address of a Associate Individual</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Current Address belonging to the Associate Individual</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Address> GetCurrentPersonalAssociateAddressAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/addresses/current");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Address>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds Bank Account details to a Associate.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<AssociateBankAccount> CreateAssociateBankAccountAsync(Guid associateUid, AssociateBankAccount body)
        {
            return CreateAssociateBankAccountAsync(associateUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds Bank Account details to a Associate.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<AssociateBankAccount> CreateAssociateBankAccountAsync(Guid associateUid, AssociateBankAccount body, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/bankaccounts");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssociateBankAccount>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Associate Bank Accounts</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<AssociateBankAccount>> GetBeneficiaryBankAccountsAsync(Guid associateUid)
        {
            return GetBeneficiaryBankAccountsAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Associate Bank Accounts</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<AssociateBankAccount>> GetBeneficiaryBankAccountsAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/bankaccounts");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<AssociateBankAccount>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Associate Bank Account</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<AssociateBankAccount> GetAssociateUidBankAccountAsync(Guid associateUid, Guid bankAccountUid)
        {
            return GetAssociateUidBankAccountAsync(associateUid, bankAccountUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Associate Bank Account</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<AssociateBankAccount> GetAssociateUidBankAccountAsync(Guid associateUid, Guid bankAccountUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (bankAccountUid == null)
                throw new ArgumentNullException("bankAccountUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/bankaccounts/{bankAccountUid}");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{bankAccountUid}", Uri.EscapeDataString(ConvertToString(bankAccountUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<AssociateBankAccount>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds a Company address to a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created Company Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAddress> CreateAssociateCompanyAddressAsync(Guid associateUid, CompanyAddress body)
        {
            return CreateAssociateCompanyAddressAsync(associateUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds a Company address to a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created Company Address with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAddress> CreateAssociateCompanyAddressAsync(Guid associateUid, CompanyAddress body, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/companyaddresses");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAddress>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Company Addresses of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Company Addresses belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<CompanyAddress>> GetAssociateCompanyAddressesAsync(Guid associateUid)
        {
            return GetAssociateCompanyAddressesAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Company Addresses of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Company Addresses belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<CompanyAddress>> GetAssociateCompanyAddressesAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/companyaddresses");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<CompanyAddress>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Company Address of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Company Address belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<CompanyAddress> GetAssociateCompanyAddressAsync(Guid associateUid, Guid addressUid)
        {
            return GetAssociateCompanyAddressAsync(associateUid, addressUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Company Address of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="addressUid">The Uid of the Address</param>
        /// <returns>The Company Address belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<CompanyAddress> GetAssociateCompanyAddressAsync(Guid associateUid, Guid addressUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (addressUid == null)
                throw new ArgumentNullException("addressUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/companyaddresses/{addressUid}");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{addressUid}", Uri.EscapeDataString(ConvertToString(addressUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<CompanyAddress>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all an Account Holder's checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>All Checks belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<AccountHolderCheck>> GetAccountHolderChecksAsync(Guid holderUid)
        {
            return GetAccountHolderChecksAsync(holderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all an Account Holder's checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>All Checks belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<AccountHolderCheck>> GetAccountHolderChecksAsync(Guid holderUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/checks");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<AccountHolderCheck>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Bank Accounts checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>All Checks belonging to the Account Holder's Bank Account</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<BankAccountCheck>> GetAccountHolderBankAccountChecksAsync(Guid holderUid, Guid ledgerUid, Guid bankAccountUid)
        {
            return GetAccountHolderBankAccountChecksAsync(holderUid, ledgerUid, bankAccountUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Bank Accounts checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>All Checks belonging to the Account Holder's Bank Account</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<BankAccountCheck>> GetAccountHolderBankAccountChecksAsync(Guid holderUid, Guid ledgerUid, Guid bankAccountUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            if (bankAccountUid == null)
                throw new ArgumentNullException("bankAccountUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/bankaccounts/{bankAccountUid}/checks");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{bankAccountUid}", Uri.EscapeDataString(ConvertToString(bankAccountUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<BankAccountCheck>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all a Ledger's checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>All Checks belonging to the Company Account Holder's Ledger</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<LedgerCheck>> GetLedgerChecksAsync(Guid holderUid, Guid ledgerUid)
        {
            return GetLedgerChecksAsync(holderUid, ledgerUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all a Ledger's checks and their status'.</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="ledgerUid">The Uid of the Ledger</param>
        /// <returns>All Checks belonging to the Company Account Holder's Ledger</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<LedgerCheck>> GetLedgerChecksAsync(Guid holderUid, Guid ledgerUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/checks");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<LedgerCheck>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all Bank Accounts checks and their status'.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>All Checks belonging to the Company Associate's Bank Account</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<BankAccountCheck>> GetBankAccountChecksAsync(Guid associateUid, Guid bankAccountUid)
        {
            return GetBankAccountChecksAsync(associateUid, bankAccountUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all Bank Accounts checks and their status'.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="bankAccountUid">The Uid of the Bank Account</param>
        /// <returns>All Checks belonging to the Company Associate's Bank Account</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<BankAccountCheck>> GetBankAccountChecksAsync(Guid associateUid, Guid bankAccountUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (bankAccountUid == null)
                throw new ArgumentNullException("bankAccountUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/bankaccounts/{bankAccountUid}/checks");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{bankAccountUid}", Uri.EscapeDataString(ConvertToString(bankAccountUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<BankAccountCheck>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all a Personal Beneficiaries checks and their status'.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>All Checks belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<AccountHolderCheck>> GetAssociateChecksAsync(Guid associateUid)
        {
            return GetAssociateChecksAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all a Personal Beneficiaries checks and their status'.</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>All Checks belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<AccountHolderCheck>> GetAssociateChecksAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/checks");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<AccountHolderCheck>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds a document to an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>The created document with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<DocumentInfo> CreateAccountHolderDocumentAsync(Guid holderUid, DocumentInfo body)
        {
            return CreateAccountHolderDocumentAsync(holderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds a document to an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>The created document with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<DocumentInfo> CreateAccountHolderDocumentAsync(Guid holderUid, DocumentInfo body, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/documents");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<DocumentInfo>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all documents of an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>The Documents belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentInfo>> GetAccountHolderDocumentsAsync(Guid holderUid)
        {
            return GetAccountHolderDocumentsAsync(holderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all documents of an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <returns>The Documents belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentInfo>> GetAccountHolderDocumentsAsync(Guid holderUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/documents");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentInfo>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a document of an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="documentUid">The Uid of the Document</param>
        /// <returns>The Document belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<DocumentInfo> GetAccountHolderDocumentAsync(Guid holderUid, Guid documentUid)
        {
            return GetAccountHolderDocumentAsync(holderUid, documentUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a document of an Account Holder</summary>
        /// <param name="holderUid">The Uid of the Account Holder</param>
        /// <param name="documentUid">The Uid of the Document</param>
        /// <returns>The Document belonging to the Account Holder</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<DocumentInfo> GetAccountHolderDocumentAsync(Guid holderUid, Guid documentUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/documents/{documentUid}");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<DocumentInfo>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Uploads a file to a document</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentFile>> UploadDocumentFilesAsync(Guid documentUid, string holderUid, System.IO.Stream body)
        {
            return UploadDocumentFilesAsync(documentUid, holderUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Uploads a file to a document</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentFile>> UploadDocumentFilesAsync(Guid documentUid, string holderUid, System.IO.Stream body, CancellationToken cancellationToken)
        {
            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/documents/{documentUid}/files");
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StreamContent(body);
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentFile>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a document's files</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentFile>> GetHolderDocumentFilesAsync(Guid documentUid, string holderUid)
        {
            return GetHolderDocumentFilesAsync(documentUid, holderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a document's files</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentFile>> GetHolderDocumentFilesAsync(Guid documentUid, string holderUid, CancellationToken cancellationToken)
        {
            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/documents/{documentUid}/files");
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentFile>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Adds a document to an Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created document with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<DocumentInfo> CreateAssociateDocumentAsync(Guid associateUid, DocumentInfo body)
        {
            return CreateAssociateDocumentAsync(associateUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Adds a document to an Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The created document with it's Uid</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<DocumentInfo> CreateAssociateDocumentAsync(Guid associateUid, DocumentInfo body, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/documents");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StringContent(JsonConvert.SerializeObject(body, Settings.Value));
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<DocumentInfo>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets all documents of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Documents belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentInfo>> GetAssociateDocumentsAsync(Guid associateUid)
        {
            return GetAssociateDocumentsAsync(associateUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets all documents of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <returns>The Documents belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentInfo>> GetAssociateDocumentsAsync(Guid associateUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/documents");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentInfo>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a document of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="documentUid">The Uid of the Document</param>
        /// <returns>The Document belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<DocumentInfo> GetAssociateDocumentAsync(Guid associateUid, Guid documentUid)
        {
            return GetAssociateDocumentAsync(associateUid, documentUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a document of a Associate</summary>
        /// <param name="associateUid">The Uid of the Associate</param>
        /// <param name="documentUid">The Uid of the Document</param>
        /// <returns>The Document belonging to the Associate</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<DocumentInfo> GetAssociateDocumentAsync(Guid associateUid, Guid documentUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/documents/{documentUid}");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<DocumentInfo>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Uploads a file to a document</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentFile>> UploadAssociateDocumentFilesAsync(Guid associateUid, string documentUid, System.IO.Stream body)
        {
            return UploadAssociateDocumentFilesAsync(associateUid, documentUid, body, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Uploads a file to a document</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentFile>> UploadAssociateDocumentFilesAsync(Guid associateUid, string documentUid, System.IO.Stream body, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/documents/{documentUid}/files");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ = new StreamContent(body);
                    content_.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentFile>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a document's files</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<DocumentFile>> GetAssociateDocumentFilesAsync(Guid associateUid, string documentUid)
        {
            return GetAssociateDocumentFilesAsync(associateUid, documentUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a document's files</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<DocumentFile>> GetAssociateDocumentFilesAsync(Guid associateUid, string documentUid, CancellationToken cancellationToken)
        {
            if (associateUid == null)
                throw new ArgumentNullException("associateUid");

            if (documentUid == null)
                throw new ArgumentNullException("documentUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/associates/{associateUid}/documents/{documentUid}/files");
            urlBuilder_.Replace("{associateUid}", Uri.EscapeDataString(ConvertToString(associateUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{documentUid}", Uri.EscapeDataString(ConvertToString(documentUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<DocumentFile>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task IdentityAsync()
        {
            return IdentityAsync(CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task IdentityAsync(CancellationToken cancellationToken)
        {
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/Identity");

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            return;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Ledger's transactions</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<ICollection<Transaction>> GetTransactionsAsync(Guid holderUid, Guid ledgerUid)
        {
            return GetTransactionsAsync(holderUid, ledgerUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Ledger's transactions</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<ICollection<Transaction>> GetTransactionsAsync(Guid holderUid, Guid ledgerUid, CancellationToken cancellationToken)
        {
            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/transactions");
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<ICollection<Transaction>>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        /// <summary>Gets a Transaction</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public Task<Transaction> GetTransactionAsync(Guid? personalAccountHolderUid, Guid ledgerUid, Guid transactionUid, string holderUid)
        {
            return GetTransactionAsync(personalAccountHolderUid, ledgerUid, transactionUid, holderUid, CancellationToken.None);
        }

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>Gets a Transaction</summary>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        public async Task<Transaction> GetTransactionAsync(Guid? personalAccountHolderUid, Guid ledgerUid, Guid transactionUid, string holderUid, CancellationToken cancellationToken)
        {
            if (ledgerUid == null)
                throw new ArgumentNullException("ledgerUid");

            if (transactionUid == null)
                throw new ArgumentNullException("transactionUid");

            if (holderUid == null)
                throw new ArgumentNullException("holderUid");

            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/v1/accountholders/{holderUid}/ledgers/{ledgerUid}/transactions/{transactionUid}?");
            urlBuilder_.Replace("{ledgerUid}", Uri.EscapeDataString(ConvertToString(ledgerUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{transactionUid}", Uri.EscapeDataString(ConvertToString(transactionUid, CultureInfo.InvariantCulture)));
            urlBuilder_.Replace("{holderUid}", Uri.EscapeDataString(ConvertToString(holderUid, CultureInfo.InvariantCulture)));
            if (personalAccountHolderUid != null)
            {
                urlBuilder_.Append("personalAccountHolderUid=").Append(Uri.EscapeDataString(ConvertToString(personalAccountHolderUid, CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;

            var client_ = _httpClient;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    request_.Method = new HttpMethod("GET");
                    request_.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);

                    var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }

                        ProcessResponse(client_, response_);

                        var status_ = ((int)response_.StatusCode).ToString();
                        if (status_ == "200")
                        {
                            var objectResponse_ = await ReadObjectResponseAsync<Transaction>(response_, headers_).ConfigureAwait(false);
                            return objectResponse_.Object;
                        }
                        else
                        if (status_ != "200" && status_ != "204")
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
                        }

                        return default;
                    }
                    finally
                    {
                        if (response_ != null)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
            }
        }

        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                this.Object = responseObject;
                this.Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }

        public bool ReadResponseAsString { get; set; }
        public Lazy<JsonSerializerSettings> Settings { get => _settings; set => _settings = value; }

        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers)
        {
            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody = JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new System.IO.StreamReader(responseStream))
                    using (var jsonTextReader = new JsonTextReader(streamReader))
                    {
                        var serializer = JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int)response.StatusCode, null, headers, exception);
                }
            }
        }

        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is Enum)
            {
                string name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(EnumMemberAttribute))
                            as EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value != null ? attribute.Value : name;
                        }
                    }
                }
            }
            else if (value is bool)
            {
                return Convert.ToString(value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return Convert.ToBase64String((byte[])value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = Enumerable.OfType<object>((Array)value);
                return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }

            return Convert.ToString(value, cultureInfo);
        }
    }
    public partial class CompanyAccountHolder
    {
        [JsonProperty("companyName", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string CompanyName { get; set; }

        [JsonProperty("countryOfRegistrationCountryCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int CountryOfRegistrationCountryCode { get; set; }

        [JsonProperty("programmeUid", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string ProgrammeUid { get; set; }

        [JsonProperty("tradingName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string TradingName { get; set; }

        [JsonProperty("legalStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LegalStatus { get; set; }

        [JsonProperty("companyRegistrationNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyRegistrationNumber { get; set; }

        [JsonProperty("dateOfIncorporation", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? DateOfIncorporation { get; set; }

        [JsonProperty("phoneNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("emailAddress", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress { get; set; }

        [JsonProperty("websiteUrls", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string WebsiteUrls { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public CompanyAccountHolderStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class CompanyAddress
    {
        [JsonProperty("countryCode", Required = Required.Always)]
        public int CountryCode { get; set; }

        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public CompanyAddressType Type { get; set; }

        [JsonProperty("cityOrTown", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CityOrTown { get; set; }

        [JsonProperty("county", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string County { get; set; }

        [JsonProperty("houseNameNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string HouseNameNumber { get; set; }

        [JsonProperty("postalCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }

        [JsonProperty("street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("holderUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid HolderUid { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class CompanyQuestionnaire
    {
        [JsonProperty("currenciesToBeBought", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CurrenciesToBeBought { get; set; }

        [JsonProperty("currenciesToBeReceived", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CurrenciesToBeReceived { get; set; }

        [JsonProperty("currenciesToBeSent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CurrenciesToBeSent { get; set; }

        [JsonProperty("currenciesToBeSold", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CurrenciesToBeSold { get; set; }

        [JsonProperty("descriptionOfBusinessActivities", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DescriptionOfBusinessActivities { get; set; }

        [JsonProperty("doesYourComanyProvideFinancialServices", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool DoesYourComanyProvideFinancialServices { get; set; }

        [JsonProperty("estimatedAverageIncomingTransactionValue", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedAverageIncomingTransactionValue { get; set; }

        [JsonProperty("estimatedAverageIncomingTransactionVolume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedAverageIncomingTransactionVolume { get; set; }

        [JsonProperty("estimatedAverageOutgoingTransactionValue", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedAverageOutgoingTransactionValue { get; set; }

        [JsonProperty("estimatedAverageOutgoingTransactionVolume", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedAverageOutgoingTransactionVolume { get; set; }

        [JsonProperty("estimatedIncomingTransactionTurnover", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedIncomingTransactionTurnover { get; set; }

        [JsonProperty("estimatedNextYearsTurnover", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedNextYearsTurnover { get; set; }

        [JsonProperty("estimatedOutgoingTransactionTurnover", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EstimatedOutgoingTransactionTurnover { get; set; }

        [JsonProperty("expectedIncomingPaymentsSourceCountries", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ExpectedIncomingPaymentsSourceCountries { get; set; }

        [JsonProperty("expectedOutgoingPaymentsDestinationCountries", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ExpectedOutgoingPaymentsDestinationCountries { get; set; }

        [JsonProperty("lastYearsTurnover", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string LastYearsTurnover { get; set; }

        [JsonProperty("mainBusinessPartners", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MainBusinessPartners { get; set; }

        [JsonProperty("mainBusinessRegions", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MainBusinessRegions { get; set; }

        [JsonProperty("managementAndShareholderStructure", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ManagementAndShareholderStructure { get; set; }

        [JsonProperty("descriptionOfServiceUsage", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string DescriptionOfServiceUsage { get; set; }

        [JsonProperty("specialLicenses", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string SpecialLicenses { get; set; }

        [JsonProperty("holderUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid HolderUid { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class Address
    {
        [JsonProperty("countryCode", Required = Required.Always)]
        public int CountryCode { get; set; }

        [JsonProperty("cityOrTown", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CityOrTown { get; set; }

        [JsonProperty("county", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string County { get; set; }

        [JsonProperty("houseNameNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string HouseNameNumber { get; set; }

        [JsonProperty("isCurrent", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCurrent { get; set; }

        [JsonProperty("postalCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }

        [JsonProperty("residentFrom", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? ResidentFrom { get; set; }

        [JsonProperty("residentTo", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? ResidentTo { get; set; }

        [JsonProperty("street", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }

        [JsonProperty("holderUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid HolderUid { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class PersonalAccountHolder
    {
        [JsonProperty("dateOfBirth", Required = Required.AllowNull)]
        public System.DateTimeOffset? DateOfBirth { get; set; }

        [JsonProperty("familyName", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string FamilyName { get; set; }

        [JsonProperty("givenName", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string GivenName { get; set; }

        [JsonProperty("nationalityCountryCode", Required = Required.Always)]
        public int NationalityCountryCode { get; set; }

        [JsonProperty("programmeUid", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string ProgrammeUid { get; set; }

        [JsonProperty("emailAddress", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress { get; set; }

        [JsonProperty("phoneNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PersonalAccountHolderStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class Ledger
    {
        [JsonProperty("currencyCode", Required = Required.Always)]
        public int CurrencyCode { get; set; }

        [JsonProperty("moneyType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string MoneyType { get; set; }

        [JsonProperty("programmeConfigurationUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProgrammeConfigurationUid { get; set; }

        [JsonProperty("balance", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long Balance { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public LedgerStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class BankAccount
    {
        [JsonProperty("ledgerUid", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public System.Guid LedgerUid { get; set; }

        [JsonProperty("currencyIsoCode", Required = Required.Always)]
        public int CurrencyIsoCode { get; set; }

        [JsonProperty("bankCountryCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string BankCountryCode { get; set; }

        [JsonProperty("checkDigits", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int CheckDigits { get; set; }

        [JsonProperty("bban", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Bban { get; set; }

        [JsonProperty("bic", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Bic { get; set; }

        [JsonProperty("iban", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public BankAccountStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }

        [JsonProperty("capabilities", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public BankAccountCapabilities Capabilities { get; set; }


    }
    public partial class Transaction
    {
        [JsonProperty("amount", Required = Required.Always)]
        public long Amount { get; set; }

        [JsonProperty("destinationLedgerUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid DestinationLedgerUid { get; set; }

        [JsonProperty("destinationAssociateUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid DestinationAssociateUid { get; set; }

        [JsonProperty("destinationBankAccountUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid DestinationBankAccountUid { get; set; }

        [JsonProperty("sourceAssociateUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid SourceAssociateUid { get; set; }

        [JsonProperty("sourceLedgerUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid SourceLedgerUid { get; set; }

        [JsonProperty("sourceBankAccountUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid SourceBankAccountUid { get; set; }

        [JsonProperty("dateTimeCreated", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset DateTimeCreated { get; set; }

        [JsonProperty("dateTimeCleared", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? DateTimeCleared { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public TransactionStatus Status { get; set; }

        [JsonProperty("transactionStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public TransactionStatus2 TransactionStatus { get; set; }

        [JsonProperty("reference", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Reference { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class Associate
    {
        [JsonProperty("nationalityCountryCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int NationalityCountryCode { get; set; }

        [JsonProperty("programmeUid", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        public string ProgrammeUid { get; set; }

        [JsonProperty("companyName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }

        [JsonProperty("emailAddress", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string EmailAddress { get; set; }

        [JsonProperty("dateOfBirth", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? DateOfBirth { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("phoneNumber", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        [JsonProperty("type", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public AssociateType Type { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public AssociateStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class AssociateBankAccount
    {
        [JsonProperty("currencyIsoCode", Required = Required.Always)]
        public int CurrencyIsoCode { get; set; }

        [JsonProperty("bankCountryCode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string BankCountryCode { get; set; }

        [JsonProperty("programmeConfigurationUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProgrammeConfigurationUid { get; set; }

        [JsonProperty("checkDigits", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int CheckDigits { get; set; }

        [JsonProperty("bban", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Bban { get; set; }

        [JsonProperty("bic", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Bic { get; set; }

        [JsonProperty("iban", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Iban { get; set; }

        [JsonProperty("status", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public AssociateBankAccountStatus Status { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class AccountHolderCheck
    {
        [JsonProperty("holderUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid HolderUid { get; set; }

        [JsonProperty("programmeConfigurationUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProgrammeConfigurationUid { get; set; }

        [JsonProperty("checkName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckName { get; set; }

        [JsonProperty("checkStartedDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStartedDateTime { get; set; }

        [JsonProperty("checkStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public AccountHolderCheckCheckStatus CheckStatus { get; set; }

        [JsonProperty("checkStatusDescription", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckStatusDescription { get; set; }

        [JsonProperty("checkStatusUpdateDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStatusUpdateDateTime { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class BankAccountCheck
    {
        [JsonProperty("bankAccountUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid BankAccountUid { get; set; }

        [JsonProperty("programmeConfigurationUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProgrammeConfigurationUid { get; set; }

        [JsonProperty("checkName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckName { get; set; }

        [JsonProperty("checkStartedDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStartedDateTime { get; set; }

        [JsonProperty("checkStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public BankAccountCheckCheckStatus CheckStatus { get; set; }

        [JsonProperty("checkStatusDescription", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckStatusDescription { get; set; }

        [JsonProperty("checkStatusUpdateDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStatusUpdateDateTime { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class LedgerCheck
    {
        [JsonProperty("ledgerUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid LedgerUid { get; set; }

        [JsonProperty("programmeConfigurationUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ProgrammeConfigurationUid { get; set; }

        [JsonProperty("checkName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckName { get; set; }

        [JsonProperty("checkStartedDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStartedDateTime { get; set; }

        [JsonProperty("checkStatus", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public LedgerCheckCheckStatus CheckStatus { get; set; }

        [JsonProperty("checkStatusDescription", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CheckStatusDescription { get; set; }

        [JsonProperty("checkStatusUpdateDateTime", Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset? CheckStatusUpdateDateTime { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class DocumentInfo
    {
        [JsonProperty("documentType", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required(AllowEmptyStrings = true)]
        [JsonConverter(typeof(Converters.StringEnumConverter))]
        public DocumentInfoDocumentType DocumentType { get; set; }

        [JsonProperty("holderUid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid HolderUid { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.Guid Uid { get; set; }


    }
    public partial class DocumentFile
    {
        [JsonProperty("contentDisposition", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ContentDisposition { get; set; }

        [JsonProperty("contentType", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("dateUploaded", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public System.DateTimeOffset DateUploaded { get; set; }

        [JsonProperty("fileName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("length", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long Length { get; set; }

        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("uid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Uid { get; set; }


    }
    public enum CompanyAccountHolderStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum CompanyAddressType
    {
        [EnumMember(Value = @"LegalAddress")]
        LegalAddress = 0,

        [EnumMember(Value = @"TradingAddress")]
        TradingAddress = 1,

        [EnumMember(Value = @"BranchAddress")]
        BranchAddress = 2,

    }
    public enum PersonalAccountHolderStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum LedgerStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum BankAccountStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum BankAccountCapabilities
    {
        [EnumMember(Value = @"None")]
        None = 0,

        [EnumMember(Value = @"SEPACreditTransfer")]
        SEPACreditTransfer = 1,

        [EnumMember(Value = @"SEPAInstantCreditTransfer")]
        SEPAInstantCreditTransfer = 2,

        [EnumMember(Value = @"SEPADirectDebit")]
        SEPADirectDebit = 3,

        [EnumMember(Value = @"SEPAB2B")]
        SEPAB2B = 4,

        [EnumMember(Value = @"SEPACardClearing")]
        SEPACardClearing = 5,

        [EnumMember(Value = @"SWIFT")]
        SWIFT = 6,

        [EnumMember(Value = @"CHAPS")]
        CHAPS = 7,

        [EnumMember(Value = @"BACSDirectDebit")]
        BACSDirectDebit = 8,

        [EnumMember(Value = @"BACSDirectCredit")]
        BACSDirectCredit = 9,

        [EnumMember(Value = @"FasterPaymentsCreditTransfer")]
        FasterPaymentsCreditTransfer = 10,

    }
    public enum TransactionStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum TransactionStatus2
    {
        [EnumMember(Value = @"Pending")]
        Pending = 0,

        [EnumMember(Value = @"Cleared")]
        Cleared = 1,

        [EnumMember(Value = @"Rejected")]
        Rejected = 2,

    }
    public enum AssociateType
    {
        [EnumMember(Value = @"Individual")]
        Individual = 0,

        [EnumMember(Value = @"Company")]
        Company = 1,

    }
    public enum AssociateStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum AssociateBankAccountStatus
    {
        [EnumMember(Value = @"Checking")]
        Checking = 0,

        [EnumMember(Value = @"CheckBlocked")]
        CheckBlocked = 1,

        [EnumMember(Value = @"Ready")]
        Ready = 2,

    }
    public enum AccountHolderCheckCheckStatus
    {
        [EnumMember(Value = @"NotStarted")]
        NotStarted = 0,

        [EnumMember(Value = @"InProgress")]
        InProgress = 1,

        [EnumMember(Value = @"Blocked")]
        Blocked = 2,

        [EnumMember(Value = @"Ok")]
        Ok = 3,

    }
    public enum BankAccountCheckCheckStatus
    {
        [EnumMember(Value = @"NotStarted")]
        NotStarted = 0,

        [EnumMember(Value = @"InProgress")]
        InProgress = 1,

        [EnumMember(Value = @"Blocked")]
        Blocked = 2,

        [EnumMember(Value = @"Ok")]
        Ok = 3,

    }
    public enum LedgerCheckCheckStatus
    {
        [EnumMember(Value = @"NotStarted")]
        NotStarted = 0,

        [EnumMember(Value = @"InProgress")]
        InProgress = 1,

        [EnumMember(Value = @"Blocked")]
        Blocked = 2,

        [EnumMember(Value = @"Ok")]
        Ok = 3,

    }
    public enum DocumentInfoDocumentType
    {
        [EnumMember(Value = @"Unclassified")]
        Unclassified = 0,

        [EnumMember(Value = @"IdentityDocument")]
        IdentityDocument = 1,

        [EnumMember(Value = @"IdentityPhoto")]
        IdentityPhoto = 2,

        [EnumMember(Value = @"IdentityVideo")]
        IdentityVideo = 3,

    }
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, IReadOnlyDictionary<string, IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }
}
