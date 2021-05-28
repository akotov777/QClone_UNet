using System.Collections.Generic;


public sealed class DefaultDyingStrategy : IDyingStategy
{
    #region Fields

    private NetworkServices _netServices;
    private Player _player;
    private Dictionary<FeatureType, BasePlayerFeature> _featureTable;

    #endregion


    #region ClassLifeCycles

    public DefaultDyingStrategy(Player player, NetworkServices netServices, Dictionary<FeatureType, BasePlayerFeature> featureTable)
    {
        _player = player;
        _netServices = netServices;
        _featureTable = featureTable;
    }

    #endregion


    #region IDyingStrategy

    public void Perform()
    {
        _netServices.CmdChangeNetworkedObjectMaterials(
            _player.gameObject,
            Constants.ResourcesPaths.Materials.TestDead);

        _featureTable[FeatureType.DamageableFeature].IsActive = false;
        _featureTable[FeatureType.FiringFeature].IsActive = false;
        _featureTable[FeatureType.MovementFeature].IsActive = false;
        _featureTable[FeatureType.DyingFeature].IsActive = false;
    }

    #endregion
}