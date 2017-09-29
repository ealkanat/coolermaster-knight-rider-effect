using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolerMaster_Knight_Rider
{
    class CoolerMasterDLL
    {
        // https://github.com/antonpup/Aurora

        public const int MAX_LED_ROW = 6;
        public const int MAX_LED_COLUMN = 22;

        public enum DEVICE_INDEX
        {
            [Description("MasterKeys Pro L")]
            DEV_MKeys_L = 0,
            [Description("MasterKeys Pro S")]
            DEV_MKeys_S = 1,
            [Description("MasterKeys Pro L White")]
            DEV_MKeys_L_White = 2,
            [Description("MasterKeys Pro M White")]
            DEV_MKeys_M_White = 3,
            [Description("MasterMouse Pro L")]
            DEV_MMouse_L = 4,
            [Description("MasterMouse Pro S")]
            DEV_MMouse_S = 5,
            [Description("MasterKeys Pro M")]
            DEV_MKeys_M = 6,
            [Description("MasterKeys Pro S White")]
            DEV_MKeys_S_White = 7,
        }

        public static List<DEVICE_INDEX> Mice = new List<DEVICE_INDEX>
        {
            DEVICE_INDEX.DEV_MMouse_L,
            DEVICE_INDEX.DEV_MMouse_S
        };

        public static List<DEVICE_INDEX> Keyboards = new List<DEVICE_INDEX>
        {
            DEVICE_INDEX.DEV_MKeys_L,
            DEVICE_INDEX.DEV_MKeys_L_White,
            DEVICE_INDEX.DEV_MKeys_M,
            DEVICE_INDEX.DEV_MKeys_M_White,
            DEVICE_INDEX.DEV_MKeys_S,
            DEVICE_INDEX.DEV_MKeys_S_White,
        };

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
