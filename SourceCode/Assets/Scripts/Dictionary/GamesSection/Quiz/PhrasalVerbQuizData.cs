using System.Collections.Generic;

namespace EnglishApp
{
    public class PhrasalVerbQuizData
    {
        private List<SingleQuiz> m_PhrasalVerbQuiz;
        public List<SingleQuiz> PhrasalVerbQuiz
        {
            get { return m_PhrasalVerbQuiz; }
            set { m_PhrasalVerbQuiz = value; }
        }

        public PhrasalVerbQuizData()
        {
            m_PhrasalVerbQuiz = new List<SingleQuiz>();
        }
    }
}
