using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishApp
{
    public class ImageReferencePopup : BaseUI
    {
        [SerializeField]
        private Image m_ImageReference;
        public Image ImageReference
        {
            get { return m_ImageReference; }
            set { m_ImageReference = value; }
        }
    }
}
