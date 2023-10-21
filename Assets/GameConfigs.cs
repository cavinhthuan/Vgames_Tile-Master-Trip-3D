using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
[CreateAssetMenu(fileName = "GameConfigs", menuName = "GameConfigs", order = 0)]
public class GameConfigs : ScriptableObject
{
    [SerializeField] private bool isDev;
    [SerializeField] private string _startScene;
    [SerializeField] private List<Level> _levels;

    public string StartScene
    {
        get => _startScene;
    }
    public List<Level> Levels
    {
        get => _levels;
    }

    public SpriteResource SpritesResource;

    public bool IsDev
    {
        get => isDev;
    }
}
