using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace EnglishApp
{
    public class GamesDictionary
    {
        public GamesDictionary()
        {
        }

        #region QuizGame

        PhrasalVerbQuizData dataPhrasalVerb;
        public PhrasalVerbQuizData DataPhrasalVerb
        {
            get { return this.dataPhrasalVerb; }
            set { this.dataPhrasalVerb = value; }
        }

        GrammarQuizData dataGrammar;
        public GrammarQuizData DataGrammar
        {
            get { return this.dataGrammar; }
            set { this.dataGrammar = value; }
        }

        CommomMistakesData dataCommonMistakes;
        public CommomMistakesData DataCommonMistakes
        {
            get { return this.dataCommonMistakes; }
            set { this.dataCommonMistakes = value; }
        }


        /// <summary>
        /// Loads the data for phrasal verb quiz
        /// </summary>
        public void LoadQuiz()
        {
            if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.PHRASALVERBQUIZ)
            {
                string jsonActionsString = Utils.LoadJSONResource("Data/Games/PhrasalVerbQuizGame");
                if (jsonActionsString != "")
                {
                    dataPhrasalVerb = JsonMapper.ToObject<PhrasalVerbQuizData>(jsonActionsString);

                }
            }
            else if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.GRAMMARQUIZ)
            {
                string jsonActionsString = Utils.LoadJSONResource("Data/Games/GrammarQuizGame");
                if (jsonActionsString != "")
                {
                    dataGrammar = JsonMapper.ToObject<GrammarQuizData>(jsonActionsString);

                }
            }
            else if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.COMMONMISTAKES)
            {
                string jsonActionsString = Utils.LoadJSONResource("Data/Games/CommonMistakesQuiz");
                if (jsonActionsString != "")
                {
                    dataCommonMistakes = JsonMapper.ToObject<CommomMistakesData>(jsonActionsString);

                }
            }
        }
        public SingleQuiz GetQuizQuestion(int index)
        {
            if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.PHRASALVERBQUIZ)
            {
                if ((dataPhrasalVerb.PhrasalVerbQuiz != null) && (index < dataPhrasalVerb.PhrasalVerbQuiz.Count))
                {
                    return dataPhrasalVerb.PhrasalVerbQuiz[index];
                }
            }
            else if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.GRAMMARQUIZ)
            {
                if ((dataGrammar.GrammarQuiz != null) && (index < dataGrammar.GrammarQuiz.Count))
                {
                    return dataGrammar.GrammarQuiz[index];
                }
            }
            return null;
        }


        public int NumberElements()
        {
            if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.PHRASALVERBQUIZ)
            {
                if (dataPhrasalVerb.PhrasalVerbQuiz != null)
                {
                    return dataPhrasalVerb.PhrasalVerbQuiz.Count;
                }
            }
            else if (GameManager.Instance.GamesSectionControl.SectionGame == GamesControl.SECTION_GAME.GRAMMARQUIZ)
            {
                if (dataGrammar.GrammarQuiz != null)
                {
                    return dataGrammar.GrammarQuiz.Count;
                }
            }
            return 0;
        }
        #endregion QuizGame

        #region CrossWordGame

        /// <summary>
        /// Gets the data cross word.
        /// </summary>
        /// <returns>The data cross word.</returns>
        public DataCrossWord GetDataCrossWord()
        {
            string pathCrossWord = "Data/Games/CrossWord_A";
            string jsonActionsString = Utils.LoadJSONResource(pathCrossWord);
            if (jsonActionsString != "")
            {
                return JsonMapper.ToObject<DataCrossWord>(jsonActionsString);
            }

            return null;
        }

        #endregion CrossWordGame

        #region CommonMistakes


        /// <summary>
        /// Loads the data for phrasal verb quiz
        /// </summary>
        public void LoadCommonMistakes()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Games/CommonMistakesQuiz");
            if (jsonActionsString != "")
            {
                dataCommonMistakes = JsonMapper.ToObject<CommomMistakesData>(jsonActionsString);
            }

        }

        public SingleCommonMistake GetCommonMistakeQuestion(int index)
        {
            if ((dataCommonMistakes.CommonMistakesQuiz != null) && (index < dataCommonMistakes.CommonMistakesQuiz.Count))
            {
                return dataCommonMistakes.CommonMistakesQuiz[index];
            }
            return null;
        }


        #endregion CommonMistakes

    }
}
