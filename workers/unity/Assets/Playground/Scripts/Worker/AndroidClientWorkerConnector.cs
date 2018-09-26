#if UNITY_ANDROID
using Improbable.Gdk.Mobile;
using Improbable.Gdk.Mobile.Android;
using Improbable.Gdk.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.Worker
{
    public class AndroidClientWorkerConnector : MobileWorkerConnector
    {
        public GameObject ConnectionPanel;
        public GameObject Level;

        private GameObject levelInstance;
        private InputField input;
        private Button button;
        private Text errorMessage;

        public void Awake()
        {
            input = ConnectionPanel.transform.Find("ConnectInput").GetComponent<InputField>();
            button = ConnectionPanel.transform.Find("ConnectButton").GetComponent<Button>();
            errorMessage = ConnectionPanel.transform.Find("ConnectionError").GetComponent<Text>();
        }

        public void Start()
        {
            button.onClick.AddListener(Connect);
        }

        public async void Connect()
        {
            await Connect(WorkerUtils.UnityClient, new ForwardingDispatcher()).ConfigureAwait(false);
        }

        protected override void HandleWorkerConnectionEstablished()
        {
            WorkerUtils.AddClientSystems(Worker.World);
            if (Level == null)
            {
                return;
            }

            levelInstance = Instantiate(Level, transform);
            levelInstance.transform.SetParent(null);

            ConnectionPanel.SetActive(false);
        }

        protected override void HandleWorkerConnectionFailure()
        {
            errorMessage.text = "YOLO";
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
