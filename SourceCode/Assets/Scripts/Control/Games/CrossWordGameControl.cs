using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CrossWordUtil;

namespace EnglishApp
{
    public class CrossWordGameControl : BaseControl
    {
        // Alphabet to create new letters
        private const string ALPHABETLETTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        /// <summary>
        /// GUI Crossword
        /// </summary>
        [SerializeField] private CrossWordGameUI                m_CrossWordGUI;

        /// Generated cross word
        private CrossWordGenerator                              m_CrossWordSolution;
        // Last row and column pressed
        private int                                             m_LastSelectedRow = -1;
        private int                                             m_LastSelectedColumn = -1;

        // Row and column to start the crossword
        private int                                             m_StartRow;
        private int                                             m_StartColumn;

        // Properties of  highlight cells
        private Color                                           m_ColorStrongHighlight = new Color32(229, 62, 0, 255);
        private Color                                           m_ColorSlightHighlight = new Color32(212, 114, 78, 255);

        private Color                                           m_ColorCorrectWord = new Color32(161, 255, 174, 255);

        private Color                                           m_ColorNormalLetter = new Color32(255, 255, 255, 255);

        private List<CellCrossWord>                             m_LastHighlightCells;
        //private string                                          m_CurrentHighlightWord; // Current word according to the cell selected

        private string m_AcrossWordSelected = string.Empty; // Selected accross word according when a cell is selected
        private string m_DownWordSelected = string.Empty; // Selected down word according when a cell is selected


        // Coords for hints
        private int                                             m_MaxNumberHints = 8;
        private int                                             m_IndexHint = 0;
        private List<Vector2>                                   m_ListCoordHints;

        // Crossword data JSON
        private DataCrossWord                                   m_DataCrossword;

        #region BaseControl        

        public override void Init()
        {

            // Load the data for crossword and create procedural cross word
            m_DataCrossword = GameManager.Instance.DataGamesDictionary.GetDataCrossWord();

            // Init gui
            m_CrossWordGUI.InitTableboard();
            // Setup callbacks IN EACH CELL
            for (int i = 0; i < CrossWordGenerator.NRows; i++)
            {
                for (int j = 0; j < CrossWordGenerator.NCols; j++)
                {
                    m_CrossWordGUI.SetCallbackPiece(i, j, OnCellPress);
                }
            }

            // Setup menu
            List<string> listMenu = new List<string>();
            listMenu.Add("NEW CROSSWORD");
            listMenu.Add("SOLVE");
            listMenu.Add("CLEAR");
            GameManager.Instance.MenuBarControl.InitScroll("Crossword", listMenu);
            GameManager.Instance.MenuBarControl.ScrollMenu.HandleButtonPress += OnHandleMenuButton;

            InitCrosswordLogic();
            m_CrossWordGUI.Show();
        }

        public override void Back()
        {
            Finish();
        }

        public override void Finish()
        {
            GameManager.Instance.MenuBarControl.ScrollMenu.HandleButtonPress -= OnHandleMenuButton;
            m_CrossWordGUI.Hide();
        }

        #endregion BaseControl

        /// <summary>
        /// Init cross word logic
        /// </summary>
        private void InitCrosswordLogic()
        {
            if (m_DataCrossword != null)
            {
                m_CrossWordSolution = new CrossWordGenerator(m_DataCrossword.GetListWords());
                // Print the grid
                SetGridInBoard();
                // Highlight a cell 
                HighlightCell(m_StartRow, m_StartColumn, true);

                // Init hint buttons to unblock
                m_IndexHint = m_MaxNumberHints;
                m_CrossWordGUI.HintButton.Text = "HINTS (" + m_IndexHint + ")";
                m_CrossWordGUI.HintButton.buttonComponent.enabled = true;
                m_CrossWordGUI.HintButton.ColorButton = new Color32(86, 182, 195, 255);
            }

        }

        /// <summary>
        /// Sets the grid from current cross word into the board
        /// </summary>
        private void SetGridInBoard()
        {
            m_ListCoordHints = new List<Vector2>();
            bool findInitWord = false;
            for (int iRow = 0; iRow < CrossWordGenerator.NRows; iRow++)
            {
                for (int iCol = 0; iCol < CrossWordGenerator.NCols; iCol++)
                {
                    char currentLetter = m_CrossWordSolution.Letter(iRow, iCol);
                    if (currentLetter == CrossWordGenerator.EMPTYCHARACTER)
                    {
                        m_CrossWordGUI.GetCell(iRow, iCol).IsBlock = true;
                        m_CrossWordGUI.GetCell(iRow, iCol).Letter = CrossWordGenerator.EMPTYCHARACTER.ToString();

                    }
                    else
                    {
                        m_CrossWordGUI.GetCell(iRow, iCol).IsBlock = false;
                        m_CrossWordGUI.GetCell(iRow, iCol).Letter = "";
                        // Check hint
                        if (!findInitWord)
                        {
                            // 50% chance to start with this valid word for first hightlith
                            if (Random.Range(0, 100) >= 50)
                            {
                                m_StartRow = iRow;
                                m_StartColumn = iCol;
                                findInitWord = true;
                            }
                        }

                        // Set hint
                        Vector2 auxCoordHint = new Vector2(iRow, iCol);
                        m_ListCoordHints.Add(auxCoordHint);
                    }
                }
            }
            // Change the seed and Shuffle list of hints
            Random.InitState((int)System.DateTime.Now.Ticks);
            m_ListCoordHints = Utils.Shuffle(m_ListCoordHints);
        }

        #region HANDLE_CROSSWORD

        /// <summary>
        /// Handle when a cell is pressed
        /// </summary>
        /// <param name="iRow">Index Row</param>
        /// <param name="iColumn">Index Column</param>
        public void OnCellPress(int iRow, int iColumn)
        {
            m_LastSelectedRow = iRow;
            m_LastSelectedColumn = iColumn;
            HighlightCell(iRow, iColumn);            
        }

        /// <summary>
        /// Handles when an available letter is pressed, 
        /// sets that letter and check if accross or down direction the word is completed and correct
        /// That list of cells will be blocked
        /// </summary>
        /// <param name="idLetter">Letter id.</param>
        public void OnLetterButtonPress(int idLetter)
        {
            string auxLetter = m_CrossWordGUI.GetLetter(idLetter);
            
            // Set letter in grid
            m_CrossWordGUI.SetLetter(m_LastSelectedRow, m_LastSelectedColumn, auxLetter);

            // Get accross and down info for this coords in solution grid
            InfoWordCrossword accrossInfo = m_CrossWordSolution.GetAcrossInfo(m_LastSelectedRow, m_LastSelectedColumn);
            InfoWordCrossword downInfo = m_CrossWordSolution.GetAcrossInfo(m_LastSelectedRow, m_LastSelectedColumn);





            // Check if that letter is the last one in the word

            //m_CurrentHighlightWord;
            Debug.Log("OnLetterButtonPress m_AcrossWordSelected: " + m_AcrossWordSelected + " m_DownWordSelected: " + m_DownWordSelected);

            // Check if the lenght are the same to check if both words are the same
            //bool isCorrect = false;
           /* if (m_CurrentHighlightWord.Length == m_LastHighlightCells.Count)
            {
                for (int i = 0; i < m_LastHighlightCells.Count; i++)
                {
                    string auxHLetter = m_LastHighlightCells[i].Letter;
                    string auxCLetter = m_CurrentHighlightWord[i].ToString();
                    // If letter is not null or empty or differents, end the process
                    Debug.LogFormat("m_LastHighlightCells[i]: {0} m_CurrentHighlightWord[i]: {1}:", auxHLetter, auxCLetter);
                    if (string.IsNullOrEmpty(auxHLetter) || (!auxHLetter.Equals(auxCLetter)) )
                    {
                        Debug.Log("NOT CORRECT");
                        isCorrect = false;
                        break;
                    }else
                    {
                        Debug.Log("BOTH CORRECT");
                    }
                }
            }*/

            // If both are correct, change the color of the cells and block the buttons
            /*if (isCorrect)
            {
                Debug.LogFormat("Both corrects");
                for (int i = 0; i < m_LastHighlightCells.Count; i++)
                {
                    m_LastHighlightCells[i].BlockButton();
                    m_LastHighlightCells[i].ColorBackground = m_ColorCorrectWord;
                }
            }
            


            // Erase from the letter
            if (auxLetter == m_CurrentCrossWord.Letter(m_LastPressedRow, m_LastPressedColumn).ToString())
            {
                m_ListCoordHints.Remove(new Vector2(m_LastPressedRow, m_LastPressedColumn));
            }*/
        }

        /// <summary>
        /// Handle when hit button is pressed
        /// </summary>
        public void OnHintButtonPress()
        {
            if (m_IndexHint >= 0)
            {
                int auxRow, auxCol = 0;
                string currentAnswer, correctAnswer = "";
                int i = 0;
                bool findHint = false;
                do
                {
                    auxRow = (int)m_ListCoordHints[i].x;
                    auxCol = (int)m_ListCoordHints[i].y;

                    currentAnswer = m_CrossWordGUI.GetCell(auxRow, auxCol).Letter;
                    correctAnswer = this.m_CrossWordSolution.Letter(auxRow, auxCol).ToString();

                    if (currentAnswer != correctAnswer)
                    {
                        findHint = true;
                    }
                    i++;

                } while ((!findHint) && (i < m_ListCoordHints.Count));

                if (findHint)
                {
                    m_CrossWordGUI.SetLetter(auxRow, auxCol, correctAnswer);
                    m_IndexHint -= 1;

                    m_CrossWordGUI.HintButton.Text = "HINTS (" + m_IndexHint + ")";

                    // Check if maxhints
                    if (m_IndexHint <= 0)
                    {
                        // Block hint button
                        m_CrossWordGUI.HintButton.buttonComponent.enabled = false;
                        m_CrossWordGUI.HintButton.ColorButton = new Color32(80, 109, 113, 255);

                    }
                }
                else
                {
                    // Block hint buttons
                    m_CrossWordGUI.HintButton.buttonComponent.enabled = false;
                    m_CrossWordGUI.HintButton.Text = "HINTS (0)";
                    m_CrossWordGUI.HintButton.ColorButton = new Color32(80, 109, 113, 255);
                }
            }
        }

        /// <summary>
        /// Highlights a cell by a given row and column. 
        /// Highlights the hole word, and sets the question for that word
        /// </summary>
        /// <param name="_iRow">_i row.</param>
        /// <param name="_iCol">_i col.</param>
        /// <param name="startGame">If set to <c>true</c> start game.</param>
        private void HighlightCell(int _iRow, int _iCol, bool startGame = false)
        {
            // Check boundaries
            if ((_iRow <= -1) || (_iRow >= CrossWordGenerator.NRows) ||
                (_iCol <= -1) || (_iCol >= CrossWordGenerator.NCols))
            {
                return;
            }

            // Restore last Highligh cells
            if (m_LastHighlightCells != null)
            {
                for (int i = 0; i < m_LastHighlightCells.Count; i++)
                {
                    m_LastHighlightCells[i].ColorBackground = m_ColorNormalLetter;
                }
            }

            m_LastHighlightCells = new List<CellCrossWord>();
            m_CrossWordGUI.Question = "";

            // Check across
            //int iRowStart = -1;
            //int iColStart = -1;
            // string accrossQuestion = string.Empty;
            //string accrossWord = string.Empty;
            InfoWordCrossword accrossInfo = m_CrossWordSolution.GetAcrossInfo(_iRow, _iCol);
           // if (m_CurrentCrossWord.GetAcrossQuestion(_iRow, _iCol, out iRowStart, out iColStart, out accrossQuestion, out accrossWord))
            if (accrossInfo.Valid)
            {
                // Setup last row, and last column pressed only if is first time
                if (startGame)
                {
                    m_LastSelectedRow = accrossInfo.StartRow;
                    m_LastSelectedColumn = accrossInfo.StartCol;
                }

                m_AcrossWordSelected = accrossInfo.Word;
                m_CrossWordGUI.Question = "Accross: " + accrossInfo.Question;                

                // Add cell for that word
                for (int i = accrossInfo.StartCol; i < CrossWordGenerator.NCols; i++)
                {
                    if (m_CrossWordSolution.Letter(accrossInfo.StartRow, i) != CrossWordGenerator.EMPTYCHARACTER)
                    {
                        if ((accrossInfo.StartRow == _iRow) && (i == _iCol))
                        {
                            m_CrossWordGUI.GetCell(accrossInfo.StartRow, i).ColorBackground = m_ColorStrongHighlight;
                        }
                        else
                        {
                            m_CrossWordGUI.GetCell(accrossInfo.StartRow, i).ColorBackground = m_ColorSlightHighlight;
                        }

                        m_LastHighlightCells.Add(m_CrossWordGUI.GetCell(accrossInfo.StartRow, i));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Check across
            //iRowStart = -1;
            //iColStart = -1;
            //string downQuestion = "";
            //string downWord = "";


            InfoWordCrossword downInfo = m_CrossWordSolution.GetDownInfo(_iRow, _iCol);

            if (downInfo.Valid)
            //if (m_CurrentCrossWord.TryGetDownQuestion(_iRow, _iCol, out iRowStart, out iColStart, out downQuestion, out downWord))
            {
                // Setup last row, and last column pressed only if is first time
                if (startGame)
                {
                    m_LastSelectedRow = downInfo.StartRow;
                    m_LastSelectedColumn = downInfo.StartCol;
                }
                m_DownWordSelected = downInfo.Word;
                if (accrossInfo.Valid)
                {
                    m_CrossWordGUI.Question += "\nDown: " + downInfo.Question;
                }
                else
                {
                    m_CrossWordGUI.Question += "Down: " + downInfo.Question;
                }

                for (int i = downInfo.StartRow; i < CrossWordGenerator.NRows; i++)
                {
                    if (m_CrossWordSolution.Letter(i, downInfo.StartCol) != CrossWordGenerator.EMPTYCHARACTER)
                    {
                        if ((i == _iRow) && (downInfo.StartCol == _iCol))
                        {
                            m_CrossWordGUI.GetCell(i, downInfo.StartCol).ColorBackground = m_ColorStrongHighlight;
                        }
                        else
                        {
                            m_CrossWordGUI.GetCell(i, downInfo.StartCol).ColorBackground = m_ColorSlightHighlight;
                        }
                        m_LastHighlightCells.Add(m_CrossWordGUI.GetCell(i, downInfo.StartCol));
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Set the letters
            Debug.Log("AccrossWord valid: " + accrossInfo.Valid + " Letter: " + accrossInfo.Letter + " - Question: " + accrossInfo.Question + " - Word:" + accrossInfo.Word);
            Debug.Log("DownWord valid: " + downInfo.Valid + " Letter: " + downInfo.Letter + " - Question: " + downInfo.Question + " - Word:" + downInfo.Word);

            // Sets the letters for this word
            //if ((!string.IsNullOrEmpty(accrossWord) 
            {
                

                Random.InitState((int)System.DateTime.Now.Ticks);

                // Letters
                List<char> listLetters = new List<char>();

                // Add correct letter in list letters first
                if (accrossInfo.Valid && downInfo.Valid)
                {
                    listLetters.Add(accrossInfo.Letter);

                }else if (accrossInfo.Valid && !downInfo.Valid)
                {
                    listLetters.Add(accrossInfo.Letter);
                }else
                {
                    listLetters.Add(downInfo.Letter);
                }


                // Add rest of the letters to the list
                if (accrossInfo.Valid)
                {
                    // Include valid letter  
                    for (int i = 0; i < accrossInfo.Word.Length; i++)
                    {
                        // Don't add the correct letter if it's already there
                        if (accrossInfo.Letter != accrossInfo.Word[i])
                        {
                            listLetters.Add(accrossInfo.Word[i]);
                        }
                    }
                }

                // Add each letter to the list
                if (downInfo.Valid)
                {
                    for (int i = 0; i < downInfo.Word.Length; i++)
                    {
                        if (downInfo.Letter != downInfo.Word[i])
                        {
                            listLetters.Add(downInfo.Word[i]);
                        }
                    }
                }


                //Debug.Log("listLetters.Count: " + listLetters.Count);

                for (int i = (listLetters.Count-1); i< m_CrossWordGUI.NumberOfLetters; i++)
                {
                    int iChanceWord = Random.Range(0, ALPHABETLETTERS.Length);
                    listLetters.Add(ALPHABETLETTERS[iChanceWord]);
                }


               // Debug.Log("Final number letters listLetters.Count: " + listLetters.Count);

                // Shuffle list and set the 
                listLetters = Utils.Shuffle(listLetters);
                m_CrossWordGUI.SetLetterButtons(listLetters);

                // Random letters to fill 
                // Include random extra letters to fill all the gaps, maximun 10 letters m_CrossWordGUI.LetterNumber

                /*// Set extra letters
                for (int i = (word.Length - 1); i < 10; i++)
                {
                    int chanceWord = Random.Range(0, ALPHABETLETTERS.Length);
                    listLetters.Add(ALPHABETLETTERS[chanceWord].ToString());
                }*/

                // Shuffle list

              
            }
        }



        /// <summary>
        /// Handle when the finish button is pressed
        /// </summary>
        public void OnFinishPress()
        {
            GameManager.Instance.MenuBarControl.Show();
        }

        /// <summary>
        /// Handle when an button option is pressed
        /// </summary>
        /// <param name="id">Identifier.</param>
        private void OnHandleMenuButton(int id)
        {
            GameManager.Instance.MenuBarControl.Close();
            switch (id)
            {
                case 0:
                    // Solve
                    NewCrossword();
                    break;
                case 1:
                    // Solve
                    SolveCrossword();
                    break;
                case 2:
                    // Clear
                    ClearCrossword();
                    break;
            }
        }

        /// <summary>
        /// Method to show the solution of the cross
        /// </summary>
        private void NewCrossword()
        {
            InitCrosswordLogic();
        }


        /// <summary>
        /// Method to show the solution of the cross
        /// </summary>
        private void SolveCrossword()
        {
            for (int iRow = 0; iRow < CrossWordGenerator.NRows; iRow++)
            {
                for (int iCol = 0; iCol < CrossWordGenerator.NCols; iCol++)
                {
                    m_CrossWordGUI.GetCell(iRow, iCol).Letter = m_CrossWordSolution.Letter(iRow, iCol).ToString();
                }
            }
            // Block hint buttons
            m_CrossWordGUI.HintButton.buttonComponent.enabled = false;
            m_CrossWordGUI.HintButton.Text = "HINTS (0)";
            m_CrossWordGUI.HintButton.ColorButton = new Color32(80, 109, 113, 255);
        }

        /// <summary>
        /// Method to clear the crossword
        /// </summary>
        private void ClearCrossword()
        {
            for (int iRow = 0; iRow < CrossWordGenerator.NRows; iRow++)
            {
                for (int iCol = 0; iCol < CrossWordGenerator.NCols; iCol++)
                {
                    m_CrossWordGUI.GetCell(iRow, iCol).Letter = "";
                }
            }
        }
        #endregion HANDLE_CROSSWORD
    }
}
