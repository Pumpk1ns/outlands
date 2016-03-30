using System;

namespace Server.Items
{
    [FlipableAttribute(0xc79, 0xc7a)]
    public class Cantaloupe : Food
    {
        [Constructable]
        public Cantaloupe(): this(1)
        {
        }

        [Constructable]
        public Cantaloupe(int amount): base(amount, 0xc79)
        {
            Weight = 1.0;
        }

        public Cantaloupe(Serial serial): base(serial)
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