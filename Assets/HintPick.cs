using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class HintPick : HintGameAbstract
{
    private bool isPicking;
    protected async override void UseHint()
    {
        if(!isPicking)
        {
            isPicking = true;
            var result = await Pick();
            isPicking = false;
        }
    }

    private async Task<bool> Pick()
    {
        var boolObjs = GameManager.Instance.swapController.boolObjects;
        var cubes = boolObjs.Where(cube => boolObjs[0].CubeKey == cube.CubeKey).ToList();
        for(int i = 0; i < cubes.Count && i < ConstantVariables.PICK_NUMBER; i++)
        {
            var cube = cubes[i];
            await PickCorotine(() => cube.OnClickedCube());
        }
        return true;
    }

    private async Task PickCorotine(Action callback)
    {
        if(callback != null)
            callback.Invoke();
        await Task.Delay(ConstantVariables.PICKCOROTINE_DELAY_TIME);
    }
}