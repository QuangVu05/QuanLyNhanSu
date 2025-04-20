namespace QuanLyNhanSu.Service_Layer
{
    public class TempImageCleanupService : BackgroundService
    {
        private readonly ILogger<TempImageCleanupService> _logger;
        private readonly string _tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "temp");

        public TempImageCleanupService(ILogger<TempImageCleanupService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    XoaAnhTam();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"❌ Lỗi khi xóa ảnh tạm: {ex.Message}");
                }

                await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken); // Kiểm tra mỗi 3 phút
            }
        }

        private void XoaAnhTam()
        {
            if (!Directory.Exists(_tempFolder)) return;

            foreach (var file in Directory.GetFiles(_tempFolder))
            {
                try
                {
                    var fileInfo = new FileInfo(file);

                    // Kiểm tra nếu file đã không được cập nhật trong 10 phút
                    if (fileInfo.LastWriteTime < DateTime.Now.AddMinutes(-10))
                    {
                        fileInfo.Delete();
                        _logger.LogInformation($"✅ Đã xóa ảnh tạm: {fileInfo.Name}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"❌ Lỗi khi xóa file {file}: {ex.Message}");
                }
            }
        }
    }
}
