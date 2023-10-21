using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScene : SceneBase
{
    public TextMeshProUGUI star;
    public TextMeshProUGUI txt_time;

    public override SceneBase LoadScene()
    {
        star.text = DataHolder.Instance.datas.Star.ToString();
        return this;
    }
}
