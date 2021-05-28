using System.Collections.Generic;
using UnityEngine;


public static class ColliderExtentions
{
    #region Fields

    private static Dictionary<Collider, List<ICollisionHandler>> _handlersTable;

    #endregion


    #region ClassLifeCycles

    static ColliderExtentions()
    {
        _handlersTable = new Dictionary<Collider, List<ICollisionHandler>>();
    }

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

    public static void HandleCollision(this Collider collider, CollisionInfo info)
    {
        if (!_handlersTable.ContainsKey(collider))
            return;
        if (_handlersTable[collider].Count == 0)
            return;

        foreach (var handler in _handlersTable[collider])
        {
            handler.HandleCollision(info);
        }
    }

    public static void RemoveHandler(this Collider collider, ICollisionHandler collisionHandler)
    {
        if (!_handlersTable.ContainsKey(collider))
            return;

        _handlersTable[collider].Remove(collisionHandler);
    }

    #endregion
}
