using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolerMaster_SDK_Test
{
    class CoolerMasterDLL
    {

        [DllImport("SDKDLL.DLL", EntryPoint = "IsDevicePlug", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsDevicePlug();

        [DllImport("SDKDLL.DLL", EntryPoint = "EnableLedControl", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool EnableLedControl(bool bEnable);

        [DllImport("SDKDLL.DLL", EntryPoint = "SetLedColor", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetLedColor(int iRow, int iColumn, byte r, byte g, byte b);

        [DllImport("SDKDLL.DLL", EntryPoint = "SetFullLedColor", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetFullLedColor(byte r, byte g, byte b);

        [DllImport("SDKDLL.DLL", EntryPoint = "SetAllLedColor", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetAllLedColor(ColorMatrix colorMatrix);

    }
}
