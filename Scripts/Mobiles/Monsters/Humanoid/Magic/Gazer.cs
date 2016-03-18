using System;
using Server;
using Server.Items;
using Server.Achievements;

namespace Server.Mobiles
{
    [CorpseName("a gazer corpse")]
    public class Gazer : BaseCreature
    {
        [Constructable]
        public Gazer(): base(AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            Name = "a gazer";
            Body = 22;
            BaseSoundID = 377;

            SetStr(50);
            SetDex(25);
            SetInt(75);

            SetHits(150);
            SetMana(1000);

            SetDamage(5, 10);

            SetSkill(SkillName.Wrestling, 50);
            SetSkill(SkillName.Tactics, 100);

            SetSkill(SkillName.MagicResist, 75);

            SetSkill(SkillName.Magery, 50);
            SetSkill(SkillName.EvalInt, 50);
            SetSkill(SkillName.Meditation, 100);

            VirtualArmor = 25;

            Fame = 3500;
            Karma = -3500;

            PackItem(new Nightshade(4));
        }

        public override void GenerateLoot()
        {
            switch (Utility.Random(1000))
            {
                case 1: { AddItem(new Diamond()); } break;
                case 2: { AddItem(new Ruby()); } break;
                case 3: { AddItem(new HealPotion()); } break;
                case 4: { AddItem(new HealScroll()); } break;
            }
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            AwardDailyAchievementForKiller(PvECategory.KillGazers);
        }

        public override int Meat { get { return 1; } }

        public Gazer(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}