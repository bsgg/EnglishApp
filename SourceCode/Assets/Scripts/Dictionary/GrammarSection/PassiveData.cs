using System.Collections.Generic;

namespace EnglishApp
{
    public class PassiveData
    {
        private List<Grammar> m_PassiveGrammar;
        public List<Grammar> PassiveGrammar
        {
            get { return m_PassiveGrammar; }
            set { m_PassiveGrammar = value; }
        }

        public PassiveData()
        {
            m_PassiveGrammar = new List<Grammar>();
        }
    }
}
