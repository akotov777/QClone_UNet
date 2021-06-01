using UnityEngine;
using System;


public sealed class DefaultSpawnChooser : IChooseSpawnPointLogic
{
    #region IChooseSpawnPointLogic

    public SpawnPoint ChooseSpawnPoint(SpawnPoint[] spawnPoints)
    {
        System.Random r = new System.Random();
        return spawnPoints[r.Next(spawnPoints.Length)];
    }

    #endregion
}