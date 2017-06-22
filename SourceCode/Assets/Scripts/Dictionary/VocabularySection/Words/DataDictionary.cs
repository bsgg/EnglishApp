using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;

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

        /// <summary>
        /// Category Word type
        /// </summary>
        public enum CATEGORY_WORDS
        {
            NONE = -1,
            ACTIONS = 0,
            ADJECTIVES,
            ADVERBS,
            ANIMALS,
            CONNECTEDWORDS,
            FOOD,
            MISC,
            OBJECTS,
            PLACES,
            BODYPARTS
        }

        private ActionsDictionary m_DataActions;
        private FoodDictionary m_DataFood;
        private AnimalsDictionary m_DataAnimals;
        private ObjectsDictionary m_DataObjects;
        private AdjectivesDictionary m_DataAdjectives;
        private AdverbsDictionary m_DataAdverbs;
        private MiscsDictionary m_DataMisc;
        private PlacesDictionary m_DataPlaces;
        private ConnectedWordsDictionary m_DataConnectedWords;
        private BodyPartsDictionary m_DataBodyParts;
        private PhrasalVerbDictionary m_DataPhrasalVerbs;
        private AgilityDictionary m_DataAgility;
        private ExpressionsDictionary m_DataExpressions;

        private bool m_WordDataLoaded = false;
        private bool m_PhrasalVerbDataLoaded = false;
        private bool m_AgilityDataLoaded = false;
        private bool m_ExpressionDataLoaded = false;

        public DataDictionary()
        {
            m_WordDataLoaded = false;
            m_PhrasalVerbDataLoaded = false;
            m_AgilityDataLoaded = false;
            m_ExpressionDataLoaded = false;
        }

        #region LOAD_DATA_METHODS
        private void LoadWords()
        {
            if (!m_WordDataLoaded)
            {
                m_WordDataLoaded = true;
                string jsonActionsString = Utils.LoadJSONResource("Data/ActionsDictionary");
                if (jsonActionsString != "")
                {
                    m_DataActions = JsonMapper.ToObject<ActionsDictionary>(jsonActionsString);
                }

                string jsonFoodString = Utils.LoadJSONResource("Data/FoodDictionary");
                if (jsonFoodString != "")
                {
                    m_DataFood = JsonMapper.ToObject<FoodDictionary>(jsonFoodString);
                }

                string jsonAnimalsString = Utils.LoadJSONResource("Data/AnimalsDictionary");
                if (jsonAnimalsString != "")
                {
                    m_DataAnimals = JsonMapper.ToObject<AnimalsDictionary>(jsonAnimalsString);
                }

                string jsonObjectsString = Utils.LoadJSONResource("Data/ObjectsDictionary");
                if (jsonObjectsString != "")
                {
                    m_DataObjects = JsonMapper.ToObject<ObjectsDictionary>(jsonObjectsString);
                }

                string jsonAdjectivesString = Utils.LoadJSONResource("Data/AdjectivesDictionary");
                if (jsonAdjectivesString != "")
                {
                    m_DataAdjectives = JsonMapper.ToObject<AdjectivesDictionary>(jsonAdjectivesString);
                }

                string jsonAdverbsString = Utils.LoadJSONResource("Data/AdverbsDictionary");
                if (jsonAdverbsString != "")
                {
                    m_DataAdverbs = JsonMapper.ToObject<AdverbsDictionary>(jsonAdverbsString);
                }

                string jsonMiscsString = Utils.LoadJSONResource("Data/MiscDictionary");
                if (jsonMiscsString != "")
                {
                    m_DataMisc = JsonMapper.ToObject<MiscsDictionary>(jsonMiscsString);
                }

                string jsonPlacesString = Utils.LoadJSONResource("Data/PlacesDictionary");
                if (jsonPlacesString != "")
                {
                    m_DataPlaces = JsonMapper.ToObject<PlacesDictionary>(jsonPlacesString);
                }

                string jsonConnectedWordsString = Utils.LoadJSONResource("Data/ConnectedWordsDictionary");
                if (jsonConnectedWordsString != "")
                {
                    m_DataConnectedWords = JsonMapper.ToObject<ConnectedWordsDictionary>(jsonConnectedWordsString);
                }

                string jsonBodyPartsString = Utils.LoadJSONResource("Data/BodyPartsDictionary");
                if (jsonBodyPartsString != "")
                {
                    m_DataBodyParts = JsonMapper.ToObject<BodyPartsDictionary>(jsonBodyPartsString);
                }
            }
        }
        private void LoadPhrasalVerb()
        {
            if (!m_PhrasalVerbDataLoaded)
            {
                m_PhrasalVerbDataLoaded = true;
                string jsonPVString = Utils.LoadJSONResource("Data/PhrasalVerbDictionary");
                if (jsonPVString != "")
                {
                    m_DataPhrasalVerbs = JsonMapper.ToObject<PhrasalVerbDictionary>(jsonPVString);
                }
            }
        }
        private void LoadAgility()
        {
            if (!m_AgilityDataLoaded)
            {
                m_AgilityDataLoaded = true;
                string jsonAgilString = Utils.LoadJSONResource("Data/AgilityDictionary");
                if (jsonAgilString != "")
                {
                    m_DataAgility = JsonMapper.ToObject<AgilityDictionary>(jsonAgilString);
                }
            }
        }
        private void LoadExpressions()
        {
            if (!m_ExpressionDataLoaded)
            {
                m_ExpressionDataLoaded = true;
                string jsonExpressionsString = Utils.LoadJSONResource("Data/ExpressionsDictionary");
                if (jsonExpressionsString != "")
                {
                    m_DataExpressions = JsonMapper.ToObject<ExpressionsDictionary>(jsonExpressionsString);
                }
            }
        }

        /// <summary>
        /// Load data from a section
        /// </summary>
        /// <param name="section"></param>
        public void LoadSection(SECTION_VOCABULARY section)
        {
            switch (section)
            {
                case SECTION_VOCABULARY.WORDS:
                    LoadWords();
                    break;
                case SECTION_VOCABULARY.PHRASALVERB:
                    LoadPhrasalVerb();
                    break;
                case SECTION_VOCABULARY.AGILITY:
                    LoadAgility();
                    break;
                case SECTION_VOCABULARY.EXPRESSIONS:
                    LoadExpressions();
                    break;
            }
        }
        #endregion LOAD_DATA_METHODS

        /// <summary>
        /// Random word from a section
        /// </summary>
        /// <param name="sectionVocabulary"></param>
        /// <returns></returns>
        public Word GetRandomWord(SECTION_VOCABULARY sectionVocabulary)
        {
            Word auxWord = null;
            switch (sectionVocabulary)
            {

                case SECTION_VOCABULARY.WORDS:
                    auxWord = new Word();
                    auxWord = GetRandomWord();
                    break;

                case SECTION_VOCABULARY.PHRASALVERB:
                    int indexPV = Random.Range(0, m_DataPhrasalVerbs.PhrasalVerbs.Count);
                    Vocabulary auxPhrasal = m_DataPhrasalVerbs.PhrasalVerbs[indexPV];

                    auxWord = new Word();
                    auxWord.VocabularyWord = auxPhrasal.VocabularyWord;
                    auxWord.Pronunciation = "";
                    auxWord.Category = "";
                    auxWord.EnglishExamples = auxPhrasal.EnglishExamples;
                    auxWord.SpanishExamples = auxPhrasal.SpanishExamples;
                    auxWord.Meanings = auxPhrasal.Meanings;

                    break;


                case SECTION_VOCABULARY.AGILITY:
                    int indexAgility = Random.Range(0, m_DataAgility.AgilityExpressions.Count);
                    Vocabulary auxAgility = m_DataAgility.AgilityExpressions[indexAgility];
                    auxWord = new Word();
                    auxWord.VocabularyWord = auxAgility.VocabularyWord;
                    auxWord.Pronunciation = "";
                    auxWord.Category = "";
                    auxWord.EnglishExamples = auxAgility.EnglishExamples;
                    auxWord.SpanishExamples = auxAgility.SpanishExamples;
                    auxWord.Meanings = auxAgility.Meanings;

                    break;

                case SECTION_VOCABULARY.EXPRESSIONS:
                    int indexExpression = Random.Range(0, m_DataExpressions.Expressions.Count);
                    Vocabulary auxExpression = m_DataExpressions.Expressions[indexExpression];
                    auxWord = new Word();
                    auxWord.VocabularyWord = auxExpression.VocabularyWord;
                    auxWord.Pronunciation = "";
                    auxWord.Category = "";
                    auxWord.EnglishExamples = auxExpression.EnglishExamples;
                    auxWord.SpanishExamples = auxExpression.SpanishExamples;
                    auxWord.Meanings = auxExpression.Meanings;

                    break;
            }
            return auxWord;
        }

        /// <summary>
        /// Gets a random word base on a random category
        /// </summary>
        /// <returns></returns>
        public Word GetRandomWord(List<CATEGORY_WORDS> listExcludes = null)
        {
            List<int> lCategories = new List<int>();
            int nCategories = System.Enum.GetNames(typeof(CATEGORY_WORDS)).Length;
            if (listExcludes != null)
            {
                for (int i = 0; i < nCategories; i++)
                {
                    CATEGORY_WORDS auxCat = (CATEGORY_WORDS)i;
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
                for (int i = 0; i < nCategories; i++)
                {
                    //CATEGORY_WORDS auxCat = (CATEGORY_WORDS)i;
                    lCategories.Add(i);
                }
            }


            // Choose a category
            int indexCategory = Random.Range(0, lCategories.Count -1);
            CATEGORY_WORDS category = (CATEGORY_WORDS)indexCategory;

            int indexWord = 0;
            Word randomWord = null;
            switch (category)
            {
                case CATEGORY_WORDS.ACTIONS:
                    indexWord = Random.Range(0, m_DataActions.Actions.Count);
                    randomWord = m_DataActions.Actions[indexWord];
                    break;
                case CATEGORY_WORDS.ADJECTIVES:
                    indexWord = Random.Range(0, m_DataAdjectives.Adjectives.Count);
                    randomWord = m_DataAdjectives.Adjectives[indexWord];
                    break;
                case CATEGORY_WORDS.ADVERBS:
                    indexWord = Random.Range(0, m_DataAdverbs.Adverbs.Count);
                    randomWord = m_DataAdverbs.Adverbs[indexWord];
                    break;
                case CATEGORY_WORDS.ANIMALS:
                    indexWord = Random.Range(0, m_DataAnimals.Animals.Count);
                    randomWord = m_DataAnimals.Animals[indexWord];
                    break;
                case CATEGORY_WORDS.CONNECTEDWORDS:
                    indexWord = Random.Range(0, m_DataConnectedWords.ConnectedWords.Count);
                    randomWord = m_DataConnectedWords.ConnectedWords[indexWord];
                    break;
                case CATEGORY_WORDS.FOOD:
                    indexWord = Random.Range(0, m_DataFood.Food.Count);
                    randomWord = m_DataFood.Food[indexWord];
                    break;
                case CATEGORY_WORDS.MISC:
                    indexWord = Random.Range(0, m_DataMisc.Miscs.Count);
                    randomWord = m_DataMisc.Miscs[indexWord];
                    break;
                case CATEGORY_WORDS.OBJECTS:
                    indexWord = Random.Range(0, m_DataObjects.Objects.Count);
                    randomWord = m_DataObjects.Objects[indexWord];
                    break;
                case CATEGORY_WORDS.PLACES:
                    indexWord = Random.Range(0, m_DataPlaces.Places.Count);
                    randomWord = m_DataPlaces.Places[indexWord];
                    break;
                case CATEGORY_WORDS.BODYPARTS:
                    indexWord = Random.Range(0, m_DataBodyParts.BodyParts.Count);
                    randomWord = m_DataBodyParts.BodyParts[indexWord];
                    break;
            }

            if (randomWord == null)
            {
                Debug.LogFormat("randomWord null", randomWord);
            }

            return randomWord;
        }

        public string GetNameCategory(CATEGORY_WORDS category)
        {
            string nameCat = "";
            switch (category)
            {
                case CATEGORY_WORDS.ACTIONS:
                    nameCat = "Actions";
                    break;
                case CATEGORY_WORDS.ADJECTIVES:
                    nameCat = "Adjectives";
                    break;
                case CATEGORY_WORDS.ADVERBS:
                    nameCat = "Adverbs";
                    break;
                case CATEGORY_WORDS.ANIMALS:
                    nameCat = "Animals";
                    break;
                case CATEGORY_WORDS.CONNECTEDWORDS:
                    nameCat = "Link Words";
                    break;
                case CATEGORY_WORDS.FOOD:
                    nameCat = "Food";
                    break;
                case CATEGORY_WORDS.MISC:
                    nameCat = "Misc";
                    break;
                case CATEGORY_WORDS.OBJECTS:
                    nameCat = "Objects";
                    break;
                case CATEGORY_WORDS.PLACES:
                    nameCat = "Places";
                    break;
                case CATEGORY_WORDS.BODYPARTS:
                    nameCat = "Body Parts";
                    break;
            }
            return nameCat;
        }

        public void DebugWord(Word word)
        {
            Debug.Log(word.Category + " | Word: " + word.VocabularyWord + " | Pronunciation: " + word.Pronunciation + " | Mean: " + word.Meanings.Count + " | Example: " + word.EnglishExamples.Count);
        }
    }
}
