using System.Collections;
using TMPro;
using UnityEngine;

namespace Platformer2D
{
    public class NumLevelTextController : BaseTextController
    {
        protected override IEnumerator UpdateText()
        {
            while(LevelManager.Instance.CurrentLevel == null)
                yield return null;
                
            text.text = $"Level: {LevelManager.Instance.CurrentLevel.Index}";
            yield return null;
        }
    }
}
