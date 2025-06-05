using System.Collections;
using UnityEngine;

namespace Platformer2D{
    public class SelectedLevelCanvasController : CanvasWithContainerController
    {
        [SerializeField] LevelButtonController levelButtonPrefab;
        [SerializeField] Transform levelButtonCointainer;

        private void Start()
        {
            GenerateLevelButton();
        }

        private void GenerateLevelButton()
        {
            var currentHighestLevel = LevelManager.Instance.CurrentHighestLevel;
            var totalLevel = LevelManager.Instance.TotalLevel;

            for (int i = 1; i <= totalLevel; i++)
            {
                LevelButtonController button = Instantiate(levelButtonPrefab, levelButtonCointainer);
                button.SetIndexForButton(i, i <= currentHighestLevel);
            }
        }
    }
}