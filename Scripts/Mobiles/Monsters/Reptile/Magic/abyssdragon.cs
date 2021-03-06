using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an abyss dragon's corpse" )]
	public class AbyssDragon : BaseCreature
	{
		[Constructable]
		public AbyssDragon() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an abyss dragon";
			Body = 150;
            BaseSoundID = 362;
			Hue = 0x902;

            SetStr(100);
            SetDex(50);
            SetInt(100);

            SetHits(750);
            SetMana(2000);

            SetDamage(25, 35);

            SetSkill(SkillName.Wrestling, 90);
            SetSkill(SkillName.Tactics, 100);

            SetSkill(SkillName.MagicResist, 25);

            SetSkill(SkillName.Magery, 75);
            SetSkill(SkillName.EvalInt, 75);
            SetSkill(SkillName.Meditation, 100);

            VirtualArmor = 25;

			Fame = 20000;
			Karma = -46000;

			CanSwim = true;
			CantWalk = false;
		}

        public override void SetUniqueAI()
        {
            DictCombatAction[CombatAction.CombatSpecialAction] = 3;
            DictCombatSpecialAction[CombatSpecialAction.FireBreathAttack] = 1;
        }		
		
		public override bool AlwaysMurderer{ get{ return true; } }
		
		public AbyssDragon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}