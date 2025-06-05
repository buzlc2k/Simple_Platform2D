namespace Platformer2D{
    public class PauseButtonController : ButtonController
    {
        protected override void OnClick()
        {
            Observer.PostEvent(EventID.PauseButton_Clicked, null);
        }
    }
}
