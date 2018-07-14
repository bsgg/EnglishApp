using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

namespace EnglishApp
{
    [Serializable]
    public class IndexFile
    {
        public LauncherController.EDATATYPE DataType;
        public string FileName;
        public string URL;
        public string FolderName;
        public FileData Data;

    }

    [Serializable]
    public class FileData
    {
        public List<IndexFile> Data;
        public FileData()
        {
            Data = new List<IndexFile>();
        }
    }

    
    public class LauncherController : Base
    {
        public enum EDATATYPE { GRAMMAR, VOCABULARY };

        [SerializeField] private string m_FileDataUrl = "http://beatrizcv.com/Data/English/";

        [SerializeField] private List<IndexFile> m_IndexFileList;


        public override IEnumerator DelayedInit()
        {

            for (int i=0; i< m_IndexFileList.Count; i++)
            {
                string localFolder = Path.Combine(Application.persistentDataPath, m_IndexFileList[i].FolderName);

                

                if (!Directory.Exists(localFolder))
                {
                    Directory.CreateDirectory(localFolder);
                }

                m_IndexFileList[i].URL = m_FileDataUrl + m_IndexFileList[i].FileName;

                Debug.Log("<color=red>" + "[LauncherController.Initialize] LocalFolder " + localFolder + " Cloud URL: " + m_IndexFileList[i].URL + "</color>");
            }

            /*m_IndexFileData = new FileData();

            // Create directories
            string localFolder = Application.persistentDataPath;
            m_LocalIndexFileURL = Path.Combine(Application.persistentDataPath, m_IndexFileName);

            string grammarFolder = Path.Combine(Application.persistentDataPath, m_GrammarFolder);
            string vocabularyFolder = Path.Combine(Application.persistentDataPath, m_VocabularyFolder);*/

            yield return new WaitForSeconds(0.1f);
        }

    }
}
