using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class PopupTableUI : BaseUI
    {
        [Header("PopupTable")]
        [SerializeField] private Text       m_TitleTable;
        public string TitleTable
        {
            get { return m_TitleTable.text; }
            set { m_TitleTable.text = value; }
        }

        [SerializeField] private Table      m_TableGrammar;
        
        public void InitPopup(TableObject _objTable)
        {
            m_TableGrammar.InitTable(_objTable.Table, _objTable.NRows, _objTable.NCols, _objTable.HeightTable, _objTable.HasHeaderRow);
            m_TitleTable.text = _objTable.Title;
        }
    }
}
