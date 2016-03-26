﻿using System;
using Server.Mobiles;

namespace Server.Items
{
	public enum TalismanForm
	{
		Ferret		= 1031672,
		Squirrel	= 1031671,
		CuSidhe		= 1031670,
		Reptalon	= 1075202
	}

	public class BaseFormTalisman : Item, ITokunoDyable
	{
		public virtual TalismanForm Form { get { return TalismanForm.Squirrel; } }

		public BaseFormTalisman() : base( 0x2F59 )
		{
			LootType = LootType.Blessed;
			Layer = Layer.Talisman;
			Weight = 1.0;
		}

		public BaseFormTalisman( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			list.Add( 1075200, String.Format( "#{0}", (int) Form ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

		public override void OnRemoved( object parent )
		{
			base.OnRemoved( parent );
		}

		public static bool EntryEnabled( Mobile m, Type type )
		{
			return true;
		}
	}

	public class FerretFormTalisman : BaseFormTalisman
	{
		public override TalismanForm Form { get { return TalismanForm.Ferret; } }

		[Constructable]
		public FerretFormTalisman() : base()
		{
		}

		public FerretFormTalisman( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SquirrelFormTalisman : BaseFormTalisman
	{
		public override TalismanForm Form { get { return TalismanForm.Squirrel; } }

		[Constructable]
		public SquirrelFormTalisman () : base()
		{
		}

		public SquirrelFormTalisman ( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class CuSidheFormTalisman : BaseFormTalisman
	{
		public override TalismanForm Form { get { return TalismanForm.CuSidhe; } }

		[Constructable]
		public CuSidheFormTalisman() : base()
		{
		}

		public CuSidheFormTalisman( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class ReptalonFormTalisman : BaseFormTalisman
	{
		public override TalismanForm Form { get { return TalismanForm.Reptalon; } }

		[Constructable]
		public ReptalonFormTalisman() : base()
		{
		}

		public ReptalonFormTalisman( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
