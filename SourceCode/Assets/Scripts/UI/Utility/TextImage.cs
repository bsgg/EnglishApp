using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class TextImage : MonoBehaviour
    {

        public Text labelComponet;
        public Image imageComponent;

        public Color32 ColorButton
        {
            set { this.imageComponent.color = value; }
            get { return this.imageComponent.color; }
        }

        public string Text
        {
            set { this.labelComponet.text = value; }
            get { return this.labelComponet.text; }
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
        }

        public void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
