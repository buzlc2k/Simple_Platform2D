using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D{
    public class CanvasManager : Singleton<CanvasManager>
    {
        private BaseCanvasController currentCanvas;

        [SerializeField] private List<BaseCanvasController> allCanvases = new();

        protected override void Awake()
        {
            GetCanvases();
        }
        private void GetCanvases(){
            if (allCanvases == null || allCanvases.Count == 0)
            {
                allCanvases.AddRange(GetComponentsInChildren<BaseCanvasController>());
            }
        }

        public IEnumerator SetActiveCanvas(GameStateID currentGameState){
            if(currentCanvas != null) 
                yield return StartCoroutine(currentCanvas.TurnOffCanvas());

            foreach(var canvas in allCanvases){
                if(!currentGameState.Equals(canvas.RespondingState)) continue;
                currentCanvas = canvas;
                yield return StartCoroutine(canvas.TurnOnCanvas());
            }
        }
    }
}