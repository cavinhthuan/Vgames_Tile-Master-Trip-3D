using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GameConfigs;

[CreateAssetMenu(fileName = "Level", menuName = "Level", order = 0)]
public class Level : ScriptableObject
{
    public int _level;
    public int star;
    public float time;
    public List<LevelData> _levelDatas;
}
[Serializable]
public struct LevelData
{
    public CubeModelData cubeData;
    public int Chance;
}
