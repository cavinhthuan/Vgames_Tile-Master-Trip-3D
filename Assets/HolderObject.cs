using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HolderObject : MonoBehaviour, IList<CubeModel>
{
    private List<CubeModel> cubeModelsHolder;
    private int maxCube = 8;
    private List<CubeModel> stackCubes;
    public HolderObject()
    {
        cubeModelsHolder = new List<CubeModel>();
        stackCubes = new List<CubeModel>();
    }
    public CubeModel this[int index] { get => cubeModelsHolder[index]; set => cubeModelsHolder[index] = value; }

    public int Count
    {
        get => cubeModelsHolder.Count;
    }
    public bool IsReadOnly
    {
        get => false;
    }

    public void Add(CubeModel item)
    {
        if(cubeModelsHolder != null && cubeModelsHolder.Count < maxCube)
        {
            int index = cubeModelsHolder.FindLastIndex(c => c.CubeKey == item.CubeKey);

            // if no such cube exists, insert at the end of the list
            if(index == -1)
            {
                cubeModelsHolder.Add(item);
            }
            // otherwise, insert after the last cube with the same key
            else
            {
                cubeModelsHolder.Insert(index + 1, item);
            }
            item.m_rigidbody.isKinematic = true;
            item.m_rigidbody.useGravity = false;
            item.transform.rotation = Quaternion.identity;
            item.transform.SetParent(GameManager.Instance.Holder.transform);
            item.transform.SetSiblingIndex(cubeModelsHolder.IndexOf(item));
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, GameManager.Instance.Holder.transform.position.z);
            var cubes = cubeModelsHolder.Where(cube => cube.CubeKey == item.CubeKey && cube.gameObject.activeSelf == true).ToList();
            if(cubes.Count == 3)
            {
                for(int i = 0; i < cubes.Count; i++)
                {
                    cubes[i].gameObject.SetActive(false);
                    cubeModelsHolder.Remove(cubes[i]);
                    stackCubes.Remove(cubes[i]);
                }
                return;
            }
            stackCubes.Add(item);
            return;
        }
        this.PostEvent(EventID.HolderFullSlot, null);
    }

    public CubeModel GetLastestAdded()
    {
        var lastIndex = stackCubes.Count - 1;
        if(lastIndex < 0)
        {
            return null;
        }
        var cube = stackCubes[lastIndex];
        cubeModelsHolder.Remove(cube);
        stackCubes.Remove(cube);
        cube.transform.SetParent(null);
        return cube;
    }

    public void Clear()
    {
        foreach(var item in cubeModelsHolder)
        {
            BoolGameObject.Revoke(item);
        }
        cubeModelsHolder.Clear();
    }

    public bool Contains(CubeModel item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(CubeModel[] array, int arrayIndex)
    {
    }

    public IEnumerator<CubeModel> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public int IndexOf(CubeModel item)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, CubeModel item)
    {
    }

    public bool Remove(CubeModel item)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
