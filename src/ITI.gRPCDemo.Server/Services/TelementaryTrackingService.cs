using Grpc.Core;
using ITI.gRPCDemo.Server.Protos;

namespace ITI.gRPCDemo.Server.Services
{
    public class TelementaryTrackingService : TrackingService.TrackingServiceBase
    {
        private readonly ILogger<TelementaryTrackingService> _logger;
        public TelementaryTrackingService(ILogger<TelementaryTrackingService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// To call it from gRPC curl 
        /// call this command line 
        /// grpcurl -d "{ \"deviceId\" :1, \"speed\":2 }" localhost:7030 TrackingService/SendMessage
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<TrackingResponse> SendMessage(TrackingMessage request, ServerCallContext context)
        {
            _logger.LogInformation($"New Message DeviceId :{request.DeviceId} location :{request.Location} speed :{request.Speed} stamp :{request.Stamp}");
            // return base.SendMessage(request, context);
            return Task.FromResult(new TrackingResponse() { Success = true });
        }
    }
}
