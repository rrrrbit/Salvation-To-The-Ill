using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Asset Aliases", menuName = "Game/Asset Aliases", order = -1)]
public class AssetAliases : ScriptableObject
{
    public AssetAliasGroup[] groups;
    public AssetAliasGroup this[string groupName]
    {
        get
        {
            return groups.First(x => x.name == groupName);
        }
    }
}

[System.Serializable]
public struct AssetAliasGroup
{
    public string name;
    public AssetAlias[] assetAliases;
    public Object this[string alias]
    {
        get
        {
            return assetAliases.First(x => x.alias == alias).asset;
        }
    }
}

[System.Serializable]
public struct AssetAlias
{
    public string alias; 
    public Object asset;
}