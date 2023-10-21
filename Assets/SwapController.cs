using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class SwapController : MonoBehaviour
{
    public int numberSwap = 0;
    public List<CubeModel> boolObjects;
    private GameManager manager;

    private Vector3 swapRootPosition;

    private void Start()
    {
        boolObjects = new List<CubeModel>();
        manager = GameManager.Instance;
        swapRootPosition = transform.position;
        //StartCoroutine(Swap());
    }
    public IEnumerator Swap(Level level, bool clear)
    {
        if(clear)
            Clear();
        List<CubeModel> cubeModels = GenerateCubeModels(level);
        boolObjects.AddRange(cubeModels);
        GameObject keeper = manager.Keeper;
        float scaleForce = 30 / 5 + 2;
        float delay = 0.06f;
        int size = cubeModels.Count;
        for(int i = 0; i < size; i++)
        {
            var index = Random.Range(0, cubeModels.Count);
            var cube = cubeModels[index];
            cubeModels.RemoveAt(index);
            yield return new WaitForSeconds(delay);
            yield return StartCoroutine(MoveCubeInSpiral(cube, keeper, scaleForce * (i * 5 / size + 1), i, boolObjects.Count));
        }
    }

    private void Clear()
    {
        foreach(var item in boolObjects)
        {
            BoolGameObject.Revoke(item);
        }
        boolObjects.Clear();
    }
    private List<CubeModel> GenerateCubeModels(Level level)
    {
        List<CubeModel> cubeModels = new List<CubeModel>();

        foreach(var item in level._levelDatas)
        {
            for(int i = 0; i < item.Chance * 3; i++)
            {
                var cube = BoolGameObject.GetBoolObject(item.cubeData);
                cubeModels.Add(cube);
            }
        }
        return cubeModels;
    }
    private IEnumerator MoveCubeInSpiral(CubeModel cube, GameObject keeper, float scaleForce, int currentIndex, int totalCubes)
    {
        float roundNumber = (totalCubes / 5f);
        float angle = (360f / roundNumber) * currentIndex;
        float radius = currentIndex * 3 / cube.transform.localScale.x; // Adjust as needed
        float cubeHeight = cube.transform.localScale.z * cube.meshRenderer.bounds.size.z;
        transform.position = new Vector3(swapRootPosition.x, swapRootPosition.y, swapRootPosition.z - (cubeHeight * currentIndex / 30));

        Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
        //Vector3 newPosition = new Vector3(keeper.transform.position.x, keeper.transform.position.y, transform.position.z - cube.transform.localScale.z) + direction * radius;

        // Adjust the height based on the cube's scale
        //newPosition.y += cubeHeight / 2;
        cube.gameObject.SetActive(true);
        //cube.transform.position = newPosition;
        cube.transform.position = transform.TransformDirection(transform.position);

        cube.m_rigidbody.AddTorque(Vector3.forward * Random.Range(0, 360)); // Add random rotation
        cube.transform.rotation = Quaternion.identity;

        // Use Unity's physics for realistic physics interactions
        cube.m_rigidbody.AddForce(direction.normalized * scaleForce, ForceMode.Impulse);

        yield return null;
    }

    public void Remove(CubeModel gameObject)
    {
        if(boolObjects.Remove(gameObject))
        {
            if(boolObjects.Count == 0)
            {
                this.PostEvent(EventID.OnSolved, null);
            }
        }
    }

    public IEnumerator Swap(CubeModel cube)
    {
        boolObjects.Add(cube);
        yield return StartCoroutine(MoveCubeInSpiral(cube, manager.Keeper, 10, boolObjects.Count - 1, boolObjects.Count));
    }
}
