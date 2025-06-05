using System.Collections;
using UnityEngine;

namespace Platformer2D{
    public class CanvasWithContainerController : BaseCanvasController
    {
        [SerializeField] private ContainerUIElement containerUIElement;
        public override IEnumerator TurnOffCanvas()
        {
            yield return StartCoroutine(containerUIElement.TurnOffElement());
        }

        public override IEnumerator TurnOnCanvas()
        {
            yield return StartCoroutine(containerUIElement.TurnOnElement());
        }
    }
}