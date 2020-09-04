using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Partiality.Modloader;

namespace NootArmy
{
    public class NootArmy : PartialityMod
    {
        public NootArmy()
        {
            this.author = "Woodensponge";
            this.Version = "1.0";
            this.ModID = "NootArmy";
        }

        public override void OnEnable()
        {
            base.OnEnable();
            OnBabyNootDeath.Patch();
        }
    }
}