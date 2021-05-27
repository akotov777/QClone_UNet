public class DamageableFeature : BasePlayerFeature, ICollisionHandler
{
    #region Fields

    private Player _player;
    private DamageCalculator _damageCalculator;

    #endregion


    #region ClassLifeCycles

    public DamageableFeature(Player player, DamageCalculator calculator)
    {
        _player = player;
        _damageCalculator = calculator;
    }

    #endregion


    #region Methods

    private void DealDamage(int damage)
    {
        int dealingDamage = _damageCalculator.CalculateDamage(damage, _player.Armor);
        _player.HP -= dealingDamage;
    }

    #endregion


    #region ICollisionHandler

    public void HandleCollision(CollisionInfo info)
    {
        DealDamage(info.IntDamage);
    }

    #endregion
}