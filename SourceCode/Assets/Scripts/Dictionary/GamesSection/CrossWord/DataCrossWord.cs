using System.Collections.Generic;
using CrossWordUtil;

namespace EnglishApp
{
    public class DataCrossWord
    {
        private List<SingleWord> listSingleWord;
        public List<SingleWord> ListSingleWord
        {
            get { return this.listSingleWord; }
            set { this.listSingleWord = value; }
        }


        public List<SingleWord> GetListWords()
        {
            List<SingleWord> auxList = new List<SingleWord>();

            listSingleWord = Utils.Shuffle(listSingleWord);

            // Get range of words
            for (int i = 0; (i < CrossWordGenerator.RANGEWORDS && i < listSingleWord.Count); i++)
            {
                auxList.Add(listSingleWord[i]);
            }
            auxList.Sort(compare);
            return auxList;
        }

        private int compare(SingleWord a, SingleWord b)
        {
            if (a.Answer.Length > b.Answer.Length)
            {
                return -1;
            }
            else if (a.Answer.Length < b.Answer.Length)
            {
                return 1;
            }

            return 0;
        }

        public DataCrossWord()
        {
            listSingleWord = new List<SingleWord>();
        }
    }
}
