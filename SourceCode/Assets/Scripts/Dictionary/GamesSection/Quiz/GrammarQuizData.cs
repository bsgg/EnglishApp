using System.Collections.Generic;

namespace EnglishApp
{
    public class GrammarQuizData
    {
        private List<SingleQuiz> m_GrammarQuiz;
        public List<SingleQuiz> GrammarQuiz
        {
            get { return m_GrammarQuiz; }
            set { m_GrammarQuiz = value; }
        }

        public GrammarQuizData()
        {
            m_GrammarQuiz = new List<SingleQuiz>();
        }
    }
}
