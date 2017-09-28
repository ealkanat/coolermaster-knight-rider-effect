using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolerMaster_SDK_Test
{
    class EffectSettings
    {
        public List<int> _color { get; set; }
        public List<int> _backgroundColor { get; set; }
        public int[] _effectRows { get; set; }

        public EffectSettings() {
            this._color = new List<int> { 255, 0, 0 };
            this._backgroundColor = new List<int> { 60, 0, 0 };
            this._effectRows = new int[] {3, 4};
        }
    }
}
