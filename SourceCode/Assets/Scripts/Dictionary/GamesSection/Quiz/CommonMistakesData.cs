using System.Collections.Generic;

namespace EnglishApp
{
    public class CommomMistakesData
    {
        private List<SingleCommonMistake> m_CommonMistakesQuiz;
        public List<SingleCommonMistake> CommonMistakesQuiz
        {
            get { return m_CommonMistakesQuiz; }
            set { m_CommonMistakesQuiz = value; }
        }

        public CommomMistakesData()
        {
            m_CommonMistakesQuiz = new List<SingleCommonMistake>();
        }
    }
}
