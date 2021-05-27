using System.Collections.Generic;
using UnityEngine;


public static class ColliderExtentions
{
    #region Fields

    private static Dictionary<Collider, List<ICollisionHandler>> _handlersTable;

    #endregion


    #region Methods

    public static void AddCollisionHandler(this Collider collider, ICollisionHandler collisionHandler)
    {
        if (_handlersTable.ContainsKey(collider))
        {
            _handlersTable[collider].Add(collisionHandler);
        }
        else
        {
            var newList = new List<ICollisionHandler>();
            newList.Add(collisionHandler);
            _handlersTable.Add(collider, newList);
        }
    }

    public static void HandleCollision(this Collider collider)
    {
        if (!_handlersTable.ContainsKey(collider))
        {
            return;
        }
        foreach (var handler in _handlersTable[collider])
        {
            handler.HandleCollision();
        }
    }

    #endregion
}
