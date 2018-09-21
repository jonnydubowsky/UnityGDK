using System;
using Improbable.Gdk.Core;
using Packages.com.improbable.gdk.ios.Utility;

namespace Improbable.Gdk.Ios
{
    public class IosWorkerConnectorBase : WorkerConnectorBase
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
                ReceptionistHost = DeviceInfo.IsIosSimulator() && hostIp.Equals(string.Empty)
                    ? RuntimeConfigDefaults.ReceptionistHost
                    : hostIp,
                WorkerType = workerType,
                WorkerId = CreateNewWorkerId(workerType),
                UseExternalIp = UseExternalIp
            };
        }
    }
}
