public class HintReturn : HintGameAbstract
{
    protected override void UseHint()
    {
        var cube = GameManager.Instance.holder.GetLastestAdded();
        if(cube != null)
        {
            StartCoroutine(GameManager.Instance.swapController.Swap(BoolGameObject.ResetBoolObject(cube)));
        }
    }
}
