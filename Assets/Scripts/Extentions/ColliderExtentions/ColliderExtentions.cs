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
        Debug.Log("InAddHandler");

        if (_handlersTable.ContainsKey(collider))
        {
            Debug.Log("HasKey-AddingHandler");

            _handlersTable[collider].Add(collisionHandler);
        }
        else
        {
            Debug.Log("NoKey-AddNewList-AddHandler");

            var newList = new List<ICollisionHandler>();
            newList.Add(collisionHandler);
            _handlersTable.Add(collider, newList);
        }
        var keys = _handlersTable.Keys;
        Debug.Log(keys.Count);
        Debug.Log("EndAddHandler");



    }

    public static void HandleCollision(this Collider collider, CollisionInfo info)
    {
        Debug.Log("InHandle");
        if (!_handlersTable.ContainsKey(collider))
        {
            Debug.Log("DoesntContainKey");
            return;
        }
        if (_handlersTable[collider].Count == 0)
        {
            Debug.Log("NoHandler");
            return;
        }

        foreach (var handler in _handlersTable[collider])
        {
            Debug.Log("Handling");

            handler.HandleCollision(info);
        }
        Debug.Log("EndHandle");
    }

    public static void RemoveHandler(this Collider collider, ICollisionHandler collisionHandler)
    {
        if (!_handlersTable.ContainsKey(collider))
            return;

        _handlersTable[collider].Remove(collisionHandler);
    }

    #endregion
}
