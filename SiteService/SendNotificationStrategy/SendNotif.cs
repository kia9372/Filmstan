using Common.Operation;
using Common.SiteEnums;
using System.Threading;
using System.Threading.Tasks;

namespace SiteService.SendNotificationStrategy
{
    public abstract class SendNotif
    {
        public abstract SendCodeVerification SendCodeType { get; }
        public abstract Task<OperationResult<string>> SendCodeAsync(string content, string to, CancellationToken cancellationToken);
    }
}
