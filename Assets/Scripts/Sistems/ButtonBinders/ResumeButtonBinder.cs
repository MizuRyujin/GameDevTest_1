public class ResumeButtonBinder : BaseButtonBinder
{
    protected override void BindMethods()
    {
        ButtonAction += GameManager.Instance.PauseGame;
    }
}
