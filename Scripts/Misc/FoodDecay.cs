using System;
using Server.Network;
using Server.Mobiles;
using Server;

namespace Server.Misc
{
    public class FoodDecayTimer : Timer
    {
        public static void Initialize()
        {
            new FoodDecayTimer().Start();
        }

        public FoodDecayTimer(): base(TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5))
        {
            Priority = TimerPriority.OneMinute;
        }

        protected override void OnTick()
        {
            FoodDecay();
        }

        public static void FoodDecay()
        {
            foreach (NetState state in NetState.Instances)
            {
                BaseCreature bc_Creature = state.Mobile as BaseCreature;

                if (bc_Creature != null)
                {
                    if (Utility.RandomDouble() < .10)
                    {
                        HungerDecay(state.Mobile);
                        ThirstDecay(state.Mobile);
                    }
                }

                else
                {
                    HungerDecay(state.Mobile);
                    ThirstDecay(state.Mobile);
                }
            }
        }

        public static void HungerDecay(Mobile m)
        {
            if (m != null && m.Hunger >= 1)
                m.Hunger -= 1;
        }

        public static void ThirstDecay(Mobile m)
        {
            if (m != null && m.Thirst >= 1)
                m.Thirst -= 1;
        }
    }
}