using System.Collections.Generic;

namespace EnglishApp
{
    public class CrossWord
    {
        private List<string> m_HQuestions;
        public List<string> HQuestions
        {
            set { m_HQuestions = value; }
            get { return m_HQuestions; }
        }

        private List<string> m_HAnswers;
        public List<string> HAnswers
        {
            set { m_HAnswers = value; }
            get { return m_HAnswers; }
        }

        private List<string> m_VQuestions;
        public List<string> VQuestions
        {
            set { m_VQuestions = value; }
            get { return m_VQuestions; }
        }

        private List<string> m_VAnswers;
        public List<string> VAnswers
        {
            set { m_VAnswers = value; }
            get { return m_VAnswers; }
        }

        private List<string> m_CrossWordSolution;
        public List<string> CrossWordSolution
        {
            set { m_CrossWordSolution = value; }
            get { return m_CrossWordSolution; }
        }
        public CrossWord() { }
    }
}
