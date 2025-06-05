using System.Collections;
using TMPro;
using UnityEngine;

namespace Platformer2D
{
    public class NumKeyTextController : BaseTextController
    {
        protected override IEnumerator UpdateText()
        {
            while (true)
            {
                if (!gameObject.activeInHierarchy)
                {
                    yield break;
                }

                if(LevelManager.Instance.CurrentLevel != null)
                    text.text = $"{LevelManager.Instance.CurrentKey}/{LevelManager.Instance.CurrentLevel.TargetKeys}";
                    
                yield return null;
            }
        }
    }
}
