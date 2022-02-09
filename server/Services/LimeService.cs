using GPPlanetGQL.Exceptions;

namespace GPPlanetGQL.Services
{
    public class LimeService
    {
        private readonly IConfiguration _config;
        private readonly string _token;
        private readonly string _baseURL;
        public LimeService(IConfiguration config)
        {
            _config = config;
            _token = Environment.GetEnvironmentVariable("LimeToken") ?? "LimeToken";
            _baseURL = "http://185.158.113.244:1003";
        }

        public bool IsValidToken(string token)
        {
            return token.Equals(_token);
        }

        public async Task<Dictionary<string, LimeUserData>> GetUserDatas()
        {
            var httpClient = HttpClientFactory.Create();

            var url = $"{_baseURL}/data?token={_token}";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsAsync<LimeResponse<Dictionary<string, LimeUserData>>>();
                return data.response;
            }
            throw new IternalException("Ошибка получения данных");
        }

        public async Task<int> GetUserTref(long discordId)
        {
            var httpClient = HttpClientFactory.Create();

            var url = $"{_baseURL}/donate?token={_token}&discord_id={discordId}";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsAsync<LimeResponse<LimeTref>>();
                return data.response.balance;
            }
            throw new IternalException("Ошибка получения данных");
        }

        public async Task<bool> Donate(long discordId, int amount)
        {
            var httpClient = HttpClientFactory.Create();

            var url = $"{_baseURL}/donate?token={_token}&discord_id={discordId}&amount={amount}";
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync(url, new { });

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            throw new IternalException("Ошибка получения данных");
        }

        public async Task<LimeUserData> GetUserData(long discordId)
        {
            var httpClient = HttpClientFactory.Create();

            var url = $"{_baseURL}/data?token={_token}&discord_id={discordId}";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsAsync<LimeResponse<LimeUserData>>();
                return data.response;
            }
            throw new IternalException("Ошибка получения данных");
        }

        public async Task<int> GetOnline()
        {
            var httpClient = HttpClientFactory.Create();

            var url = $"{_baseURL}/data/online?token={_token}";
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var content = httpResponseMessage.Content;
                var data = await content.ReadAsAsync<LimeResponse<LimeOnline>>();
                return data.response.online;
            }
            throw new IternalException("Ошибка получения данных");
        }
    }

    public class LimeResponse<T>
    {
        public string status { get; set; } = "";
        public T response { get; set; }
    }

    public class LimeTref
    {
        public int balance { get; set; } = 0;
    }

    public class LimeOnline
    {
        public int online { get; set; } = 0;
    }

    public class LimeUserData
    {
        public string? work { get; set; } = null;
        public string? role { get; set; } = null;
        public int level { get; set; } = 0;
        public int? phone { get; set; } = null;

        public override string ToString()
        {
            return $"{work}, {role}, {level}, {phone}";
        }
    }
}
