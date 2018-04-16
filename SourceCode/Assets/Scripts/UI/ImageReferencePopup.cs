using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class ImageReferencePopup : UIBase
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
