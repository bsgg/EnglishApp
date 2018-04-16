using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class PopupScoreUI : UIBase
    {
        [SerializeField]
        private Text m_LblScore;
        public string LblScore
        {
            get { return m_LblScore.text; }
            set { m_LblScore.text = value; }
        }
    }
}
