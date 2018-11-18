using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace EnglishApp
{
    public class AppController :MonoBehaviour
    {
        #region Instance
        private static AppController m_Instance;
        public static AppController Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (AppController)FindObjectOfType(typeof(AppController));

                    if (m_Instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(AppController) + " is needed in the scene, but there is none.");
                    }
                }
                return m_Instance;
            }
        }
        #endregion Instance


        //public enum ESECTION { NONE = -1, Vocabulary = 0, PhrasalVerbs, Agility, Expressions, Grammar, Games, NUM };
        private LauncherController.EDATATYPE m_SelectedSection = LauncherController.EDATATYPE.NONE;
        public LauncherController.EDATATYPE SelectedSection
        {
            get { return m_SelectedSection; }
        }


        [SerializeField] private LauncherController m_LauncherControl;
        public LauncherController LauncherControl
        {
            get { return m_LauncherControl; }
        }

        [SerializeField] private VocabularyControl m_VocabularyControl;

        [SerializeField] private GrammarControl m_GrammarControlControl;

        private Base m_CurrentControl = null;

        [SerializeField]
        private UIBase m_MainMenu;

        private void Start()
        {

#if !UNITY_EDITOR && UNITY_ANDROID
		EasyTTSUtil.Initialize();
#endif
            StartCoroutine(Init());

        }

        private IEnumerator Init()
        {
            yield return m_LauncherControl.DelayedInit();

            m_LauncherControl.OnDownloadCompleted += LauncherControl_OnDownloadCompleted;

            m_MainMenu.Show();

            if (!m_LauncherControl.IsInitialized)
            {
                Debug.Log("<color=purple>" + "[AppController.Init] Launcher not INITIALIZED, Ready to download " + "</color>");

                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    //m_PopupWithButtons.ShowPopup("No Internet", "Please connect to internet to download data and restart the app");
                }
                else
                {
                    StartCoroutine(m_LauncherControl.DownloadData());
                }

            }
            else
            {
                Debug.Log("<color=purple>" + "[AppController.Init] Launcher INITIALIZED " + "</color>");
            }
        }

        private void LauncherControl_OnDownloadCompleted(LauncherController.ERESULT Result, string Message)
        {
            Debug.Log("<color=purple>" + "[AppController.LauncherControl_OnDownloadCompleted] On Download Completed" + "</color>");
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OnBack();
            }
        }

        public void OnBack()
        {
            switch (m_SelectedSection)
            {
                case LauncherController.EDATATYPE.VOCABULARY:
                case LauncherController.EDATATYPE.PHRASAL_VERBS:
                case LauncherController.EDATATYPE.EXPRESSIONS:
                case LauncherController.EDATATYPE.IDIOMS:
                    m_VocabularyControl.Hide();
                    m_MainMenu.Show();
                break;
            }
        }

        public void OnMainMenuPress(int id)
        {
            m_SelectedSection = (LauncherController.EDATATYPE)id;
            switch (m_SelectedSection)
            {                
                case LauncherController.EDATATYPE.VOCABULARY:
                case LauncherController.EDATATYPE.PHRASAL_VERBS:
                case LauncherController.EDATATYPE.EXPRESSIONS:
                case LauncherController.EDATATYPE.IDIOMS:
                    // Vocabulary
                    m_VocabularyControl.SetRandom();
                    m_VocabularyControl.Show();
                    m_MainMenu.Hide();

                break;

            }

        }
    }
}
