namespace Shooting2D
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Image))]
    internal sealed class Healthbar : MonoBehaviour
    {
        private Image fillImage;

        public float FillAmount
        {
            get { return this.fillImage.fillAmount; }
            set { this.fillImage.fillAmount = Mathf.Clamp01(value); }
        }

        private void Awake()
        {
            this.fillImage = this.GetComponent<Image>();
        }
    }
}