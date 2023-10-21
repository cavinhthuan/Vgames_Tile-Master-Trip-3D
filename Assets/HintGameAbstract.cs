using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public abstract class HintGameAbstract : MonoBehaviour
{
    public class HintConfig
    {
        public int maxUse;
        public int currentUse;
        public bool isActive;
    }
    public int maxUse;
    public int currentUse;
    public bool isActive;
    public void Config(Action<HintConfig> configAction)
    {
        if(configAction != null)
        {
            HintConfig hintConfig = new HintConfig();
            configAction(hintConfig);
            this.maxUse = hintConfig.maxUse;
            this.currentUse = hintConfig.currentUse;
            this.isActive = hintConfig.isActive;
        }
    }

    private void Start()
    {
        this.Config((config) =>
        {
            SetConfigData(ref config);
        });
    }

    public virtual void SetConfigData(ref HintConfig config)
    {
        config.maxUse = ConstantVariables.HINT_MAX_USED;
        config.currentUse = ConstantVariables.HINT_MIN_USE;
        config.isActive = true;
    }

    public void Use()
    {
        if(!isActive || DataHolder.Instance.configs.IsDev)
        {
            UseHint();
            return;
        }

        if(isActive && currentUse < maxUse)
        {
            currentUse++;
            UseHint();
            return;
        }

        if(isActive && currentUse < maxUse)
        {
            ModalController.Instance.openModelMessage(ConstantVariables.OUT_OF_STOCK);
        }
    }

    protected virtual void UseHint()
    {
        ModalController.Instance.openModelMessage(ConstantVariables.COMING_SOON);
    }
}
