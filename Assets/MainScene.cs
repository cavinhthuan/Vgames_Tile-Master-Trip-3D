using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class MainScene : SceneBase
{
    public TextMeshProUGUI txt_star;
    public TextMeshProUGUI txt_level;
    public Button btn_play;

    public override SceneBase LoadScene()
    {
        txt_star.text = DataHolder.Instance.datas.Star.ToString();
        txt_level.text = string.Format(ConstantVariables.LEVEL_FORMAT, DataHolder.Instance.datas.CurrentLevel);
        return this;
    }
}
