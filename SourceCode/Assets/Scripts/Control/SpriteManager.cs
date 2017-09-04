using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishApp
{
    public class SpriteManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> m_SpriteList;


        public Sprite GetSpriteByName(string name)
        {
            for (int i= 0; i<m_SpriteList.Count; i++)
            {
                Debug.Log("Sprite: " + m_SpriteList[i].name);
                if (m_SpriteList[i].name.Equals(name))
                {
                    return m_SpriteList[i];
                }
            }

            return null;

        }
	   
		
	}
}
