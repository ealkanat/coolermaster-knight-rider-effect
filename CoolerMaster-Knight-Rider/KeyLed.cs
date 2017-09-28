using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolerMaster_SDK_Test
{
    class KeyLed
    {
        private Timer dimmerTimer;
        private EffectTimer timer;
        private EventHandler eventHandler;

        public KeyLed(int keyId, int row, int column, Color color, Color bgColor, Boolean autoDimToBgColor, String name )
        {
            init(keyId, row, column, color, bgColor, autoDimToBgColor, name);
        }

        public KeyLed(int row, int column, Color color, Color bgColor)
        {
            init(int.Parse(row.ToString() + column.ToString()), row, column, color, bgColor, true, "");
        }

        public KeyLed(int row, int column, Color color)
        {
            init(int.Parse(row.ToString() + column.ToString()), row, column, color, Color.Black, true, "");
        }

        private void init(int keyId, int row, int column, Color color, Color bgColor, Boolean autoDimToBgColor, String name) {

            this._keyId = keyId;
            this._row = row;
            this._column = column;
            this._color = color;
            this._bgColor = bgColor;
            this._name = name;
            this._autoDimToBgColor = autoDimToBgColor;
        }

        public int _keyId { get; set; }
        public int _row { get; set; }
        public int _column { get; set; }
        public Color _color { get; set; }
        public Color _bgColor { get; set; }
        public String _name { get; set; }
        public Boolean _autoDimToBgColor { get; set; }

        public void setColor(Color color) {
            this._color = color;
        }

        public void lightOn() {
            this._color = Color.FromArgb(255, this._color.R, this._color.G, this._color.B);
            CoolerMasterDLL.SetLedColor(this._row-1, this._column-1, this._color.R, this._color.G, this._color.B);
            if (this._autoDimToBgColor)
            {
                //if (dimmerTimer != null) {
                //    dimmerTimer.Stop();
                //    dimmerTimer = null;
                //}
                lightDim();
            }                
        }

        public void lightOff()
        {
            CoolerMasterDLL.SetLedColor(this._row-1, this._column-1, Color.Black.R, Color.Black.G, Color.Black.B);
        }

        public void lightDim()
        {
            //if (dimmerTimer == null) {
            //    dimmerTimer = new Timer();
            //    dimmerTimer.Tick += new EventHandler(dimmerTimerEventProcessor);
            //    dimmerTimer.Interval = 40;
            //}
            //dimmerTimer.Start();

            timer = EffectTimer.GetInstance();
            eventHandler = new EventHandler(dimmerTimerEventProcessor);
            timer.attachEventHandler(eventHandler);
            
        }

        private void dimmerTimerEventProcessor(Object myObject, EventArgs myEventArgs)
        {
            int alphaStep = 255 / 8;
            if (this._color.A > 0)
            {
                int newAlpha = this._color.A - alphaStep;
                if (newAlpha < 0) { newAlpha = 0; }
                this._color = Color.FromArgb(newAlpha, this._color.R, this._color.G, this._color.B);
                Color nColor = CalculateSolidColorFromTransparentColor(this._color, this._bgColor);
                CoolerMasterDLL.SetLedColor(this._row -1, this._column - 1, nColor.R, nColor.G, nColor.B);
            }
            else {
                timer.detachEventHandler(eventHandler);
                //dimmerTimer.Stop();
                //dimmerTimer = null;
            }
        }

        private Color CalculateSolidColorFromTransparentColor(Color color, Color background)
        {
            float alpha = color.A / 255f;
            float oneminusalpha = 1f - alpha;

            int newR = Convert.ToInt32(((color.R * alpha) + (oneminusalpha * background.R)));
            int newG = Convert.ToInt32(((color.G * alpha) + (oneminusalpha * background.G)));
            int newB = Convert.ToInt32(((color.B * alpha) + (oneminusalpha * background.B)));

            return Color.FromArgb(255, newR, newG, newB);
        }

        public bool Equals(KeyLed other)
        {
            if (other == null) return false;
            return (this._keyId.Equals(other._keyId));
        }
    }
}
