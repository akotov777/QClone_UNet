public class LinearAttenuationDamageCalculator : DamageCalculator
{
    #region Methods

    public override int CalculateDamage(float incomingDamage, float attenuationFactor)
    {
        if ((int)attenuationFactor == 0)
        {
            return (int)incomingDamage;
        }
        return (int)(incomingDamage / attenuationFactor);
    }

    #endregion
}