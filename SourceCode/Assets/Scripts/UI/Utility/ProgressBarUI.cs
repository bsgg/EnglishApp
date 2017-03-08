using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


namespace EnglishApp
{
    public class ProgressBarUI : MonoBehaviour
    {
        public Scrollbar ProgressBarObject;

        public Text LabelTime;

        public delegate void OnProgressFinish();
        public OnProgressFinish OnFinish;

        private System.DateTime startDate;
        private System.DateTime finishDate;
        private float seconds;

        public void InitProgressBar(float seconds)
        {
            this.seconds = seconds;
            ProgressBarObject.size = 0.0f;
            if (this.gameObject.activeInHierarchy)
            {
                StartCoroutine(ProgressBarTime());
            }

            LabelTime.text = seconds + " seconds";
        }

        public void Enable()
        {
            if (this.gameObject != null)
            {
                this.gameObject.SetActive(true);
            }
        }

        public void Disable()
        {
            if (this.gameObject != null)
            {
                this.gameObject.SetActive(false);
            }

        }

        public void StopProgressBar()
        {
            StopCoroutine(ProgressBarTime());
            ProgressBarObject.size = 0.0f;
            LabelTime.text = "";
        }

        IEnumerator ProgressBarTime()
        {
            yield return new WaitForSeconds(0.5f);

            startDate = System.DateTime.Now;
            finishDate = startDate.AddSeconds(seconds);
            System.DateTime currentTime;
            TimeSpan difference = finishDate.Subtract(startDate);
            while ((currentTime = System.DateTime.Now) <= finishDate)
            {
                TimeSpan currentDiference = finishDate.Subtract(currentTime);
                float auxDiff = (float)((currentDiference.TotalMilliseconds) / difference.TotalMilliseconds);
                float percent = 1.0f - auxDiff;
                ProgressBarObject.size = percent;

                LabelTime.text = currentDiference.Seconds + " seconds";

                yield return new WaitForSeconds(0.05f);
            }
            ProgressBarObject.size = 1.0f;

            LabelTime.text = "";
            if (OnFinish != null)
            {
                OnFinish();
            }
        }
    }
}
