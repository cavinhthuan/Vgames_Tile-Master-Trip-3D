using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteResource", menuName = "SpriteResource", order = 0)]
public class SpriteResource : ScriptableObject
{
    public List<Sprite> sprites;
    public List<CubeModelData> cubeModelDatas;
}