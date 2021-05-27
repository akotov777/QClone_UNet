public class DamageableFeature : BasePlayerFeature, ICollisionHandler
{
    #region Fields

    private Player _player;

    #endregion


    #region ClassLifeCycles

    public DamageableFeature(Player player)
    {
        _player = player;
    }

    #endregion


    #region Methods

    private void DealDamage(int damage)
    {

    }

    #endregion


    #region ICollisionHandler

    public void HandleCollision(CollisionInfo info)
    {

    }

    #endregion
}