namespace Gangster_duel
{
	internal class Attacker : Gangster
	{
		internal int damage;
		internal int accuracy;
		internal int const_damage;
		internal override void Additional_Chancge_Characteristic(int gang)
		{
			const_damage = damage;
			if (gang == 0)
			{
				damage += 15;
				if (stamina > 19)
					stamina -= 20;
			}
			else
			{
				damage -= 10;
			}
		}

		internal override void Characteristic(int gang)
		{
			Adjunction(const_damage, damage);
			Minus_Characteristic(damage, const_damage);
			Additional_Chancge_Characteristic(gang);
			Check_Characteristic(damage, 50, 90);
		}
	}
}
