using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class TextButton : MonoBehaviour
    {

        public Text labelComponet;
        public Image imageComponent;
        public Button buttonComponent;

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

        public bool EnableButton
        {
            set { this.buttonComponent.enabled = value; }
            get { return this.buttonComponent.enabled; }
        }

        public bool EnableObject
        {
            set { this.gameObject.SetActive(value); }
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
