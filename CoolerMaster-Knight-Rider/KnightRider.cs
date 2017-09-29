using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolerMaster_SDK_Test
{
    public enum DIRECTION {
        RIGHT = 0,
        LEFT = 1
    }

    class KnightRider
    {
        private Timer timer;
        private static Color bgColor = Color.FromArgb(60, 0, 0);
        private static Color effectColor = Color.FromArgb(255, 0, 0);
        private static int CURRENT_LED_COLUMN = 0;
        private static DIRECTION currentDirection = DIRECTION.RIGHT;
        //private static List<KeyLed> activeKeyLeds;
        private static Dictionary<int, KeyLed> activeKeyLeds;
        private static int[] rows;

        public KnightRider(Color color, Color background, int[] effectRows) {
            effectColor = color;
            bgColor = background;
            rows = effectRows;
            init();
        }

        public KnightRider() {
            init();
        }

        public void changeLedColor(Color effectColor, Color bgColor) {
            CoolerMasterDLL.SetFullLedColor(bgColor.R, bgColor.G, bgColor.B);
            foreach (var led in activeKeyLeds) {
                led.Value._color = effectColor;
                led.Value._bgColor = bgColor;
            }
        }

        public void changeUpdateRowIndex(int[] nRows) {
            rows = nRows;
        }

        public void stop() {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }

        public void start()
        {            
            init();
        }

        private void init() {

            int totalLedCount = CoolerMasterDLL.MAX_LED_COLUMN * CoolerMasterDLL.MAX_LED_ROW;

            if (activeKeyLeds == null)
            {
                activeKeyLeds = new Dictionary<int, KeyLed>();
                for (int i = 0; i < totalLedCount; i++)
                {
                    int row = (int)Math.Ceiling((double)(i + 1) / 22);
                    int column = (i + 1);
                    if (column > CoolerMasterDLL.MAX_LED_COLUMN)
                    {
                        column = (i + 1) - (22 * (row - 1));
                    }
                    var keyItem = new KeyLed(row, column, effectColor, bgColor);
                    activeKeyLeds.Add(int.Parse(row.ToString() + column.ToString()), keyItem);
                }
            }

            if (timer != null) {
                timer.Stop();
                timer = null;
            }

            timer = new Timer();
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Interval = 30;
            timer.Start();
            
            if (CoolerMasterDLL.IsDevicePlug())
            {
                //MessageBox.Show("Connected");
                CoolerMasterDLL.EnableLedControl(true);
                CoolerMasterDLL.SetFullLedColor(bgColor.R, bgColor.G, bgColor.B);
            }
            else
            {
                MessageBox.Show("Keyboard is not connected");
            }
        }

        private static void TimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            animateLeds(rows, effectColor);
        }

        private static void animateLeds(int[] rows, Color color) {

            if (CURRENT_LED_COLUMN == CoolerMasterDLL.MAX_LED_COLUMN - 1) {
                currentDirection = DIRECTION.LEFT;
            }

            if (CURRENT_LED_COLUMN <= 0) {
                currentDirection = DIRECTION.RIGHT;
            }

            if (currentDirection == DIRECTION.RIGHT) {
                foreach (int r in rows) {
                    //KeyLed led = activeKeyLeds.Find(x => x._keyId == int.Parse((r).ToString() + (CURRENT_LED_COLUMN + 1).ToString()));
                    int keyId = int.Parse((r).ToString() + (CURRENT_LED_COLUMN + 1).ToString());
                    KeyLed led = activeKeyLeds[keyId];
                    led.lightOn();
                }
                CURRENT_LED_COLUMN++;
            }

            if (currentDirection == DIRECTION.LEFT)
            {
                foreach (int r in rows)
                {
                    //KeyLed led = activeKeyLeds.Find(x => x._keyId == int.Parse((r).ToString() + (CURRENT_LED_COLUMN + 1).ToString()));
                    int keyId = int.Parse((r).ToString() + (CURRENT_LED_COLUMN + 1).ToString());
                    KeyLed led = activeKeyLeds[keyId];
                    led.lightOn();
                }
                CURRENT_LED_COLUMN--;
            }

        }

    }
}
