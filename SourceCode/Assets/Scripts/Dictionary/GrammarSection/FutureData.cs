using System.Collections.Generic;

namespace EnglishApp
{
    public class FutureData
    {
        private List<Grammar> m_FutureGrammar;
        public List<Grammar> FutureGrammar
        {
            get { return m_FutureGrammar; }
            set { m_FutureGrammar = value; }
        }

        public FutureData()
        {
            m_FutureGrammar = new List<Grammar>();
        }
    }
}
