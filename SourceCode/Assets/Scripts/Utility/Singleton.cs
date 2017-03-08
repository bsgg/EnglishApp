using UnityEngine;
using System.Collections;

namespace Utility
{
    /// <summary>
    /// Singleton generaic T, where T inherances from Monobehaviour
    /// We can call methods from MonoBehaviour with T, 
    /// If T doesn't inherance from Monobehaviour we can't
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        /**
           Returns the instance of this singleton.
        */
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) +
                           " is needed in the scene, but there is none.");
                    }
                }

                return instance;
            }
        }
    }
}