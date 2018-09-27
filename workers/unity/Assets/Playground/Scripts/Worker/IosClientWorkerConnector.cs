using System;
using Improbable.Gdk.Mobile;
#if UNITY_IOS
using Improbable.Gdk.Mobile.Ios;
#endif
using Improbable.Gdk.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Playground.Worker
{
    public class IosClientWorkerConnector : MobileWorkerConnector
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
            errorMessage.text = "Connection failure";
        }

        private string GetIpFromField()
        {
            return input.text;
        }

        protected override string GetHostIp()
        {
#if UNITY_IOS
            var hostIp = GetIpFromField();
            if (DeviceInfo.IsIosSimulator() && hostIp.Equals(string.Empty))
            {
                return RuntimeConfigDefaults.ReceptionistHost;
            }

            return hostIp;
#else
            throw new NotImplementedException("Incompatible platform: Please use iOS");
#endif
        }
    }
}
