using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "RegisteredPrefabs", menuName = "ScriptableObjects/RegisteredPrefabsCollection")]
public class RegisteredPrefabs : ScriptableObject
{
    public List<GameObject> Prefabs;
}
