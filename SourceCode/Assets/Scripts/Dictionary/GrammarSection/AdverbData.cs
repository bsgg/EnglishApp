using System.Collections.Generic;

namespace EnglishApp
{
    public class AdverbData
    {
        private List<Grammar> m_AdverbGrammar;
        public List<Grammar> AdverbGrammar
        {
            get { return m_AdverbGrammar; }
            set { m_AdverbGrammar = value; }
        }

        public AdverbData()
        {
            m_AdverbGrammar = new List<Grammar>();
        }
    }
}
