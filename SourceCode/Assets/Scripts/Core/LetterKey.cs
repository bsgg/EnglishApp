using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class LetterKey : MonoBehaviour
    {

        public Button ButtonLetter;
        public delegate void DelegateOnLetter(char letter);
        public DelegateOnLetter OnLetter;

        public char Letter;

        public void OnLetterPress()
        {
            if (OnLetter != null)
            {
                OnLetter(Letter);
            }
        }

        public void ActiveButtonLetter(bool enable)
        {
            this.ButtonLetter.enabled = enable;
        }
    }
}
