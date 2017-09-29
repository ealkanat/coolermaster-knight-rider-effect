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
        public const int MAX_LED_ROW = 6;
        public const int MAX_LED_COLUMN = 22;

        [StructLayout(LayoutKind.Sequential)]
        public struct KEY_COLOR
        {
            public byte r;
            public byte g;
            public byte b;

            public KEY_COLOR(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COLOR_MATRIX
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LED_ROW * MAX_LED_COLUMN, ArraySubType = UnmanagedType.Struct)]
            public KEY_COLOR[,] KeyColor;
        }

        [DllImport("SDKDLL.DLL", EntryPoint = "IsDevicePlug", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsDevicePlug();

        [DllImport("SDKDLL.DLL", EntryPoint = "EnableLedControl", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool EnableLedControl(bool bEnable);
        
        [DllImport("SDKDLL.DLL", EntryPoint = "SetLedColor")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SetLedColor(int iRow, int iColumn, byte r, byte g, byte b);

        [DllImport("SDKDLL.DLL", EntryPoint = "SetFullLedColor", SetLastError = true,
        CharSet = System.Runtime.InteropServices.CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern bool SetFullLedColor(byte r, byte g, byte b);

        [DllImport("SDKDLL.DLL", EntryPoint = "SetAllLedColor")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SetAllLedColor(COLOR_MATRIX colorMatrix);

    }
}
