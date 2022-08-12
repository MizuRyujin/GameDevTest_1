public class ReturnButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += LoadingManager.Instance.ReturnToMenu;
        ButtonAction += GameManager.Instance.PauseGame;
    }
}
