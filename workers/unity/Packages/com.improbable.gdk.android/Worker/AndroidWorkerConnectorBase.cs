using System;
using Improbable.Gdk.Core;

namespace Improbable.Gdk.Android
{
    public class AndroidWorkerConnectorBase : WorkerConnectorBase
    {
        protected delegate string GetHostIpDelegate();

        protected static GetHostIpDelegate GetHostIp;

        protected override ReceptionistConfig GetReceptionistConfig(string workerType)
        {
            if (GetHostIp == null)
            {
                throw new NotImplementedException();
            }

            var hostIp = GetHostIp();

            return new ReceptionistConfig
            {
                ReceptionistHost = DeviceInfo.IsAndroidStudioEmulator() && hostIp.Equals(string.Empty)
                    ? "10.0.2.2"
                    : hostIp,
                WorkerType = workerType,
                WorkerId = CreateNewWorkerId(workerType),
                UseExternalIp = UseExternalIp
            };
        }
    }
}
