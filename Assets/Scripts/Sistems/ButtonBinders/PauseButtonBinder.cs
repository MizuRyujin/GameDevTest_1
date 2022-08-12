public class PauseButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += GameManager.Instance.PauseGame;
    }
}
