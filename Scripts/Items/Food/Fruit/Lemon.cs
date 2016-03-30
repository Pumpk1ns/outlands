using System;

namespace Server.Items
{
    public class Lemon : Food
    {
        [Constructable]
        public Lemon() : this(1)
        {
        }

        [Constructable]
        public Lemon(int amount): base(amount, 0x1728)
        {
            Weight = 1.0;
        }

        public Lemon(Serial serial): base(serial)
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