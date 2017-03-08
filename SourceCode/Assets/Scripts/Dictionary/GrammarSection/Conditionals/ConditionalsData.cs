using System.Collections.Generic;

namespace EnglishApp
{
    public class ConditionalData
    {
        private List<Grammar> m_Conditionals;
        public List<Grammar> Conditionals
        {
            get { return m_Conditionals; }
            set { m_Conditionals = value; }
        }

        public ConditionalData()
        {
            m_Conditionals = new List<Grammar>();
        }
    }
}
