using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class PopupScoreUI : BaseUI
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
