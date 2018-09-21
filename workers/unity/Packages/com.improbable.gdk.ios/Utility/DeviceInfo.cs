using UnityEngine;

namespace Packages.com.improbable.gdk.ios.Utility
{
    public class DeviceInfo
    {
        public static bool IsIosSimulator()
        {
            return SystemInfo.deviceModel.Equals("x86_64");
        }
    }
}
