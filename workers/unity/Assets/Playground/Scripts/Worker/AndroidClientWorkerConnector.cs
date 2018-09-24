#if UNITY_ANDROID
using Improbable.Gdk.Mobile;
using Improbable.Gdk.Mobile.Android;
using Improbable.Gdk.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.Worker
{
    public class AndroidClientWorkerConnector : MobileWorkerConnectorBase
    {
        public InputField input;
        public Button button;

        public void Start()
        {
            button.onClick.AddListener(Connect);
        }

        public async void Connect()
        {
            await Connect(WorkerUtils.UnityClient, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void AddWorkerSystems()
        {
            WorkerUtils.AddClientSystems(Worker.World);
        }

        private string GetIpFromField()
        {
            return input.text;
        }

        protected override string GetHostIp()
        {
            var hostIp = GetIpFromField();
            if (DeviceInfo.IsAndroidStudioEmulator() && hostIp.Equals(string.Empty))
            {
                return DeviceInfo.AndroidStudioEmulatorDefaultCallbackIp;
            }

            return hostIp;
        }
    }
}
#endif
