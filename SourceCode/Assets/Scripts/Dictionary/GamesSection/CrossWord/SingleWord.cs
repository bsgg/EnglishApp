
namespace EnglishApp
{
    public class SingleWord
    {
        private string m_Answer;
        public string Answer
        {
            set { m_Answer = value; }
            get { return m_Answer; }
        }

        private string m_Question;
        public string Questions
        {
            set { m_Question = value; }
            get { return m_Question; }
        }

        private bool m_IsInCrossWord;
        public bool IsInCrossWord
        {
            set { m_IsInCrossWord = value; }
            get { return m_IsInCrossWord; }
        }
        public SingleWord()
        {
            m_IsInCrossWord = false;
        }
    }
}

