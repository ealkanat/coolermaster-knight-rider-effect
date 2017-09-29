using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolerMaster_Knight_Rider
{
    class EffectTimer
    {

        public static readonly object SyncRoot = new object();
        private static EffectTimer instance_ = null;
        private static Timer timer_;
        private int eventCount = 0;

        public static EffectTimer GetInstance()
        {
            if (instance_ == null) instance_ = new EffectTimer();
            return instance_;
        }

        private EffectTimer()
        {
            timer_ = new Timer();
            timer_.Interval = 40;
            timer_.Start();
        }

        public void attachEventHandler(EventHandler eventHandler) {
            timer_.Tick += eventHandler;
            eventCount++;
        }

        public void detachEventHandler(EventHandler eventHandler)
        {
            timer_.Tick -= eventHandler;
            eventCount--;
            if (eventCount == 0) {
                timer_.Stop();
                timer_ = null;
                instance_ = null;
            }
        }
    }
 }
