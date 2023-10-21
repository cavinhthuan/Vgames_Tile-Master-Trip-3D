using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;
public class BoolGameObject : MonoBehaviour
{
    private BoolGameObject _instance;
    public static BoolGameObject Instance;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            Instance = _instance;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        boolObject = new Dictionary<int, List<CubeModel>>();
    }
    Dictionary<int, List<CubeModel>> boolObject;
    public static CubeModel GetBoolObject(CubeModelData data)
    {
        CubeModel cube;
        if(BoolGameObject.Instance.boolObject.ContainsKey(data.CubeKey))
        {
            cube = BoolGameObject.Instance.boolObject[data.CubeKey].FirstOrDefault(cube => !cube.onBoolActive);
            if(cube != null)
            {
                return ResetBoolObject(cube);
            }
        }
        else
        {
            BoolGameObject.Instance.boolObject.Add(data.CubeKey, new List<CubeModel>());
        }

        cube = Instantiate(GameManager.Instance.cubeModel).GetComponent<CubeModel>();
        cube.CubeKey = data.CubeKey;
        cube.gameObject.SetActive(false);
        Instance.boolObject[data.CubeKey].Add(cube);
        cube.onBoolActive = true;
        cube.transform.SetParent(null);
        cube.SetSprite(data.CubeSprite);
        return cube;
    }

    public static CubeModel GetBoolObject(int cubeKey)
    {
        CubeModel cube;
        if(BoolGameObject.Instance.boolObject.ContainsKey(cubeKey))
        {
            cube = BoolGameObject.Instance.boolObject[cubeKey].FirstOrDefault(cube => !cube.onBoolActive);
            if(cube != null)
            {
                cube.onBoolActive = true;
                cube.transform.SetParent(null);
                cube.transform.position = GameManager.Instance.cubeModel.transform.position;
                cube.m_rigidbody.isKinematic = false;
                cube.m_rigidbody.useGravity = true;
                return cube;
            }
        }
        return null;
    }
    public static void Revoke(CubeModel item)
    {
        item.onBoolActive = false;
        item.gameObject.SetActive(false);
    }

    public static CubeModel ResetBoolObject(CubeModel cube)
    {
        cube.onBoolActive = true;
        cube.transform.SetParent(null);
        cube.transform.position = GameManager.Instance.cubeModel.transform.position;
        cube.m_rigidbody.isKinematic = false;
        cube.m_rigidbody.useGravity = true;
        return cube;
    }
}