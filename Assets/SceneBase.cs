using UnityEngine;

public abstract class SceneBase : MonoBehaviour
{
    public abstract SceneBase LoadScene();
    public virtual bool Close()
    {
        gameObject.SetActive(false);
        return true;
    }

}