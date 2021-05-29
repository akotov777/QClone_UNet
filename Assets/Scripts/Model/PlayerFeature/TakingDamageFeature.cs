using UnityEngine;


public class TakingDamageFeature : BasePlayerFeature, ICollisionHandler
{
    #region Fields

    private Player _player;
    private DamageCalculator _damageCalculator;

    #endregion


    #region ClassLifeCycles

    public TakingDamageFeature(Player player, DamageCalculator calculator)
    {
        _player = player;
        _player.Collider.AddCollisionHandler(this);
        _damageCalculator = calculator;
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
        _player.HP -= dealingDamage;
    }

    #endregion


    #region ICollisionHandler

    public void HandleCollision(CollisionInfo info)
    {
        Debug.Log("InDamageFeature");
        if (!IsActive)
        {
            Debug.Log("Feature not active");

            return;
        }

        DealDamage(info.IntDamage);
    }

    #endregion
}