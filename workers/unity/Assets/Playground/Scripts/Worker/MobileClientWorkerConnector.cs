using Improbable.Gdk.Android;
using Improbable.Gdk.Core;

namespace Playground.Worker
{
    public class MobileClientWorkerConnector : AndroidWorkerConnectorBase
    {
        private async void Start()
        {
            GetHostIp = GetIpFromField;
            await Connect(WorkerUtils.UnityClient, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void AddWorkerSystems()
        {
            WorkerUtils.AddClientSystems(Worker.World);
        }

        private string GetIpFromField()
        {
            return "yolo";
        }
    }
}
