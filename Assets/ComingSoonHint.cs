public class ComingSoonHint: HintGameAbstract
{
    public override void SetConfigData(ref HintConfig config)
    {
        config.maxUse = int.MaxValue;
        config.currentUse = 0;
        config.isActive = false;
    }
}