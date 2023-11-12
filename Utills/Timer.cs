using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TF2.Utills
{
    public class Timer
    {
        private int _TimePassed;
        private int max = 60;
        public Timer(int max=60) { this.max = max; }
        public void Tick() { _TimePassed = _TimePassed < max ? _TimePassed + 1 : 0; }
        public bool TimePassed(int amount)
        {
            if (_TimePassed > 0 && _TimePassed % amount == 0) return true;
            return false;
        }

    }
}

