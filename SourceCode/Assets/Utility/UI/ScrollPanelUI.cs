using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Utility.UI
{
    public class ScrollPanelUI : MonoBehaviour
    {
        public Action<ButtonWithText> OnButtonPress;

        [Header("Prefab Item")]
        [SerializeField]
        private GameObject m_ItemMenuPrefab;

        [Header("Prefab Item")]
        [SerializeField]
        private RectTransform m_ContentRecTransform;

        [Header("Grid content layout")]
        [SerializeField]
        private GridLayoutGroup m_GridContent;

        /// <summary>
        /// List of objects in content panel
        /// </summary>
        private List<GameObject> m_ListElements;
        /// <summary>
        /// Method to init menu
        /// </summary>
        /// <param name="data">Data to fill the scroll</param>
        public void InitScroll(List<string> data)
        {
            if (m_ListElements != null)
            {
                for (int i = 0; i < m_ListElements.Count; i++)
                {
                    Destroy(m_ListElements[i]);
                }
            }

            m_ListElements = new List<GameObject>();

            for (int i = 0; i < data.Count; i++)
            {
                GameObject element = Instantiate(m_ItemMenuPrefab) as GameObject;
                m_ListElements.Add(element);
                element.transform.SetParent(m_ContentRecTransform.transform);

                RectTransform cellRectTransform = element.GetComponent<RectTransform>();
                if (cellRectTransform != null)
                {
                    cellRectTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }

                ButtonWithText Button = element.GetComponent<ButtonWithText>();
                Button.ButtonIndex = i;
                Button.Title = data[i];
                Button.OnButtonClicked += OnScrollButonPress;
            }
        }

        public void OnScrollButonPress(ButtonWithText a_button)
        {
            if (OnButtonPress != null)
            {
                OnButtonPress(a_button);
            }
        }

    }
}
