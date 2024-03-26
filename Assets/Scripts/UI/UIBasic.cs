using UnityEngine;

namespace Scripts.UI
{
    public abstract class UIBasic : MonoBehaviour
    {
        public void Show()
            => gameObject.SetActive(true);

        public void Hide()
            => gameObject.SetActive(false);
    }
}