public class StartButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += GameManager.Instance.StartGame;
    }
}
