using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

namespace EnglishApp
{
    public class GrammarDictionary
    {
        /// <summary>
        /// Type of category
        /// </summary>
        public enum SECTION_GRAMMAR
        {
            CONDITIONALS = 0,
            FUTURE,
            ADVERBS,
            MISC,
            QUESTIONS,
            MODALS,
            PREPOSITIONS,
            SPEAKING_GRAMMAR,
            COMPARATIVES,
            PASSIVE
        }

        ConditionalData dataConditionals;
        public ConditionalData DataConditionals
        {
            get { return this.dataConditionals; }
            set { this.dataConditionals = value; }
        }

        FutureData dataFuture;
        public FutureData DataFuture
        {
            get { return this.dataFuture; }
            set { this.dataFuture = value; }
        }

        AdverbData dataAdverbs;
        public AdverbData DataAdverbs
        {
            get { return this.dataAdverbs; }
            set { this.dataAdverbs = value; }
        }

        MiscData dataMisc;
        public MiscData DataMisc
        {
            get { return this.dataMisc; }
            set { this.dataMisc = value; }
        }

        QuestionsData dataQuestion;
        public QuestionsData DataQuestion
        {
            get { return this.dataQuestion; }
            set { this.dataQuestion = value; }
        }

        ModalsData dataModals;
        public ModalsData DataModals
        {
            get { return this.dataModals; }
            set { this.dataModals = value; }
        }

        PrepositionsData dataPrepositions;
        public PrepositionsData DataPrepositions
        {
            get { return this.dataPrepositions; }
            set { this.dataPrepositions = value; }
        }

        SpeakingGrammarData dataSpeakingGrammar;
        public SpeakingGrammarData DataSpeakingGrammar
        {
            get { return this.dataSpeakingGrammar; }
            set { this.dataSpeakingGrammar = value; }
        }

        ComparativeGrammarData dataComparativeGrammar;
        public ComparativeGrammarData DataComparativeGrammar
        {
            get { return this.dataComparativeGrammar; }
            set { this.dataComparativeGrammar = value; }
        }

        private PassiveData m_DataPassiveGrammar;
        public PassiveData DataPassiveGrammar
        {
            get { return m_DataPassiveGrammar; }
            set { m_DataPassiveGrammar = value; }
        }

        public GrammarDictionary() { }

        #region LoadGrammarMethods
        private void loadConditionals()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Conditionals/Conditionals");
            if (jsonActionsString != "")
            {
                dataConditionals = JsonMapper.ToObject<ConditionalData>(jsonActionsString);
            }
        }

        private void loadFuture()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Future/FutureGrammar");
            if (jsonActionsString != "")
            {
                dataFuture = JsonMapper.ToObject<FutureData>(jsonActionsString);
            }
        }

        private void loadAdverbs()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Adverbs/Adverbs");
            if (jsonActionsString != "")
            {
                dataAdverbs = JsonMapper.ToObject<AdverbData>(jsonActionsString);
            }
        }

        private void loadMisc()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Misc/Misc");
            if (jsonActionsString != "")
            {
                dataMisc = JsonMapper.ToObject<MiscData>(jsonActionsString);
            }
        }

        private void loadQuestions()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Questions");
            if (jsonActionsString != "")
            {
                dataQuestion = JsonMapper.ToObject<QuestionsData>(jsonActionsString);
            }
        }

        private void loadModals()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Modals/Modals");
            if (jsonActionsString != "")
            {
                dataModals = JsonMapper.ToObject<ModalsData>(jsonActionsString);
            }
        }

        private void loadPrepositions()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Prepositions/Prepositions");
            if (jsonActionsString != "")
            {
                dataPrepositions = JsonMapper.ToObject<PrepositionsData>(jsonActionsString);
            }
        }

        private void loadSpeakingGrammar()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Speaking/Speaking");
            if (jsonActionsString != "")
            {
                dataSpeakingGrammar = JsonMapper.ToObject<SpeakingGrammarData>(jsonActionsString);
            }
        }

        private void loadComparativesGrammar()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Comparatives/Comparatives");
            if (jsonActionsString != "")
            {
                dataComparativeGrammar = JsonMapper.ToObject<ComparativeGrammarData>(jsonActionsString);
            }
        }

        private void LoadPassiveGrammar()
        {
            string jsonActionsString = Utils.LoadJSONResource("Data/Grammar/Passive/Passive");
            if (jsonActionsString != "")
            {
                m_DataPassiveGrammar = JsonMapper.ToObject<PassiveData>(jsonActionsString);
            }
        }

        #endregion LoadGrammarMethods

        public string GetSectionGrammarTitle(SECTION_GRAMMAR section)
        {
            string title = "";
            switch (section)
            {
                case SECTION_GRAMMAR.CONDITIONALS:
                    title = "CONDITIONALS";
                    break;
                case SECTION_GRAMMAR.FUTURE:
                    title = "FUTURE";
                    break;
                case SECTION_GRAMMAR.ADVERBS:
                    title = "ADVERBS";
                    break;
                case SECTION_GRAMMAR.MISC:
                    title = "MISC";
                    break;
                case SECTION_GRAMMAR.QUESTIONS:
                    title = "QUESTIONS";
                    break;
                case SECTION_GRAMMAR.MODALS:
                    title = "MODALS";
                    break;
                case SECTION_GRAMMAR.PREPOSITIONS:
                    title = "PREPOSITIONS";
                    break;
                case SECTION_GRAMMAR.SPEAKING_GRAMMAR:
                    title = "SPEAKING GRAMMAR";
                    break;
                case SECTION_GRAMMAR.COMPARATIVES:
                    title = "COMPARATIVES";
                    break;
                case SECTION_GRAMMAR.PASSIVE:
                    title = "PASSIVE VOICE";
                    break;
            }
            return title;
        }

        /// <summary>
        /// Loads a Grammar secction
        /// </summary>
        /// <returns>The section.</returns>
        /// <param name="section">Section.</param>
        public List<Grammar> LoadSection(SECTION_GRAMMAR section)
        {
            if (section == SECTION_GRAMMAR.CONDITIONALS)
            {
                loadConditionals();
                return this.dataConditionals.Conditionals;
            }

            if (section == SECTION_GRAMMAR.FUTURE)
            {
                loadFuture();
                return this.dataFuture.FutureGrammar;
            }

            if (section == SECTION_GRAMMAR.ADVERBS)
            {
                loadAdverbs();
                return this.dataAdverbs.AdverbGrammar;
            }

            if (section == SECTION_GRAMMAR.MISC)
            {
                loadMisc();
                return this.dataMisc.MiscGrammar;
            }

            if (section == SECTION_GRAMMAR.QUESTIONS)
            {
                loadQuestions();
                return this.dataQuestion.QuestionsGrammar;
            }

            if (section == SECTION_GRAMMAR.MODALS)
            {
                loadModals();
                return this.dataModals.ModalsGrammar;
            }
            if (section == SECTION_GRAMMAR.PREPOSITIONS)
            {
                loadPrepositions();
                return this.dataPrepositions.PrepositionsGrammar;
            }

            if (section == SECTION_GRAMMAR.SPEAKING_GRAMMAR)
            {
                loadSpeakingGrammar();
                return this.dataSpeakingGrammar.SpeakingGrammar;
            }

            if (section == SECTION_GRAMMAR.COMPARATIVES)
            {
                loadComparativesGrammar();
                return this.dataComparativeGrammar.ComparativesGrammar;
            }

            if (section == SECTION_GRAMMAR.PASSIVE)
            {
                LoadPassiveGrammar();
                return m_DataPassiveGrammar.PassiveGrammar;
            }

            return null;
        }
    }
}
