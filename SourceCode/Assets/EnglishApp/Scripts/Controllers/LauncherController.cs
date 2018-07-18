using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;
using Utility.UI;

namespace EnglishApp
{

    #region INDEX_DATA_FILES
    [Serializable]
    public class IndexFile
    {
        public LauncherController.EDATATYPE DataType;
        public string FileName;
        
    }

    [Serializable]
    public class InfoFile: IndexFile
    {
        public string DataFolderName;
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

    #endregion INDEX_DATA_FILES

    public class LauncherController : Base
    {
        public delegate void LauncherAction(ERESULT Result, string Message);
        public event LauncherAction OnDownloadCompleted;

        public enum EDATATYPE { GRAMMAR, VOCABULARY };
        public enum ERESULT { SUCCESS, FAIL };

        [SerializeField] private string m_CloudDataUrl = "http://beatrizcv.com/Data/English/";

        [SerializeField] private List<InfoFile> m_InfoFileList;

        [SerializeField] private ProgressUI m_Progress;

        private bool m_Initialized = false;
        public bool IsInitialized
        {
            get { return m_Initialized; }
        }

        [Header("Data")]
        [SerializeField] private List<WordDictionary> m_VocabularySet;
        public List<WordDictionary> VocabularySet
        {
            get { return m_VocabularySet; }
        }
        [SerializeField] private List<GrammarDictionary> m_DictionarySet;
        public List<GrammarDictionary> DictionarySet
        {
            get { return m_DictionarySet; }
        }

        public override void Show()
        {
            m_Progress.Hide();
            base.Show();
        }

        public override void Hide()
        {
            m_Progress.Hide();
            base.Hide();
        }


        public override IEnumerator DelayedInit()
        {
            m_VocabularySet = new List<WordDictionary>();
            m_DictionarySet = new List<GrammarDictionary>();

            m_Progress.Show();
            m_Progress.SetProgress("Wait..");

            m_Initialized = true;
            int nFilesInLocal = 0;
           
            for (int i=0; i< m_InfoFileList.Count; i++)
            {
                string localDataFolder = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].DataFolderName);           

                if (!Directory.Exists(localDataFolder))
                {
                    Directory.CreateDirectory(localDataFolder);
                }

                string indexLocalFileURL = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].FileName);            

                Debug.Log("<color=purple>" + "[LauncherController.Initialize]  localDataFolder: " + localDataFolder + " - indexLocalFileURL: " + indexLocalFileURL +  "</color>");

                if (File.Exists(indexLocalFileURL))
                {
                    nFilesInLocal++;
                }
            }

            yield return new WaitForEndOfFrame();
            // Load files
            if (nFilesInLocal == m_InfoFileList.Count)
            {
                if (!LoadLocalData())
                {
                    m_Initialized = false;
                }
            }else
            {
                m_Initialized = false;
            }

            yield return new WaitForEndOfFrame();             

            m_Progress.Hide();

        }       

        private bool LoadLocalData()
        {
            m_Progress.Show();
            m_Progress.SetProgress("Wait..");

            for (int i = 0; i < m_InfoFileList.Count; i++)
            {
                string data = string.Empty;

                string indexLocalFileURL = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].FileName);
                if (ReadFile(indexLocalFileURL, ref data))
                {
                    try
                    {
                        m_InfoFileList[i].Data = JsonUtility.FromJson<FileData>(data);

                        // Read sub data 
                        for (int iData = 0; iData < m_InfoFileList[i].Data.Data.Count; iData++)
                        {
                            string localDataFile = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].DataFolderName);
                            localDataFile = Path.Combine(localDataFile, m_InfoFileList[i].Data.Data[iData].FileName + ".json");
                            string jsonData = string.Empty;
                            if (ReadFile(localDataFile, ref jsonData))
                            {
                                
                                if (m_InfoFileList[i].DataType == EDATATYPE.VOCABULARY)
                                {
                                    WordDictionary set = JsonUtility.FromJson<WordDictionary>(jsonData);
                                    m_VocabularySet.Add(set);
                                }

                                else if (m_InfoFileList[i].DataType == EDATATYPE.GRAMMAR)
                                {
                                    GrammarDictionary set = JsonUtility.FromJson<GrammarDictionary>(jsonData);
                                    m_DictionarySet.Add(set);
                                }
                                
                            }
                            else
                            {
                                return false;
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] Unable to Load Data: " + e.Message + "</color>");
                        return false;
                    }

                }else
                {
                    return false;
                }
            }

            return true;           
        }
        
        public IEnumerator DownloadData()
        {
            m_Progress.Show();
            m_Progress.SetProgress("Wait..");

            m_VocabularySet = new List<WordDictionary>();
            m_DictionarySet = new List<GrammarDictionary>();

            for (int i = 0; i < m_InfoFileList.Count; i++)
            {
                // Index cloud url
                string indexCloudURL = m_CloudDataUrl + "/" + m_InfoFileList[i].FileName;
                WWW wwwFile = new WWW(indexCloudURL);

                yield return wwwFile;

                string data = wwwFile.text;

                if (!string.IsNullOrEmpty(data))
                {
                    try
                    {
                        m_InfoFileList[i].Data = JsonUtility.FromJson<FileData>(data);                        

                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] There was an error parsing json: " + data + "  - ERROR: " + e.Message + "</color>");

                        if (OnDownloadCompleted != null)
                        {
                            OnDownloadCompleted(ERESULT.FAIL, "ERROR PARSING JSON");
                        }
                    }

                    yield return new WaitForEndOfFrame();

                    string indexLocalFileURL = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].FileName);
                    bool result = SaveFileToLocal(indexLocalFileURL, wwwFile);

                    if (!result)
                    {
                        if (OnDownloadCompleted != null)
                        {
                            OnDownloadCompleted(ERESULT.FAIL, "ERROR PARSING JSON");
                        }

                        yield break;
                    }

                    for (int iData = 0; iData < m_InfoFileList[i].Data.Data.Count; iData++)
                    {
                        string fileName = m_InfoFileList[i].Data.Data[iData].FileName + ".json";
                        string dataCloudURL = m_CloudDataUrl + "/" + m_InfoFileList[i].DataFolderName + "/" + fileName;
                        Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] Retrieving (" + iData + "/" + m_InfoFileList[i].Data.Data.Count + ") - URL: " + dataCloudURL + "</color>");
                        WWW wwwDataFile = new WWW(dataCloudURL);

                        yield return wwwDataFile;

                        if (!string.IsNullOrEmpty(wwwDataFile.text))
                        {
                            string dataLocalURL = Path.Combine(Application.persistentDataPath, m_InfoFileList[i].DataFolderName);
                            dataLocalURL = Path.Combine(dataLocalURL, fileName);


                            if (SaveFileToLocal(dataLocalURL, wwwDataFile))
                            {
                                if (m_InfoFileList[i].DataType == EDATATYPE.VOCABULARY)
                                {
                                    WordDictionary set = JsonUtility.FromJson<WordDictionary>(wwwDataFile.text);
                                    m_VocabularySet.Add(set);
                                }

                                else if (m_InfoFileList[i].DataType == EDATATYPE.GRAMMAR)
                                {
                                    GrammarDictionary set = JsonUtility.FromJson<GrammarDictionary>(wwwDataFile.text);
                                    m_DictionarySet.Add(set);
                                }
                            }else
                            {
                                Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] Fail Saving " + fileName + " at " + dataLocalURL + "</color>");
                                if (OnDownloadCompleted != null)
                                {
                                    OnDownloadCompleted(ERESULT.FAIL, "UNABLE TO SAVE FILE");
                                }
                            }

                            // TODO: DOWNLOAD IMAGES
                            
                        }
                        else
                        {
                            if (OnDownloadCompleted != null)
                            {
                                OnDownloadCompleted(ERESULT.FAIL, "Empty Data");
                            }
                        }
                    }
                }else
                {
                    if (OnDownloadCompleted != null)
                    {
                        OnDownloadCompleted(ERESULT.FAIL, "Empty Data");
                    }
                }
            }

            m_Progress.Hide();

            if (OnDownloadCompleted != null)
            {
                OnDownloadCompleted(ERESULT.SUCCESS, "");
            }

        }

        private bool ReadFile(string url, ref string data)
        {
            data = string.Empty;

            if (!File.Exists(url)) return false;

            try
            {
                StreamReader reader = new StreamReader(url);

                data = reader.ReadToEnd();

                reader.Close();
            }
            catch (Exception e)
            {
                Debug.Log("<color=purple>" + "[LauncherControl.ReadFile] There was an error trying to read file " + url + " ERROR: " + e.Message + "</color>");
                return false;
            }

            return true;
        }

        private bool SaveFileToLocal(string url, WWW request)
        {
            try
            {
                if (File.Exists(url))// File exists
                {
                    File.Delete(url);
                }

                // Save file
                byte[] bytes = request.bytes;
                File.WriteAllBytes(url, bytes);
            }
            catch (Exception e)
            {
                Debug.Log("<color=purple>" + "[LauncherControl.SaveFileToLocal] There was an error trying to save file " + url + " ERROR: " + e.Message + "</color>");

                return false;
            }

            return true;

        }



    }
}
