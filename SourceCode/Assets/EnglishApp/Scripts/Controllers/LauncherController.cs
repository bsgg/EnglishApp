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

        public enum EDATATYPE { NONE = -1, GRAMMAR = 0, VOCABULARY, PHRASAL_VERBS };
        public enum ERESULT { SUCCESS, FAIL };

        [SerializeField] private string m_CloudDataUrl = "http://beatrizcv.com/Data/English/";

        [SerializeField] private string m_PicturesFolder = "Pictures";
        private string m_LocalPictureDirectory;

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

        [SerializeField] private List<WordDictionary> m_PhrasalVerbSet;
        public List<WordDictionary> PhrasalVerbSet
        {
            get { return m_PhrasalVerbSet; }
        }

        private ERESULT m_Result;

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
            m_PhrasalVerbSet = new List<WordDictionary>();

            m_Progress.Show();
            m_Progress.SetProgress("Wait..");

            m_Initialized = true;
            int nFilesInLocal = 0;

            // local Pictures folder
            m_LocalPictureDirectory = Path.Combine(Application.persistentDataPath, m_PicturesFolder);
            if (!Directory.Exists(m_LocalPictureDirectory))
            {
                Directory.CreateDirectory(m_LocalPictureDirectory);
            }

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

                        if (!LoadSubData((EDATATYPE)i))
                        {
                            return false;
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

        private bool LoadSubData(EDATATYPE tData)
        {
            if (m_InfoFileList == null)
            {
                return false;
            }

            int indexData = (int)tData;

            if (indexData >= m_InfoFileList.Count)
            {
                return false;
            }

            // Read sub data 
            for (int iData = 0; iData < m_InfoFileList[indexData].Data.Data.Count; iData++)
            {
                string localDataFile = Path.Combine(Application.persistentDataPath, m_InfoFileList[indexData].DataFolderName);
                localDataFile = Path.Combine(localDataFile, m_InfoFileList[indexData].Data.Data[iData].FileName + ".json");
                string jsonData = string.Empty;
                if (ReadFile(localDataFile, ref jsonData))
                {
                    if (m_InfoFileList[indexData].DataType == EDATATYPE.VOCABULARY)
                    {
                        WordDictionary set = JsonUtility.FromJson<WordDictionary>(jsonData);
                        m_VocabularySet.Add(set);

                        // Load images
                        for (int iWord = 0; iWord < set.Data.Count; iWord++)
                        {
                            if (!string.IsNullOrEmpty(set.Data[iWord].PictureName))
                            {
                                string pictureUrl = Path.Combine(m_LocalPictureDirectory, set.Data[iWord].PictureName);
                                if (File.Exists(pictureUrl))
                                {
                                    Texture2D texture = new Texture2D(2, 2);

                                    try
                                    {
                                        byte[] pictureData = File.ReadAllBytes(pictureUrl);

                                        texture.LoadImage(pictureData);

                                        Rect rec = new Rect(0, 0, texture.width, texture.width);

                                        set.Data[iWord].Sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
                                    }
                                    catch (Exception e)
                                    {
                                        Debug.Log("<color=red>" + "[LauncherControl.LoadSubData] There was an error trying to get file (image) " + pictureUrl + " ERROR: " + e.Message + "</color>");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else if (m_InfoFileList[indexData].DataType == EDATATYPE.GRAMMAR)
                    {
                        GrammarDictionary set = JsonUtility.FromJson<GrammarDictionary>(jsonData);
                        m_DictionarySet.Add(set);
                    }
                    else if (m_InfoFileList[indexData].DataType == EDATATYPE.PHRASAL_VERBS)
                    {
                        WordDictionary set = JsonUtility.FromJson<WordDictionary>(jsonData);
                        m_PhrasalVerbSet.Add(set);
                    }
                }
                else
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
            m_PhrasalVerbSet = new List<WordDictionary>();

            string pictureFolder = Path.Combine(m_CloudDataUrl, m_PicturesFolder);

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

                    m_Result = ERESULT.SUCCESS;
                    yield return DownloadSubData((EDATATYPE) i);

                    m_Progress.Show();
                    if (m_Result == ERESULT.FAIL)
                    {                       
                        if (OnDownloadCompleted != null)
                        {
                            OnDownloadCompleted(ERESULT.FAIL, "Fail to download sub data");
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

        public void OnDownloadDataPress(int id)
        {
            m_Result = ERESULT.SUCCESS;

            m_Progress.Show();
            m_Progress.SetProgress("Wait.."); 
            
            StartCoroutine(DownloadSubData((EDATATYPE)id));
        }  

        public IEnumerator DownloadSubData(EDATATYPE tData)
        {
            m_Result = ERESULT.SUCCESS;

            if (m_InfoFileList == null)
            {               
                m_Result = ERESULT.FAIL;
                yield break;
            }

            int indexData = (int)tData;

            if (indexData >= m_InfoFileList.Count)
            {
                m_Result = ERESULT.FAIL;
                yield break;
            }

            string pictureFolder = Path.Combine(m_CloudDataUrl, m_PicturesFolder);            

            for (int iData = 0; iData < m_InfoFileList[indexData].Data.Data.Count; iData++)
            {
                m_Progress.SetProgress("Downloading " + m_InfoFileList[indexData].Data.Data[iData].FileName);


                string fileName = m_InfoFileList[indexData].Data.Data[iData].FileName + ".json";
                string dataCloudURL = m_CloudDataUrl + "/" + m_InfoFileList[indexData].DataFolderName + "/" + fileName;
                Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] Retrieving (" + iData + "/" + m_InfoFileList[indexData].Data.Data.Count + ") - URL: " + dataCloudURL + "</color>");
                WWW wwwDataFile = new WWW(dataCloudURL);

                yield return wwwDataFile;

                if (!string.IsNullOrEmpty(wwwDataFile.text))
                {
                    string dataLocalURL = Path.Combine(Application.persistentDataPath, m_InfoFileList[indexData].DataFolderName);
                    dataLocalURL = Path.Combine(dataLocalURL, fileName);

                    if (SaveFileToLocal(dataLocalURL, wwwDataFile))
                    {
                        if (m_InfoFileList[indexData].DataType == EDATATYPE.VOCABULARY)
                        {
                            WordDictionary set = JsonUtility.FromJson<WordDictionary>(wwwDataFile.text);
                            m_VocabularySet.Add(set);

                            // Download images
                            for (int iWord = 0; iWord < set.Data.Count; iWord++)
                            {
                                if (!string.IsNullOrEmpty(set.Data[iWord].PictureName))
                                {
                                    string pictureUrl = Path.Combine(pictureFolder, set.Data[iWord].PictureName);

                                    Debug.Log("<color=purple>" + "[LauncherControl.DownloadData] Picture URL at " + pictureUrl + "</color>");

                                    WWW wwwPictureFile = new WWW(pictureUrl);

                                    yield return wwwPictureFile;

                                    if (wwwPictureFile.texture != null)
                                    {
                                        Debug.Log("<color=purple>" + "[LauncherControl.RequestRecipe] Texture: (" + wwwPictureFile.texture.width + " x " + wwwPictureFile.texture.height + ")" + "</color>");

                                        Texture2D texture = new Texture2D(wwwPictureFile.texture.width, wwwPictureFile.texture.height, TextureFormat.DXT1, false);
                                        wwwPictureFile.LoadImageIntoTexture(texture);

                                        Rect rec = new Rect(0, 0, texture.width, texture.height);

                                        set.Data[iWord].Sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);

                                        yield return new WaitForEndOfFrame();


                                        string localPicture = Path.Combine(m_LocalPictureDirectory, set.Data[iWord].PictureName);

                                        // Load Picture in memory
                                        SaveFileToLocal(localPicture, wwwPictureFile);                                       
                                       
                                    }else
                                    {
                                        m_Result = ERESULT.FAIL;
                                        break;
                                    }

                                    wwwPictureFile.Dispose();
                                    wwwPictureFile = null;
                                }

                            }
                        }
                        else if (m_InfoFileList[indexData].DataType == EDATATYPE.GRAMMAR)
                        {
                            GrammarDictionary set = JsonUtility.FromJson<GrammarDictionary>(wwwDataFile.text);
                            m_DictionarySet.Add(set);

                        }else if (m_InfoFileList[indexData].DataType == EDATATYPE.PHRASAL_VERBS)
                        {
                            WordDictionary set = JsonUtility.FromJson<WordDictionary>(wwwDataFile.text);
                            m_PhrasalVerbSet.Add(set);
                        }
                    }
                }else
                {
                    m_Result = ERESULT.FAIL;
                    break;
                }
            }

            m_Progress.Hide();

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
