using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{

   /* [Serializable]
    public class IndexFile
    {
        public string FileName;
        public string URL;
        //public string Data;
        public FileRequestManager.EDATATYPE DataType;
    }

    [Serializable]
    public class FileData
    {
        public List<IndexFile> Data;
        public FileData()
        {
            Data = new List<IndexFile>();
        }
    }*/

    public class FileRequestManager : MonoBehaviour
    {
        //public enum EDATATYPE { GRAMMAR, VOCABULARY };

        #region Instance
        private static FileRequestManager m_Instance;
        public static FileRequestManager Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = (FileRequestManager)FindObjectOfType(typeof(FileRequestManager));

                    if (m_Instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(FileRequestManager) + " is needed in the scene, but there is none.");
                    }
                }
                return m_Instance;
            }
        }
        #endregion Instance

       

       /* [SerializeField]
        public List<IndexFile> m_IndexFileDataURL;

        [SerializeField]
        public List<FileData> m_Data;

        //[SerializeField]
        private FileData m_FileData;
        public FileData FileData
        {
            get
            {
                return m_FileData;
            }
        }*/

        /*private float m_PercentProgress;
        private string m_ProgressText;

        public string ProgressText
        {
            get { return m_ProgressText; }
        }*/


  
        //private string m_FileDataUrl = "http://beatrizcv.com/Data/";


        public IEnumerator RequestIndexFiles()
        {
            /* m_Data = new List<FileData>();
             for (int i=0; i< m_IndexFileDataURL.Count; i++)
             {
                 WWW wwwFile = new WWW(m_IndexFileDataURL[i].URL);
                 yield return wwwFile;
                 string jsonData = wwwFile.text;

                 Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Retrieving file " + m_IndexFileDataURL[i] + " Data: " + jsonData + "</color>");

                 try
                 {
                     m_Data.Add(JsonMapper.ToObject<FileData>(jsonData));

                 }
                 catch (Exception e)
                 {
                     Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Unable to parse " + m_IndexFileDataURL[i] + "</color>");
                 }

                 yield return null;

             }*/

            yield return null;

        }

        /*public IEnumerator RequestFiles()
        {
            m_FileData = new FileData();
            m_PercentProgress = 0.0f;
            m_ProgressText = m_PercentProgress.ToString() + " % ";

            if (string.IsNullOrEmpty(m_FileDataUrl))
            {
                Debug.LogWarning("<color=yellow>" + "[FileRequestManager] File Data Url is null or empty" + "</color>");
                yield return null;
            }

            WWW wwwFile = new WWW(m_FileDataUrl);
            yield return wwwFile;
            string jsonData = wwwFile.text;

            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    m_FileData = JsonMapper.ToObject<FileData>(jsonData);

                }
                catch (Exception e)
                {
                    Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Unable to parse " + m_FileDataUrl + " Data: " + jsonData  + "</color>");
                }

                yield return null;


                Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Requesting... " + m_FileData.Data.Count + " Files " + "</color>");
                for (int i = 0; i < m_FileData.Data.Count; i++)
                {
                    if (string.IsNullOrEmpty(m_FileData.Data[i].URL))
                    {
                        continue;
                    }

                    // Request
                    Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Requesting: " + (i + 1) + "/" + m_FileData.Data.Count + " : " + m_FileData.Data[i].FileName + "</color>");
                 
                    WWW www = new WWW(m_FileData.Data[i].URL);
                    while (!www.isDone)
                    {
                        m_PercentProgress = www.progress * 100.0f;
                        m_ProgressText = m_PercentProgress.ToString() + " % ";
                        yield return null;
                    }

                    m_PercentProgress = www.progress * 100.0f;
                    m_ProgressText = m_PercentProgress.ToString() + " % ";

                    //m_FileData.Data[i].Data = www.text;


                    Debug.LogWarning("<color=yellow>" + "[FileRequestManager] Data Received "+ "</color>");
                }
            }
            else
            {
                Debug.LogWarning("<color=yellow>" + "[FileRequestManager] File Data " + m_FileDataUrl + " Json is null or empty" + "</color>");
            }
        }*/

    }
}
