public class LinearAttenuationDamageCalculator : DamageCalculator
{
    #region Methods

    public override int CalculateDamage(float incomingDamage, float attenuationFactor)
    {
        return (int)(incomingDamage / attenuationFactor);
    }

    #endregion
}