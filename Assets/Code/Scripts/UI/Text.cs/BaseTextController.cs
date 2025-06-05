using System.Collections;
using TMPro;
using UnityEngine;

namespace Platformer2D
{
    public abstract class BaseTextController : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI text;

        protected virtual void Awake()
        {
            if (text == null) text = GetComponent<TextMeshProUGUI>();
        }

        protected virtual void OnEnable()
        {
            StartCoroutine(UpdateText());
        }

        protected abstract IEnumerator UpdateText();
    }   
}