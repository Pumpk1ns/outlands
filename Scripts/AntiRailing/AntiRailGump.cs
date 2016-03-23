﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//////////////////////////////////////////////////////////////////////////
// Based on the RTT system from Angel Island
//////////////////////////////////////////////////////////////////////////
using System.Collections;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Custom.AntiRailing
{
	class AntiRailTimer : Timer
	{
		private Mobile m_from;

		public AntiRailTimer(Mobile from, TimeSpan ts)
			: base(ts)
		{
			m_from = from;
		}

		protected override void OnTick()
		{
			; // no answer :(
		}

	}
	class AntiRailGump : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register("AntiRailTest", AccessLevel.GameMaster, new CommandEventHandler(ManualAntiRailTest_OnCommand));
		}
		[Usage("AntiRailTest")]
		[Description("Manually trigger the anti railing test for yourself")]
		public static void ManualAntiRailTest_OnCommand(CommandEventArgs e)
		{
			Mobile from = e.Mobile;
			from.SendGump(new AntiRailGump(from, "Hello, friend", "SKILLNAME"));
		}

		private Mobile m_Mobile;
		private string m_strNotify = "";
		private string m_strSkill = "";
		private int m_CorrectResponse = -1;
		private int m_CorrectResponseOffset = 0;
		private DateTime m_AntiRailTestLaunched;
		private AntiRailTimer m_timeout = null;

		protected Mobile Mobile
		{
			get { return m_Mobile; }
			set { m_Mobile = value; }
		}

		protected string Notification
		{
			get { return m_strNotify; }
			set { m_strNotify = value; }
		}

		protected string Skill
		{
			get { return m_strSkill; }
			set { m_strSkill = value; }
		}

		protected int CorrectResponse
		{
			get { return m_CorrectResponse; }
			set { m_CorrectResponse = value; }
		}

		protected int CorrectResponseOffset
		{
			get { return m_CorrectResponseOffset; }
			set { m_CorrectResponseOffset = value; }
		}

		#region buttons and stuff
		private static int[] ButtonIDS = new int[]
		{
			0x08C0,	0x08C1,	0x08C2,	0x08C3,	0x08C4,	0x08C5,	0x08C6,	0x08C7,
			0x08C8,	0x08C9,	0x08CA,	0x08CB,	0x08CC,	0x08CD,	0x08CE,	0x08CF,
			0x08D0,	0x08D1,	0x08D2,	0x08D3,	0x08D4,	0x08D5,	0x08D6,	0x08D7,
			0x08D8,	0x08D9,	0x08DA,	0x08DB,	0x08DC,	0x08DD,	0x08DE,	0x08DF,
			0x08E0,	0x08E1,	0x08E2,	0x08E3,	0x08E4,	0x08E5,	0x08E6,	0x08E7,
			0x08E8,	0x08E9,	0x08EA,	0x08EB,	0x08EC,	0x08ED,	0x08EE,	0x08EF,
			0x08F0,	0x08F1,	0x08F2,	0x08F3,	0x08F4,	0x08F5,	0x08F6,	0x08F7,
			0x08F8,	0x08F9,	0x08FA,	0x08FB,	0x08FC,	0x08FD,	0x08FE,	0x08FF,
		};

		private static int[] ItemIDS = new int[]
		{
			0xFAF, //anvil
			0xF9D, //sewing kit
			0xF9F, //scissors
			//0x11CD, //flowerpot //Something's wrong with this - too big?
			0x1443, //axe
			0x1AE2, //skull
			0x1BF4, //ingots
			//0x2329, //snowman //Too big for the gump :(
			0x1721, //bananas
			0x1712, //boots
			0x153E, //apron
			0x14F7, //anchor
			0x14F8, //rope
			0x13B9, //sword
			0x0EEF, //gold coins
			0x0F39, //shovel
			0x0F40, //arrows
			0x0E43, //wooden chest
			0x09E9, //cake
			0x09CF, //fish
			0x0993, //fruit basket
			0x0E77, //barrel
			0x0F9C, //bolt of cloth
			0x0FB6, //horseshoes
			0x100F, //key
			0x116E, //gravestone
			//0x113C, //leather tunic //Something's wrong with this - wrong ID?
			0x14F3, //ship model
			0x171A, //feathered hat
			0x1811, //hourglass
			0x1B76, //shield
			0x1BDE, //logs
			0x1E2F, //dartboard
		};
		private static string[] ItemIDS_Strings = new string[]
		{
			"anvil",
			"sewing kit",
			"scissors",
			//"flowerpot", //Something's wrong with this - too big?
			"axe",
			"skull",
			"ingots",
			//"snowman", //Too big for the gump
			"bananas",
			"boots",
			"apron",
			"anchor",
			"rope",
			"sword",
			"gold coins",
			"shovel",
			"arrows",
			"wooden chest",
			"cake",
			"fish",
			"fruit basket",
			"barrel",
			"bolt of cloth",
			"horseshoes",
			"key",
			"gravestone",
			//"leather tunic", //Something's wrong with this - wrong ID?
			"ship model",
			"feathered hat",
			"hourglass",
			"shield",
			"logs",
			"dartboard",
		};

		private static int[] NoiseImageIDs = new int[]
		{
			0x98C, 0x98D, 0x98E, 0x98F, 0x990, 0x991, 0x992, 0x994, 
			0x995, 0x996, 0x997, 0x998, 0x999, 0x99A, 0x99B, 0x99C,
			0x99D, 0x99E, 0x99F, 0x9A0, 0x9A1, 0x9A2, 0x9A3, 0x9A4,
			0x9A5, 0x9A6, 0x9A7, 0x9A8, 0x9BB, 0x9D0, 0x9D1, 0x9D2,
			0x9D3, 0x9D4, 0x9D5, 0x9D6, 0x9d7, 0x9d8, 0x9d9, 0x9da,
			0x9Db, 0x9Dc, 0x9Dd, 0x9De, 0x9df, 0x9e0, 0x9e1, 0x9e2,
			0x9e3, 0x9e4, 0x9e5, 0x9e6, 0x9e7, 0x9e8, 0x9ea, 0x9eb,
			0xc6a, 0xc6b, 0xc6c, 0xc6d, 0xc6e, 0xc70, 0xc71, 0xc72,
		};
		#endregion

		public AntiRailGump(Mobile from, string strNotice, string strSkill)
			: base(50, 50)
		{
			m_Mobile = from;
			m_strNotify = strNotice;
			m_strSkill = strSkill;

			//close any other RTT gumps
			from.CloseGump(typeof(AntiRailGump));

			// notify staff
			CommandHandlers.BroadcastMessage(AccessLevel.Administrator,
				0x482,
				String.Format("{0}({1}) is taking the RTT ({2}).", m_Mobile.Name, m_Mobile.Serial, strSkill));

			SetupGump();

			m_AntiRailTestLaunched = DateTime.UtcNow;

			m_timeout = new AntiRailTimer(m_Mobile, TimeSpan.FromSeconds(Utility.RandomList(90, 120, 180)));
			m_timeout.Start();
		}

		public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
			// kill our timeout timer
			if (m_timeout != null)
			{
				m_timeout.Stop();
				m_timeout = null;
			}

            int button = info.ButtonID;
			TimeSpan diff = DateTime.UtcNow - m_AntiRailTestLaunched;

            if (button == m_CorrectResponse + m_CorrectResponseOffset)
            {
                m_Mobile.SendMessage("Thanks for verifying that you're at your computer.");

                if (diff <= TimeSpan.FromSeconds(1.0))
                {
                    CommandHandlers.BroadcastMessage(AccessLevel.Counselor,
                    0x22,
                    String.Format("{0}({1}) has quickly passed the RTT ({2} ms) ({3}).", m_Mobile.Name, m_Mobile.Serial, diff.TotalMilliseconds, m_strNotify));
                }
                else
                {
                    CommandHandlers.BroadcastMessage(AccessLevel.Counselor,
                    0x482,
                    String.Format("{0}({1}) has passed the RTT ({2} ms).  ({3})", m_Mobile.Name, m_Mobile.Serial, diff.TotalMilliseconds, m_strNotify));
                }
            }
            else
            {
                m_Mobile.SendMessage("You have failed the AFK test.");
                CommandHandlers.BroadcastMessage(AccessLevel.Counselor,
                0x482,
                String.Format("{0}({1}) has failed the RTT. ({2})", m_Mobile.Name, m_Mobile.Serial, this.m_strNotify));
            }
        }

		protected void SetupGump()
		{
			Dragable = true;
			
			AddPage(0);
			AddBackground(0, 0, 350, 125, 5054);
			AddBackground(50, 140, 200, 290, 5054);

			int iMode = Utility.RandomMinMax(1, 4);

			if (iMode == 1)
			{
				int[] responses = new int[10];
				int[] buttonids = new int[10];

				//Get 10 unique button graphics
				ArrayList alIDs = new ArrayList();
				while (alIDs.Count < 10)
				{
					int sx = ButtonIDS[Utility.Random(ButtonIDS.Length)];
					if (!alIDs.Contains(sx))
						alIDs.Add(sx);
				}
				for (int i = 0; i < 10; i++)
				{
					buttonids[i] = (int)alIDs[i];
				}

				//Get the correct response
				this.CorrectResponse = Utility.Random(1, 1000);
				//Determine the correct position
				int iCorrectPosition = Utility.Random(10);
				//Get 10 other responses
				for (int i = 0; i < 10; i++)
				{
					//get a random number and make sure it's not the same as the correct response
					do { responses[i] = Utility.Random(1, 1000); } while (responses[i] == this.CorrectResponse);
				}
				//replace the correct position with the correct response
				responses[iCorrectPosition] = this.CorrectResponse;


				// draw the gump/graphics
				int y = 20;
				int x = 20;
				AddHtml(x, y, 330, 25, this.Notification, false, false);
				y += 30;
				AddHtml(x, y, 330, 25, "Pick the following button from the images below:", false, false);
				y += 25;
				int randomModX = Utility.RandomMinMax(3, 100);
				AddImage(x + randomModX, y, buttonids[iCorrectPosition]);
				y += 40;

				randomModX = Utility.RandomMinMax(3, 11);
				int randomModY = Utility.RandomMinMax(-5, 5);

				y += 40;
				x += 50; //make the response part smaller :-)
				for (int i = 0; i < 10; i += 2)
				{
					AddButton(x + randomModX, y - 1, buttonids[i], buttonids[i], responses[i], GumpButtonType.Reply, 0);
					AddButton(x + 100, y - 1, buttonids[i + 1], buttonids[i + 1], responses[i + 1], GumpButtonType.Reply, 0);

					y += (50 + randomModY);
				}
			}
			else
			{
				//new mode :-P
				int[] responses = new int[10];
				int[] buttonids = new int[10];

				//Get 10 unique button graphics
				ArrayList alIDs = new ArrayList();
				while (alIDs.Count < 10)
				{
					//old way - with all valid responses
					//					int sx = ItemIDS[Utility.Random(ItemIDS.Length)];
					int sx = NoiseImageIDs[Utility.Random(NoiseImageIDs.Length)];
					if (!alIDs.Contains(sx))
						alIDs.Add(sx);
				}
				for (int i = 0; i < 10; i++)
				{
					buttonids[i] = (int)alIDs[i];
				}


				//Get the correct response
				this.CorrectResponse = Utility.Random(1, 1000);
				//Determine the correct position
				int iCorrectPosition = Utility.Random(10);

				//substitute a valid response @iCorrectPosition
				buttonids[iCorrectPosition] = ItemIDS[Utility.Random(ItemIDS.Length)];
				//flip another valid response in to confuse people
				int avid = ItemIDS[Utility.Random(ItemIDS.Length)];
				if (avid != buttonids[iCorrectPosition])
				{
					int pos = Utility.Random(10);
					if (pos != iCorrectPosition)
					{
						buttonids[pos] = avid;
					}
				}

				//Get 10 other responses
				for (int i = 0; i < 10; i++)
				{
					//get a random number and make sure it's not the same as the correct response
					do { responses[i] = Utility.Random(1, 1000); } while (responses[i] == this.CorrectResponse);
				}
				//replace the correct position with the correct response
				responses[iCorrectPosition] = this.CorrectResponse;


				//FINALLY, draw the gump/graphics
				int y = 20;
				int x = 20;
				AddHtml(x, y, 330, 25, this.Notification, false, false);
				y += 30;
				string itemstr = "";
				for (int i = 0; i < ItemIDS.Length; i++)
				{
					if (ItemIDS[i] == buttonids[iCorrectPosition])
					{
						itemstr = ItemIDS_Strings[i];
						break;
					}
				}
				string message = "Pick the " + itemstr + " from the choices below.";
				switch (Utility.RandomMinMax(1, 4))
				{
					case 1:
						//use the default above
						break;
					case 2:
						message = "Pick the following from the choices below: " + itemstr + ".";
						break;
					case 3:
						message = "Find the image of the " + itemstr + " to continue.";
						break;
					case 4:
						message = "Select the picture of the " + itemstr + " below to proceed.";
						break;
				}
				AddHtml(x, y, 330, 60, message, false, false);

				y += 65;

				int randomModX = Utility.RandomMinMax(3, 11);
				int randomModY = Utility.RandomMinMax(-5, 5);

				y += 40;
				x += 50; //make the response part smaller :-)
				for (int i = 0; i < 10; i += 2)
				{
					//AddButton(x + randomModX, y - 1, buttonids[i], buttonids[i], responses[i], GumpButtonType.Reply, 0);
					AddButton(x + randomModX, y - 1, 4005, 4007, responses[i], GumpButtonType.Reply, 0);
					AddItem(x + randomModX + 30, y, buttonids[i]);

					//AddButton(x + 100, y - 1, buttonids[i + 1], buttonids[i + 1], responses[i + 1], GumpButtonType.Reply, 0);
					AddButton(x + 100, y - 1, 4005, 4007, responses[i + 1], GumpButtonType.Reply, 0);
					AddItem(x + 100 + 30, y, buttonids[i + 1]);

					y += (50 + randomModY);
				}

			}
		}
	}
}