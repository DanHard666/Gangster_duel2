namespace Gangster_duel
{
	public abstract class Gangster
	{
		internal string name;
		internal int stamina;
		bool _isInitialized;
		int _charact;

		internal void Adjunction(int const_charact, int charact)
		{
			if (!_isInitialized)
			{
				const_charact = charact;
				_isInitialized = true;
			}
		}

		internal void Checking_stamina(int negative_stamina)
		{
			stamina -= negative_stamina;
			if (stamina < 0)
			{
				stamina = 0;
			}
		}
		internal virtual void Minus_Characteristic(int characteristic, int const_charact)
		{
			characteristic = const_charact - (100 - stamina) / 18 * 12;
		}
		internal abstract void Additional_Chancge_Characteristic(int gang);

		internal virtual void Check_Characteristic(int characteristic, int minimum, int max)
		{
			if (characteristic < minimum)
				characteristic = minimum;
			else
			{
				if (characteristic > max)
					characteristic = max;
			}
		}
		internal virtual void Characteristic(int gang)
		{
			Adjunction(1, 2);
			Minus_Characteristic(_charact, _charact);
			Additional_Chancge_Characteristic(gang);
			Check_Characteristic(_charact, 1, 2);
		}
	}
}
