using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian corpse" )]
	public class OphidianWarrior : BaseCreature
	{
		private static string[] m_Names = new string[]
			{
				"an ophidian warrior",
				"an ophidian enforcer"
			};

		[Constructable]
		public OphidianWarrior() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = m_Names[Utility.Random( m_Names.Length )];
			Body = 86;
			BaseSoundID = 634;

            SetStr(75);
            SetDex(75);
            SetInt(25);

            SetHits(200);

            SetDamage(9, 18);

            SetSkill(SkillName.Wrestling, 75);
            SetSkill(SkillName.Tactics, 100);

            SetSkill(SkillName.MagicResist, 50);

            SetSkill(SkillName.Poisoning, 5);

			Fame = 4500;
			Karma = -4500;
		}

        public override Poison HitPoison { get { return Poison.Greater; } }
        public override Poison PoisonImmune { get { return Poison.Greater; } }

		public override int Meat{ get{ return 1; } }

		public OphidianWarrior( Serial serial ) : base( serial )
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