using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using CrossWordUtil;

namespace EnglishApp
{
    public class CellCrossWord : MonoBehaviour
    {        
        [SerializeField] private Button     m_ButtonComponent;
        [SerializeField] private Image      m_BackgroundComponent;
        [SerializeField] private Text       m_LetterComponent;

        private bool m_MarkAsCorrect = false;
        public bool IsMarkAsCorrect
        {
            get { return m_MarkAsCorrect; }
        }

        void Awake()
        {
            m_ButtonComponent = transform.GetComponent<Button>();
            m_BackgroundComponent = transform.GetChild(0).GetComponent<Image>();
            m_LetterComponent = transform.GetChild(1).GetComponent<Text>();
            m_MarkAsCorrect = false;

        }

        /*private bool isBlock = false;
        public bool IsBlock
        {
            set
            {
                if (value)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
                }
                isBlock = value;
            }
            get { return isBlock; }
        }*/

        private bool m_Empty = false;
        public bool Empty
        {
            set
            {
                if (value)
                {
                    m_LetterComponent.text = CrossWordGenerator.EMPTYCHARACTER.ToString();
                    gameObject.SetActive(false);
                }
                else
                {
                    m_LetterComponent.text = "";
                    gameObject.SetActive(true);
                }
                m_Empty = value;
            }
            get { return m_Empty; }
        }

        public string Letter
        {
            set { m_LetterComponent.text = value; }
            get { return m_LetterComponent.text; }
        }

        public Button ButtonComponent
        {
            set { m_ButtonComponent = value; }
            get { return m_ButtonComponent; }
        }

        public void BlockButton()
        {
            m_ButtonComponent.enabled = false;
        }

        public Color32 ColorBackground
        {
            set { m_BackgroundComponent.color = value; }
            get { return m_BackgroundComponent.color; }
        }

        public void MarkCorrect(Color32 color)
        {
            m_ButtonComponent.enabled = false;
            m_MarkAsCorrect = true;
            m_BackgroundComponent.color = color;
        }
    }
}
