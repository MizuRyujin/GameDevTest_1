public class ExitButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += GameManager.Instance.ExitGame;
    }
}
