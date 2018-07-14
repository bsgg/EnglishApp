using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Utility;

namespace EnglishApp
{
    public class CommonMistakesGameControl : Base
    {
        /// <summary>
        /// Common mistakes GUI
        /// </summary>
        public GameCommonMistakesUI     m_CommonMistakesGUI;

        // Colors for the options
        private Color32                 m_ColorBase = new Color32(86, 182, 195, 255);
        private Color32                 m_ColorGreen = new Color32(147, 211, 127, 255);
        private Color32                 m_ColorRed = new Color32(229, 125, 113, 255);

        private List<int>               m_LIndicesQuestions;
        private int                     m_IndexQuestion;
        private SingleCommonMistake     m_CurrentQuestion;
        private int                     m_MaxQuestions = 0;


        // Answer indices and index of correct answer
        private List<int>               m_LIndicesAnswers;
        private int                     m_IndexCorrectAnswer;

        public override void Init()
        {
            // Load the quiz
            GameManager.Instance.DataGamesDictionary.LoadCommonMistakes();
            m_LIndicesAnswers = new List<int>();

            m_LIndicesQuestions = new List<int>();
            m_MaxQuestions = GameManager.Instance.DataGamesDictionary.DataCommonMistakes.CommonMistakesQuiz.Count;
            for (int i = 0; i < m_MaxQuestions; i++)
            {
                m_LIndicesQuestions.Add(i);
            }

            SetNextQuestion();
            m_CommonMistakesGUI.Show();
        }

        private void SetNextQuestion()
        {
            m_LIndicesQuestions.Shuffle();
            m_IndexQuestion = 0;

            m_CurrentQuestion = new SingleCommonMistake();
            m_CurrentQuestion = GameManager.Instance.DataGamesDictionary.GetCommonMistakeQuestion(m_LIndicesQuestions[m_IndexQuestion]);

            if (m_CurrentQuestion != null)
            {
                m_CommonMistakesGUI.Question = m_CurrentQuestion.Question;


                // Get the answers
                m_LIndicesAnswers = new List<int>();

                // Set up the options getting the indices and shuffle the list
                for (int i = 0; i < m_CurrentQuestion.Answers.Count; i++)
                {
                    m_LIndicesAnswers.Add(i);
                }
                m_LIndicesAnswers.Shuffle();

                for (int i = 0; i < m_CommonMistakesGUI.NumberAnswers; i++)
                {
                    // Gets the index of correct answer
                    if (m_LIndicesAnswers[i] == 0)
                    {
                        m_IndexCorrectAnswer = i;
                    }
                    m_CommonMistakesGUI.SetTextOption(m_CurrentQuestion.Answers[m_LIndicesAnswers[i]], i);
                    m_CommonMistakesGUI.SetColorOption(m_ColorBase, i);
                    m_CommonMistakesGUI.EnableOption(true, i);
                }

            }
        }

        public override void Finish()
        {
            m_CommonMistakesGUI.Hide();
        }

        public override void Back()
        {
            Finish();
        }

        /// <summary>
        /// On option pressed
        /// </summary>
        /// <param name="option">Index option</param>
        public void OnOptionPress(int option)
        {
            // Setup options
            for (int i = 0; i < m_CurrentQuestion.Answers.Count; i++)
            {
                m_CommonMistakesGUI.EnableOption(false, i);
            }
            if (option == m_IndexCorrectAnswer)
            {
                m_CommonMistakesGUI.SetColorOption(m_ColorGreen, option);
            }
            else
            {
                m_CommonMistakesGUI.SetColorOption(m_ColorRed, option);
                m_CommonMistakesGUI.SetColorOption(m_ColorGreen, m_IndexCorrectAnswer);
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
    }
}
