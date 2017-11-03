using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

namespace EnglishApp
{
    
    public class DataDictionary
    {
        /// <summary>
        /// Section Type
        /// </summary>
        public enum SECTION_VOCABULARY
        {
            NONE = 0,
            WORDS,
            PHRASALVERB,
            AGILITY,
            EXPRESSIONS
        }


        
        [SerializeField]
        private List<WordDictionary> m_VocabularySet;

        /*public DataDictionary()
        {
            LoadVocabulary();
        }*/

        
        /*public CATEGORY GetRandomCategory()
        {
            return ((CATEGORY)Random.Range(0, (int)CATEGORY.NUM));
        }*/

        
        

        /// <summary>
        /// Random word
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /*public Word GetRandomWord(CATEGORY category)
        {
           if (category == CATEGORY.NONE || category == CATEGORY.NUM) return null;
           return  m_VocabularySet[(int)category].GetRandomWord();
        }*/

        /// <summary>
        /// Gets a random word base on a random category
        /// </summary>
        /// <returns></returns>
        /*public Word GetRandomWord(List<CATEGORY> listExcludes = null)
        {

            List<int> lCategories = new List<int>();

            if ((listExcludes != null) && (listExcludes.Count > 0))
            {
                for (int i = 0; i < (int)CATEGORY.NUM; i++)
                {
                    CATEGORY auxCat = (CATEGORY)i;
                    bool include = true;
                    for (int iExclude = 0; iExclude < listExcludes.Count; iExclude++)
                    {
                        if (auxCat == listExcludes[iExclude])
                        {
                            include = false;
                            break;
                        }
                    }
                    if (include)
                    {
                        lCategories.Add(i);
                    }
                }
            }
            else
            {
                for (int i = 0; i < (int)CATEGORY.NUM; i++)
                {
                    //CATEGORY_WORDS auxCat = (CATEGORY_WORDS)i;
                    lCategories.Add(i);
                }
            }


            // random word in the category
            int indexCategory = Random.Range(0, lCategories.Count -1);
            Word randomWord = m_VocabularySet[indexCategory].GetRandomWord();           

            if (randomWord == null)
            {
                Debug.LogFormat("randomWord null", randomWord);
            }

            return randomWord;
        }        

        public void DebugWord(Word word)
        {
            Debug.Log(word.Category + " | Word: " + word.VocabularyWord + " | Pronunciation: " + word.Pronunciation + " | Mean: " + word.Meanings.Count + " | Example: " + word.EnglishExamples.Count);
        }*/
    }
}
