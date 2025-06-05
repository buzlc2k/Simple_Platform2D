namespace Platformer2D{
    public class HomeButtonController : ButtonController
    {
        protected override void OnClick()
        {
            Observer.PostEvent(EventID.HomeButton_Clicked, null);
        }
    }
}
