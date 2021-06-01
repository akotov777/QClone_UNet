

public class TakingDamageFeature : BasePlayerFeature, ICollisionHandler
{
    #region Fields

    private Player _player;
    private DamageCalculator _damageCalculator;
    private NetworkServices _netServices;

    #endregion


    #region ClassLifeCycles

    public TakingDamageFeature(Player player, NetworkServices netServices, DamageCalculator calculator)
    {
        _player = player;
        _player.Collider.AddCollisionHandler(this);
        _damageCalculator = calculator;
        _netServices = netServices;
    }

    ~TakingDamageFeature()
    {
        _player.Collider.RemoveHandler(this);
    }

    #endregion


    #region Methods

    private void DealDamage(int damage)
    {
        int dealingDamage = _damageCalculator.CalculateDamage(damage, _player.Armor);
        _netServices.CmdChangePlayerHP(_player.HP - dealingDamage);
    }

    #endregion


    #region ICollisionHandler

    public void HandleCollision(CollisionInfo info)
    {
        if (!IsActive)
            return;

        DealDamage(info.IntDamage);
    }

    #endregion
}