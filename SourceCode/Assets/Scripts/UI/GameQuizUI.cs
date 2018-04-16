using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Utility;


namespace EnglishApp
{
    public class GameQuizUI : UIBase
    {
        [Header("Game Quiz UI")]

        [SerializeField] private Text       m_LblQuestion;
        public string Question
        {
            set { m_LblQuestion.text = value; }
            get { return m_LblQuestion.text; }
        }
        [SerializeField] private Text       m_LblTranslation;
        public string Translation
        {
            set { m_LblTranslation.text = value; }
            get { return m_LblTranslation.text; }
        }
        [SerializeField] private Text       m_LblNumberQuestions;
        public string NumberQuestions
        {
            set { m_LblNumberQuestions.text = value; }
            get { return m_LblNumberQuestions.text; }
        }
        [SerializeField] private GameObject m_NextQuestionButton;
        public GameObject NextQuestionButton
        {
            set { m_NextQuestionButton = value; }
            get { return m_NextQuestionButton; }
        }
        [SerializeField] private GameObject m_FinishGameButton;
        public GameObject FinishGameButton
        {
            set { m_FinishGameButton = value; }
            get { return m_FinishGameButton; }
        }

        [Header("Options")]
        [SerializeField] private TextButton[] m_OptionsButtons;

        public void SetColorOption(Color32 colorOption, int indexOp)
        {
            if (indexOp < m_OptionsButtons.Length)
            {
                m_OptionsButtons[indexOp].ColorButton = colorOption;
            }

        }
        public void SetTextOption(string textOpt, int indexOp)
        {
            if (indexOp < m_OptionsButtons.Length)
            {
                m_OptionsButtons[indexOp].Text = textOpt;
            }
        }

        public void EnableOption(bool enable, int indexOp)
        {
            if (indexOp < m_OptionsButtons.Length)
            {
                m_OptionsButtons[indexOp].EnableButton = enable;
            }
        }
    }

}