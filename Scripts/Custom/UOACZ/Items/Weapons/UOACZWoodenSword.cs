using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
    [FlipableAttribute(0x13B9, 0x13Ba)]
    public class UOACZWoodenSword : BaseSword
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.CrushingBlow; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.ParalyzingBlow; } }

        public override int AosStrengthReq { get { return 40; } }
        public override int AosMinDamage { get { return 15; } }
        public override int AosMaxDamage { get { return 17; } }
        public override int AosSpeed { get { return 28; } }
        public override float MlSpeed { get { return 3.75f; } }

        public override int OldStrengthReq { get { return 40; } }
        public override int OldMinDamage { get { return 12; } }
        public override int OldMaxDamage { get { return 21; } }
        public override int OldSpeed { get { return 45; } }

        public override int DefHitSound { get { return 0x232; } }
        public override int DefMissSound { get { return 0x23A; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 100; } }

        [Constructable]
        public UOACZWoodenSword(): base(0x13B9)
        {
            Name = "wooden sword";

            Weight = 6.0;
            Hue = 2415;
        }

        public UOACZWoodenSword(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}