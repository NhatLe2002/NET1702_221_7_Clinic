using ClinicBusiness;
using ClinicCommon;

namespace ClinicRazorWebApp
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public BackgroundWorkerService(IServiceScopeFactory serviceScopeFactory, ILogger<BackgroundWorkerService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Calculate delay time until next midnight
                var now = DateTime.Now;

                // Wait until next midnight
                await Task.Delay(TimeSpan.FromSeconds(60), stoppingToken);

                ExecuteExpriredCustomer(stoppingToken);
            }
        }

        private async Task ExecuteExpriredCustomer(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var customerBusiness = scope.ServiceProvider.GetRequiredService<ICustomerBusinessClass>();
                    var result = await customerBusiness.Notification();
                    if (result.Status == Const.FAIL_DELETE_CODE)
                    {
                        _logger.LogError("Failed to delete in background service");
                    }
                    else
                    {
                        _logger.LogError("************* Success to delete in background service");
                    }
                    _logger.LogInformation("Expired salappointment checked and updated at: {time}", DateTimeOffset.Now);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete in background service");
            }
        }
    }
}
