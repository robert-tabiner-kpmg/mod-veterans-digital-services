using System;
using System.Collections.Generic;
using Forms.Core.Models.InFlight;

namespace Forms.Core.EffectHandlers.Models
{
    public class PathChangeEffect : Effect
    {
        public override EffectTypes EffectTypes => EffectTypes.PathChange;

        public PathChangeEffect(Func<List<InFlightQuestion>, string> handle)
        {
            Handle = handle;
        }

        public Func<List<InFlightQuestion>, string> Handle { get; }
        
    }
}