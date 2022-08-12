public class StartButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += LoadingManager.Instance.StartGame;
    }
}
