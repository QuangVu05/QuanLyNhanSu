using Newtonsoft.Json;

namespace QuanLyNhanSu.Service_Layer
{
    public class CountryService : BackgroundService
    {
        /* private readonly IHttpClientFactory _httpClientFactory;

         public CountryService(IHttpClientFactory httpClientFactory)
         {
             _httpClientFactory = httpClientFactory;
         }

         public async Task<List<string>> GetCountriesAsync()
         {
             var client = _httpClientFactory.CreateClient();
             var response = await client.GetAsync("https://restcountries.com/v3.1/all");

             if (response.IsSuccessStatusCode)
             {
                 var content = await response.Content.ReadAsStringAsync();
                 var countries = JsonConvert.DeserializeObject<List<Country>>(content);
                 return countries.ConvertAll(c => c.Name.Common); // Trả về tên quốc gia
             }

             return new List<string>();
         }*/
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CountryService> _logger;
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "countries.json");
        private const int UpdateIntervalDays = 30; // Cập nhật mỗi 30 ngày

        public CountryService(IHttpClientFactory httpClientFactory, ILogger<CountryService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("🔄 Kiểm tra danh sách quốc gia...");

                    if (IsUpdateNeeded())
                    {
                        var countries = await GetCountriesAsync();
                        if (countries.Count > 0)
                        {
                            SaveToFile(countries);
                            _logger.LogInformation($"✅ Đã lưu {countries.Count} quốc gia vào {FilePath}");
                        }
                        else
                        {
                            _logger.LogWarning("⚠ Không lấy được danh sách quốc gia từ API.");
                        }
                    }
                    else
                    {
                        _logger.LogInformation("✅ Danh sách quốc gia vẫn còn mới, không cần cập nhật.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"❌ Lỗi khi cập nhật danh sách quốc gia: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromDays(UpdateIntervalDays), stoppingToken); // Chạy lại sau 30 ngày
            }
        }

        private bool IsUpdateNeeded()
        {
            if (!File.Exists(FilePath)) return true;

            try
            {
                var json = File.ReadAllText(FilePath);
                var data = JsonConvert.DeserializeObject<CountryData>(json);
                if (data == null || data.LastUpdated == null) return true;

                return (DateTime.UtcNow - data.LastUpdated).TotalDays > UpdateIntervalDays;
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi khi kiểm tra file: {ex.Message}");
                return true;
            }
        }

        private async Task<List<string>> GetCountriesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var countries = JsonConvert.DeserializeObject<List<Country>>(content);
                return countries.ConvertAll(c => c.Name.Common);
            }

            return new List<string>();
        }

        private void SaveToFile(List<string> countries)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath)); // Tạo thư mục nếu chưa tồn tại
                var json = JsonConvert.SerializeObject(new CountryData
                {
                    LastUpdated = DateTime.UtcNow,
                    Countries = countries
                }, Formatting.Indented);

                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi khi lưu file: {ex.Message}");
            }
        }

        public List<string> LoadFromFile()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var json = File.ReadAllText(FilePath);
                    var data = JsonConvert.DeserializeObject<CountryData>(json);
                    return data?.Countries ?? new List<string>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi khi đọc file: {ex.Message}");
            }
            return new List<string>();
        }
    }

    public class CountryData
    {
        public DateTime LastUpdated { get; set; }
        public List<string> Countries { get; set; }
    }

    public class Country
    {
        [JsonProperty("name")]
        public CountryName Name { get; set; }
    }

    public class CountryName
    {
        [JsonProperty("common")]
        public string Common { get; set; }
    }
}




