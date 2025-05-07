namespace Gangster_duel
{
	internal static class AllGangsters
	{
		internal static Attacker[] BloodsAttackers()
		{
			return new Attacker[]
			{
				new Attacker
				{
					name = "Red_Joe",
					stamina = 100,
					damage = 70,
					accuracy = 80
				},
				new Attacker
				{
					name = "Small_Tim",
					stamina = 100,
					damage = 68,
					accuracy = 82
				},
				new Attacker
				{
					name = "Crazy_Franklin",
					stamina = 95,
					damage = 72,
					accuracy = 78
				},
				new Attacker
				{
					name = "Fast_Lamar",
					stamina = 95,
					damage = 74,
					accuracy = 76
				}
			};
		}

		internal static Defender[] BloodsDefenders()
		{
			return new Defender[]
			{
				new Defender
				{
					name = "Big_CJ",
					stamina = 90,
					defence = 85,
					reaction = 80
				},
				new Defender
				{
					name = "Bad_Kai",
					stamina = 90,
					defence = 82,
					reaction = 83
				}
			};
		}

		internal static Attacker[] CripsAttackers()
		{
			return new Attacker[]
			{
				new Attacker
				{
					name = "Chill_Tony",
					stamina = 100,
					damage = 65,
					accuracy = 88
				},
				new Attacker
				{
					name = "Angry_George",
					stamina = 100,
					damage = 70,
					accuracy = 80
				},
				new Attacker
				{
					name = "Lil_T",
					stamina = 95,
					damage = 75,
					accuracy = 72
				},
				new Attacker
				{
					name = "Black_M",
					stamina = 95,
					damage = 78,
					accuracy = 70
				}
			};
		}

		internal static Defender[] CripsDefenders()
		{
			return new Defender[]
			{
				new Defender
				{
					name = "James",
					stamina = 95,
					defence = 90,
					reaction = 85
				},
				new Defender
				{
					name = "Blue_D",
					stamina = 90,
					defence = 85,
					reaction = 80
				}
			};
		}
	}
}
