using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class SplashScreen : MonoBehaviour
    {        
        [SerializeField] private CanvasGroup m_SplashScreenPanel;

        [Header("SplashScreen Settings")]
        [SerializeField] private float m_AlphaSpeed = 1.0f;
        [SerializeField] private float m_SteadyTime = 0.5f;

        [Header("Load Scene")]
        [SerializeField] private float m_TimeToWait = 0.5f;
        [SerializeField] private string m_LoadScene = "Main";

        void Start()
        { 
            StartCoroutine(DoUpdateSplashScreen());
        }

        private IEnumerator DoUpdateSplashScreen()
        {
            float alpha = 0.0f;
            while (alpha < 1.0f)
            {
                alpha += (m_AlphaSpeed * Time.deltaTime);
                m_SplashScreenPanel.alpha = alpha;
                yield return new WaitForEndOfFrame();
            }
            alpha = 1.0f;
            m_SplashScreenPanel.alpha = alpha;
            yield return new WaitForSeconds(m_SteadyTime);


            while (alpha > 0.0f)
            {
                alpha -= (m_AlphaSpeed * Time.deltaTime);
                m_SplashScreenPanel.alpha = alpha;
                yield return new WaitForEndOfFrame();
            }
            alpha = 0.0f;
            m_SplashScreenPanel.alpha = alpha;
            m_SplashScreenPanel.gameObject.SetActive(false);
            yield return new WaitForSeconds(m_TimeToWait);

            if (!string.IsNullOrEmpty(m_LoadScene))
            {
                // Load Scene
                SceneManager.LoadScene(m_LoadScene);
            }
        }
    }
}
