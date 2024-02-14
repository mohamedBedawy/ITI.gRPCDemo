using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using ITI.gRPCDemo.Server.Protos;

namespace ITI.gRPCDemo.Device
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _deviceId;

        private TrackingService.TrackingServiceClient _client;
        private TrackingService.TrackingServiceClient Client
        {
            get
            {
                if (_client == null)
                {
                    GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7030");
                    return new TrackingService.TrackingServiceClient(channel);
                }
                return _client;
            }
        }
        public Worker(ILogger<Worker> logger, int deviceId)
        {
            _logger = logger;
            _deviceId = deviceId;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var req = new TrackingMessage()
                {
                    DeviceId = _deviceId,
                    Speed = new Random().Next(),
                    Location = new Location() { Lat = new Random().NextDouble(), Long = new Random().NextDouble() },
                    Stamp = Timestamp.FromDateTime(DateTime.UtcNow)
                };
                var resp = await Client.SendMessageAsync(req);

                _logger.LogInformation($"Response from client : {resp.Success}");
                //if (_logger.IsEnabled(LogLevel.Information))
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //}
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
