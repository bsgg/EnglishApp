using System.Collections.Generic;

namespace EnglishApp
{
    public class SingleQuiz
    {
        private string m_Question;
        public string Question
        {
            set { m_Question = value; }
            get { return m_Question; }
        }

        private string m_Answer;
        public string Answer
        {
            set { m_Answer = value; }
            get { return m_Answer; }
        }

        private string m_Translation;
        public string Translation
        {
            set { m_Translation = value; }
            get { return m_Translation; }
        }

        private List<string> m_Options;
        public List<string> Options
        {
            set { m_Options = value; }
            get { return m_Options; }
        }

        public SingleQuiz() { }
    }
}
