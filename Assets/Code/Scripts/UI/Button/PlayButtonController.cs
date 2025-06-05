namespace Platformer2D{
    public class PlayButtonController : ButtonController
    {
        protected override void OnClick()
        {
            Observer.PostEvent(EventID.PlayButton_Clicked, null);
        }
    }
}
