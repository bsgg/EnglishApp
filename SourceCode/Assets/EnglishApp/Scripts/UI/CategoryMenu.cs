using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.UI;

namespace EnglishApp
{
    public class CategoryMenu : UIBase
    {
        [Header("GrammarSectionUI")]
        [SerializeField] private ScrollPanelUI m_optionList;
        public ScrollPanelUI OptionList
        {
            get { return m_optionList; }
            set { m_optionList = value; }
        }



    }
}
