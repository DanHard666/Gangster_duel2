namespace Gangster_duel
{
	internal class Defender : Gangster
	{
		internal int defence;
		internal int reaction;
		internal int const_defender;

		internal override void Additional_Chancge_Characteristic(int gang)
		{
			if (gang == 0)
			{
				if (stamina > 19)//
					stamina -= 20;//
			}
			else
			{
				defence += 15;
			}
		}

		internal override void Characteristic(int gang)
		{
			Adjunction(const_defender, defence);
			Minus_Characteristic(defence, const_defender);
			Additional_Chancge_Characteristic(gang);
			Check_Characteristic(defence, 60, 100);
		}
	}
}
