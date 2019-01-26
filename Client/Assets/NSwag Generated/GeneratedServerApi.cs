﻿//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v12.0.13.0 (NJsonSchema v9.13.17.0 (Newtonsoft.Json v11.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

namespace GeneratedServerAPI
{
    #pragma warning disable

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.13.0 (NJsonSchema v9.13.17.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class Client 
    {
        private string _baseUrl = "";
        private System.Net.Http.HttpClient _httpClient;
        private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;
    
        public Client(string baseUrl, System.Net.Http.HttpClient httpClient)
        {
            BaseUrl = baseUrl; 
            _httpClient = httpClient; 
            _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(() => 
            {
                var settings = new Newtonsoft.Json.JsonSerializerSettings();
                UpdateJsonSerializerSettings(settings);
                return settings;
            });
        }
    
        public string BaseUrl 
        {
            get { return _baseUrl; }
            set { _baseUrl = value; }
        }
    
        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }
    
        partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
        partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RoomInformation> StartRoomAsync(string roomName, string playerId)
        {
            return StartRoomAsync(roomName, playerId, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RoomInformation> StartRoomAsync(string roomName, string playerId, System.Threading.CancellationToken cancellationToken)
        {
            if (roomName == null)
                throw new System.ArgumentNullException("roomName");
    
            if (playerId == null)
                throw new System.ArgumentNullException("playerId");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/start-room?");
            urlBuilder_.Append("roomName=").Append(System.Uri.EscapeDataString(ConvertToString(roomName, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("playerId=").Append(System.Uri.EscapeDataString(ConvertToString(playerId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(RoomInformation); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<Room> CreateRoomAsync(string playerId, string possibleThreats, int? roundLengthInSeconds, long? seed)
        {
            return CreateRoomAsync(playerId, possibleThreats, roundLengthInSeconds, seed, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<Room> CreateRoomAsync(string playerId, string possibleThreats, int? roundLengthInSeconds, long? seed, System.Threading.CancellationToken cancellationToken)
        {
            if (playerId == null)
                throw new System.ArgumentNullException("playerId");
    
            if (possibleThreats == null)
                throw new System.ArgumentNullException("possibleThreats");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/create-room?");
            urlBuilder_.Append("playerId=").Append(System.Uri.EscapeDataString(ConvertToString(playerId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("possibleThreats=").Append(System.Uri.EscapeDataString(ConvertToString(possibleThreats, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            if (roundLengthInSeconds != null) 
            {
                urlBuilder_.Append("roundLengthInSeconds=").Append(System.Uri.EscapeDataString(ConvertToString(roundLengthInSeconds, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            if (seed != null) 
            {
                urlBuilder_.Append("seed=").Append(System.Uri.EscapeDataString(ConvertToString(seed, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            }
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(Room); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<Room>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RoomInformation> JoinRoomAsync(string roomName, string playerId)
        {
            return JoinRoomAsync(roomName, playerId, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RoomInformation> JoinRoomAsync(string roomName, string playerId, System.Threading.CancellationToken cancellationToken)
        {
            if (roomName == null)
                throw new System.ArgumentNullException("roomName");
    
            if (playerId == null)
                throw new System.ArgumentNullException("playerId");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/join-room?");
            urlBuilder_.Append("roomName=").Append(System.Uri.EscapeDataString(ConvertToString(roomName, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("playerId=").Append(System.Uri.EscapeDataString(ConvertToString(playerId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(RoomInformation); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RoomInformation> RoomInformationAsync(string roomName)
        {
            return RoomInformationAsync(roomName, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RoomInformation> RoomInformationAsync(string roomName, System.Threading.CancellationToken cancellationToken)
        {
            if (roomName == null)
                throw new System.ArgumentNullException("roomName");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/room-information?");
            urlBuilder_.Append("roomName=").Append(System.Uri.EscapeDataString(ConvertToString(roomName, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(RoomInformation); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RoomInformation> SendEmojisAsync(string roomName, string playerId, string emojis)
        {
            return SendEmojisAsync(roomName, playerId, emojis, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RoomInformation> SendEmojisAsync(string roomName, string playerId, string emojis, System.Threading.CancellationToken cancellationToken)
        {
            if (roomName == null)
                throw new System.ArgumentNullException("roomName");
    
            if (playerId == null)
                throw new System.ArgumentNullException("playerId");
    
            if (emojis == null)
                throw new System.ArgumentNullException("emojis");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/send-emojis?");
            urlBuilder_.Append("roomName=").Append(System.Uri.EscapeDataString(ConvertToString(roomName, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("playerId=").Append(System.Uri.EscapeDataString(ConvertToString(playerId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("emojis=").Append(System.Uri.EscapeDataString(ConvertToString(emojis, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(RoomInformation); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        public System.Threading.Tasks.Task<RoomInformation> SetRoleAsync(string roomName, string playerId, string role)
        {
            return SetRoleAsync(roomName, playerId, role, System.Threading.CancellationToken.None);
        }
    
        /// <returns>default response</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        public async System.Threading.Tasks.Task<RoomInformation> SetRoleAsync(string roomName, string playerId, string role, System.Threading.CancellationToken cancellationToken)
        {
            if (roomName == null)
                throw new System.ArgumentNullException("roomName");
    
            if (playerId == null)
                throw new System.ArgumentNullException("playerId");
    
            if (role == null)
                throw new System.ArgumentNullException("role");
    
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/api/game/set-role?");
            urlBuilder_.Append("roomName=").Append(System.Uri.EscapeDataString(ConvertToString(roomName, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("playerId=").Append(System.Uri.EscapeDataString(ConvertToString(playerId, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Append("role=").Append(System.Uri.EscapeDataString(ConvertToString(role, System.Globalization.CultureInfo.InvariantCulture))).Append("&");
            urlBuilder_.Length--;
    
            var client_ = _httpClient;
            try
            {
                using (var request_ = new System.Net.Http.HttpRequestMessage())
                {
                    request_.Method = new System.Net.Http.HttpMethod("GET");
                    request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
    
                    PrepareRequest(client_, request_, urlBuilder_);
                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);
                    PrepareRequest(client_, request_, url_);
    
                    var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
                    try
                    {
                        var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }
    
                        ProcessResponse(client_, response_);
    
                        var status_ = ((int)response_.StatusCode).ToString();
                        {
                            var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false); 
                            var result_ = default(RoomInformation); 
                            try
                            {
                                result_ = Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(responseData_, _settings.Value);
                                return result_; 
                            } 
                            catch (System.Exception exception_) 
                            {
                                throw new SwaggerException("Could not deserialize the response body.", (int)response_.StatusCode, responseData_, headers_, exception_);
                            }
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
    
        private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value is System.Enum)
            {
                string name = System.Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute)) 
                            as System.Runtime.Serialization.EnumMemberAttribute;
                        if (attribute != null)
                        {
                            return attribute.Value;
                        }
                    }
                }
            }
            else if (value is bool) {
                return System.Convert.ToString(value, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[])
            {
                return System.Convert.ToBase64String((byte[]) value);
            }
            else if (value != null && value.GetType().IsArray)
            {
                var array = System.Linq.Enumerable.OfType<object>((System.Array) value);
                return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
            }
        
            return System.Convert.ToString(value, cultureInfo);
        }
    }
    
    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Emoji 
    {
        [Newtonsoft.Json.JsonProperty("emoji", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Emoji1 { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static Emoji FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Emoji>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class PlayerId 
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static PlayerId FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerId>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Playing 
    {
        [Newtonsoft.Json.JsonProperty("players", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<PlayerId> Players { get; set; }
    
        [Newtonsoft.Json.JsonProperty("possibleThreats", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<RoleThreat> PossibleThreats { get; set; }
    
        [Newtonsoft.Json.JsonProperty("forbiddenRoles", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.IDictionary<string, RoleThreat> ForbiddenRoles { get; set; }
    
        [Newtonsoft.Json.JsonProperty("playedPlayerRoles", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.IDictionary<string, RoleThreat> PlayedPlayerRoles { get; set; }
    
        [Newtonsoft.Json.JsonProperty("playerEmojis", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.IDictionary<string, Emoji> PlayerEmojis { get; set; }
    
        [Newtonsoft.Json.JsonProperty("playerEmojisHistory", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.IDictionary<string, System.Collections.Generic.ICollection<Emoji>> PlayerEmojisHistory { get; set; }
    
        [Newtonsoft.Json.JsonProperty("lastFailedThreats", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<RoleThreat> LastFailedThreats { get; set; }
    
        [Newtonsoft.Json.JsonProperty("openThreats", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<RoleThreat> OpenThreats { get; set; }
    
        [Newtonsoft.Json.JsonProperty("roundEndingTime", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public long RoundEndingTime { get; set; }
    
        [Newtonsoft.Json.JsonProperty("currentRoundState", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PlayingCurrentRoundState CurrentRoundState { get; set; }
    
        [Newtonsoft.Json.JsonProperty("currentRoundNumber", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int CurrentRoundNumber { get; set; }
    
        [Newtonsoft.Json.JsonProperty("maxRoundNumber", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int MaxRoundNumber { get; set; }
    
        [Newtonsoft.Json.JsonProperty("gameWon", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool GameWon { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static Playing FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Playing>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class RoleThreat 
    {
        [Newtonsoft.Json.JsonProperty("value", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Value { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static RoleThreat FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RoleThreat>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Room 
    {
        [Newtonsoft.Json.JsonProperty("players", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<PlayerId> Players { get; set; }
    
        [Newtonsoft.Json.JsonProperty("possibleThreats", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.ICollection<RoleThreat> PossibleThreats { get; set; }
    
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }
    
        [Newtonsoft.Json.JsonProperty("owner", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Owner { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static Room FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Room>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class RoomInformation 
    {
        [Newtonsoft.Json.JsonProperty("waiting", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Room Waiting { get; set; }
    
        [Newtonsoft.Json.JsonProperty("playing", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Playing Playing { get; set; }
    
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    
        public static RoomInformation FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<RoomInformation>(data);
        }
    
    }
    
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.13.17.0 (Newtonsoft.Json v11.0.0.0)")]
    public enum PlayingCurrentRoundState
    {
        [System.Runtime.Serialization.EnumMember(Value = @"COMMUNICATION_PHASE")]
        COMMUNICATION_PHASE = 0,
    
        [System.Runtime.Serialization.EnumMember(Value = @"PLAYOUT_PHASE")]
        PLAYOUT_PHASE = 1,
    
        [System.Runtime.Serialization.EnumMember(Value = @"DOOMED_PHASE")]
        DOOMED_PHASE = 2,
    
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.13.0 (NJsonSchema v9.13.17.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class SwaggerException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException) 
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

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "12.0.13.0 (NJsonSchema v9.13.17.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class SwaggerException<TResult> : SwaggerException
    {
        public TResult Result { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.Dictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException) 
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

    #pragma warning restore
}