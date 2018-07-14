using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utility;

namespace EnglishApp
{
    public class QuizGameControl : Base
    {
        /// <summary>
        /// Number of questions
        /// </summary>
        const int NUMBERQUESTIONS = 10;
        /// <summary>
        /// Time per question
        /// </summary>
        const int TIMEQUESTION = 20;

        [Header("UI Elements")]
        [SerializeField] private GameQuizUI                 m_GameQuizGUI;
        [SerializeField] private PopupScoreUI               m_PopupScore;
        [SerializeField] private ProgressBarUI              m_ProgressBarAnswerTime;

        // Colors for the options
        private Color32                                     m_ColorBase = new Color32(86, 182, 195, 255);
        private Color32                                     m_ColorGreen = new Color32(147, 211, 127, 255);
        private Color32                                     m_ColorRed = new Color32(229, 125, 113, 255);        

        private List<int>                                   m_LIndicesQuestions;
        private SingleQuiz                                  m_CurrentQuestion;
        private int                                         m_IndexQuestion;
        private int                                         m_MaxQuestions = 0;
        private int                                         m_NumberCorrect = 0;

        // Answer indices and index of correct answer
        private List<int>                                   m_LIndicesAnswers;
        private int                                         m_IndexCorrectAnswer;

        #region BaseControl
        public override void Init()
        {
            m_PopupScore.Hide();

            // Load the quiz
            GameManager.Instance.DataGamesDictionary.LoadQuiz();

            m_ProgressBarAnswerTime.OnFinish += OnFinishProgressBar;

            m_MaxQuestions = NUMBERQUESTIONS;

            m_LIndicesQuestions = new List<int>();
            for (int i = 0; i < GameManager.Instance.DataGamesDictionary.NumberElements(); i++)
            {
                m_LIndicesQuestions.Add(i);
            }

            InitQuiz();
            m_GameQuizGUI.FinishGameButton.SetActive(false);
            m_GameQuizGUI.Show();
        }

        private void InitQuiz()
        {
            m_NumberCorrect = 0;
            m_IndexQuestion = 0;
            m_LIndicesQuestions.Shuffle();
            m_LIndicesAnswers = new List<int>();
            m_GameQuizGUI.FinishGameButton.SetActive(false);
            SetNextQuestion();
        }

        /// <summary>
        /// Sets the next question.
        /// </summary>
        private void SetNextQuestion()
        {
            m_GameQuizGUI.NextQuestionButton.SetActive(false);
            StopCoroutine(RoutineShowAnswer());

            // Set question
            m_CurrentQuestion = new SingleQuiz();
            m_CurrentQuestion = GameManager.Instance.DataGamesDictionary.GetQuizQuestion(m_LIndicesQuestions[m_IndexQuestion]);

            m_GameQuizGUI.NumberQuestions= (m_IndexQuestion + 1).ToString() + "/" + m_MaxQuestions;

            if (m_CurrentQuestion != null)
            {
                m_GameQuizGUI.Question = m_CurrentQuestion.Question;
                m_GameQuizGUI.Translation = "";

                m_LIndicesAnswers = new List<int>();
                // Set up the options getting the indices and shuffle the list
                for (int i = 0; i < m_CurrentQuestion.Options.Count; i++)
                {
                    m_LIndicesAnswers.Add(i);
                }
                m_LIndicesAnswers.Shuffle();


                // Setup options
                for (int i = 0; i < m_CurrentQuestion.Options.Count; i++)
                {
                    // Gets the correct index answer (always the 0 position in JSON)
                    if (m_LIndicesAnswers[i] == 0)
                    {
                        m_IndexCorrectAnswer = i;
                    }

                    string textOption = m_CurrentQuestion.Options[m_LIndicesAnswers[i]];
                    m_GameQuizGUI.SetTextOption(textOption, i);
                    m_GameQuizGUI.SetColorOption(m_ColorBase, i);
                    m_GameQuizGUI.EnableOption(true, i);
                }

                m_ProgressBarAnswerTime.Enable();
                m_ProgressBarAnswerTime.InitProgressBar(TIMEQUESTION);
            }
        }

        public override void Finish()
        {
            m_ProgressBarAnswerTime.OnFinish -= OnFinishProgressBar;

            m_GameQuizGUI.Hide();
            m_PopupScore.Hide();
        }

        public override void Back()
        {
            Finish();
        }

        private void OnFinishProgressBar()
        {
            m_ProgressBarAnswerTime.Disable();
            StartCoroutine(RoutineShowAnswer());
        }

        #endregion BaseControl

        private IEnumerator RoutineShowAnswer()
        {
            yield return new WaitForSeconds(0.05f);
            SetAnswer();

            // Sets the correct answer marked in red
            m_GameQuizGUI.SetColorOption(m_ColorRed, m_IndexCorrectAnswer);
        }

        private void SetAnswer()
        {
            // Sets the correct answer and translation
            m_GameQuizGUI.Question = m_CurrentQuestion.Answer;
            m_GameQuizGUI.Translation = "<color=#93d47f>" + m_CurrentQuestion.Translation + "</color>";

            // Disable options
            for (int i = 0; i < m_CurrentQuestion.Options.Count; i++)
            {
                m_GameQuizGUI.EnableOption(false, i);
            }

            // Check if it's the last answer
            if ((m_IndexQuestion + 1) < m_MaxQuestions)
            {
                m_GameQuizGUI.NextQuestionButton.SetActive(true);
            }
            else
            {
                m_GameQuizGUI.FinishGameButton.SetActive(true);
            }
        }

        #region BUTTON_HANDLES

        /// <summary>
        /// On option pressed
        /// </summary>
        /// <param name="option">Index option</param>
        public void OnOptionPress(int option)
        {
            StopCoroutine(RoutineShowAnswer());
            m_ProgressBarAnswerTime.Disable();
            SetAnswer();

            if (option == m_IndexCorrectAnswer)
            {
                m_GameQuizGUI.SetColorOption(m_ColorGreen, option);
                m_NumberCorrect++;
            }
            else
            {
                m_GameQuizGUI.SetColorOption(m_ColorRed, option);
            }
        }

        /// <summary>
        /// On the next question pressed
        /// </summary>
        public void OnNextQuestion()
        {
            m_IndexQuestion++;
            if (m_IndexQuestion < m_MaxQuestions)
            {
                SetNextQuestion();
            }
        }

        /// <summary>
        /// On the next question pressed
        /// </summary>
        public void OnFinishExercise()
        {
            m_PopupScore.LblScore = m_NumberCorrect.ToString() + "/" + m_MaxQuestions.ToString();
            m_PopupScore.Show();
        }
        #endregion BUTTON_HANDLES

        #region Button Popup Handles
        public void OnRetryPopupPress()
        {
            m_PopupScore.Hide();
            InitQuiz();
        }

        public void OnFinishPopupPress()
        {
            GameManager.Instance.GamesSectionControl.Back();
        }
        #endregion Button Popup Handles

    }
}
