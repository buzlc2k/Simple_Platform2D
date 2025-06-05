using UnityEngine;

namespace Platformer2D{
    public class ContinueButtonController : ButtonController
    {
        protected override void OnClick()
        {
            Observer.PostEvent(EventID.ContinueButton_Clicked, null);
        }
    }
}
