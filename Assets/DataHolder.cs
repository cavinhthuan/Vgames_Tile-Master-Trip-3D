using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHolder : MonoBehaviour
{
    private static DataHolder _instance;
    public static DataHolder Instance
    {
        get => _instance;
    }
    public GameConfigs configs;
    public GameDatas datas;
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            datas.Load();
            return;
        }
        Destroy(this.gameObject);
    }
    private void Start()
    {
        if(configs.IsDev)
        {
            datas.UpdateCurrentLevel(0);
        }
    }
    [ContextMenu("Generate Level from resource")]
    private void GenerateLevel()
    {
        foreach(Level level in configs.Levels)
        {
            if(configs.SpritesResource != null && level._levelDatas.Count < configs.SpritesResource.cubeModelDatas.Count)
            {
                level._levelDatas.Clear();
                foreach(var data in configs.SpritesResource.cubeModelDatas)
                {
                    if(level._levelDatas == null)
                    {
                        level._levelDatas = new List<LevelData>();
                    }
                    level._levelDatas.Add(new LevelData()
                    {
                        cubeData = data,
                        Chance = 0
                    });
                }

            }
            else
            {
                level._levelDatas.Clear();
            }
        }

    }

    [ContextMenu("Update CubeModelData")]
    private void UpdateCubeModelData()
    {
        var rs = this.configs.SpritesResource;
        if(rs != null)
        {
            if(rs.cubeModelDatas == null)
                rs.cubeModelDatas = new List<CubeModelData>();
            else
            {
                rs.cubeModelDatas.Clear();
            }
            foreach(Sprite sprite in rs.sprites)
            {
                rs.cubeModelDatas.Add(new CubeModelData()
                {
                    CubeKey = rs.cubeModelDatas.Count,
                    CubeName = sprite.name,
                    CubeSprite = sprite
                });
            }
            Debug.Log("Update success");
            return;
        }
        Debug.Log("Update failed");
    }

}
