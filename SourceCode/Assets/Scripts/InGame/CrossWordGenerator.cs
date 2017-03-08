using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EnglishApp;

namespace CrossWordUtil
{
	#region Coord Struct


	/// <summary>
	/// Class to handle a Coord in crossword
	/// </summary>
	public class Coord
	{
		private int iRow;
		public int IRow
		{
			set { this.iRow = value; }
			get { return this.iRow; }
		}
		private int iCol;
		public int ICol
		{
			set { this.iCol = value; }
			get { return this.iCol; }
		}
		private int iLetter;
		public int ILetter
		{
			set { this.iLetter = value; }
			get { return this.iLetter; }
		}
		private bool isDown;
		public bool IsDown
		{
			set { this.isDown = value; }
			get { return this.isDown; }
		}
		private bool isAcross;
		public bool IsAcross
		{
			set { this.isAcross = value; }
			get { return this.isAcross; }
		}
		private int score;
		public int Score
		{
			set { this.score = value; }
			get { return this.score; }
		}

		public Coord(int _iRow, int _iCol, int _iLetter)
		{
			iRow = _iRow;
			iCol = _iCol;
			iLetter = _iLetter;
			isDown = false;
			isAcross = false;
			score = 0;
		}
	}
	#endregion Coord Struct

	public class CrossWordGenerator
	{	
		public static int RANGEWORDS = 20;
		public static int NRows = 8;
		public static int NCols = 11;
		public static char EMPTYCHARACTER = '_';

		// Grid Crossword
		char[,] grid;

		/// <summary>
		/// List of single words
		/// </summary>
		List<SingleWord> listSingleWords;

		/// <summary>
		/// Gets a letter in grid
		/// </summary>
		/// <param name="_iRow">Index Column</param>
		/// <param name="_iCol">Index Row</param>
		public char Letter(int _iRow, int _iCol)
		{		
			if ((_iRow <= -1) || (_iRow >= NRows) ||
			    (_iCol <= -1) || (_iCol >= NCols)) 
			{
				return EMPTYCHARACTER;
				
			}
			return grid[_iRow,_iCol];
		}

		public CrossWordGenerator(List<SingleWord> _lWords)
		{
			listSingleWords = _lWords;
			grid = new char[NRows, NCols];
			// Init table with an empty character
			for (int iRow= 0; iRow<NRows; iRow++) 
			{
				for (int iCol= 0; iCol<NCols; iCol++) 
				{
					grid [iRow, iCol] = EMPTYCHARACTER;
				}				
			}

			if ((listSingleWords != null) && (listSingleWords.Count > 0))
			{
				listSingleWords[0].IsInCrossWord = false;
				// Put first word in a random position
				// Check if it fits in down direction
				if (listSingleWords [0].Answer.Length < NRows)
				{
					int row = Random.Range (0, NRows - listSingleWords[0].Answer.Length);
					int col = Random.Range (0, NCols);
					Coord auxCoord = new Coord (row, col, 0);
					auxCoord.IsDown = true;
					putWord (auxCoord, listSingleWords [0].Answer);
					listSingleWords[0].IsInCrossWord = true;

				} else if (listSingleWords [0].Answer.Length < NCols) 
				{
					int row = Random.Range (0, NRows );
					int col = Random.Range (0, NCols - listSingleWords[0].Answer.Length);

					Coord auxCoord = new Coord (row, col, 0);
					auxCoord.IsAcross = true;
					putWord (auxCoord, listSingleWords[0].Answer);
					listSingleWords[0].IsInCrossWord = true;
				}

				// Put the rest of the words in grid
				for (int i= 1; i<listSingleWords.Count; i++)
				{
					string auxWord = listSingleWords[i].Answer;
					//List<Coord> lSugestedCoords = getSuggestedCoordsWord(auxWord);
					Coord finalCoord = getCoord(auxWord);					
					if (finalCoord != null) 
					{
						// Mark word as used
						putWord(finalCoord, auxWord);
						listSingleWords[i].IsInCrossWord = true;
					}else
					{
						listSingleWords[i].IsInCrossWord = false;
					}
				}


				// DEBUG
				/*Debug.Log("******** FINAL GRID ***********");
				for (int iRow= 0; iRow<NRows; iRow++) 
				{
					string col = "";
					for (int iCol= 0; iCol<NCols; iCol++) 
					{
						col += (grid[iRow, iCol] + " ");
					}

					Debug.Log("ROW: " + iRow + ":   " + col);
					
				}
				Debug.Log("******** FINAL GRID ***********");*/
				// DEBUG

			}

			// DEBUG
			/*Debug.Log("******** words? ***********");
			for (int i= 0; i<listSingleWords.Count; i++) 
			{

				Debug.Log(listSingleWords[i].Answer + "   ISINCROSS? " + listSingleWords[i].IsInCrossWord);
				
			}
			Debug.Log("******** FINAL GRID ***********");*/
			// DEBUG
		}

		/// <summary>
		/// Puts a word in a given coord
		/// </summary>
		/// <param name="_currentCoord">_current coordinate.</param>
		/// <param name="_currentWord">_current word.</param>
		private void putWord(Coord _currentCoord, string _currentWord)
		{
			if (_currentCoord != null) 
			{
				// Put word down direction
				if (_currentCoord.IsDown)
				{
					grid [_currentCoord.IRow,_currentCoord.ICol] = _currentWord[_currentCoord.ILetter];

					int iRow = _currentCoord.IRow + 1;
					for (int iLetter = (_currentCoord.ILetter+1); iLetter < _currentWord.Length; iLetter++)
					{
						grid [iRow,_currentCoord.ICol] = _currentWord[iLetter];
						iRow++;
					}
					iRow = _currentCoord.IRow - 1;
					for (int iLetter = (_currentCoord.ILetter-1); iLetter >= 0 ; iLetter--)
					{
						grid [iRow,_currentCoord.ICol] = _currentWord[iLetter];
						iRow--;
					}

				}else if (_currentCoord.IsAcross)
				{
					grid [_currentCoord.IRow,_currentCoord.ICol] = _currentWord[_currentCoord.ILetter];
					
					int iCol = _currentCoord.ICol + 1;
					for (int iLetter = (_currentCoord.ILetter+1); iLetter < _currentWord.Length; iLetter++)
					{
						grid [_currentCoord.IRow,iCol] = _currentWord[iLetter];
						iCol++;
					}
					iCol = _currentCoord.ICol - 1;
					for (int iLetter = (_currentCoord.ILetter-1); iLetter >= 0 ; iLetter--)
					{
						grid [_currentCoord.IRow,iCol] = _currentWord[iLetter];
						iCol--;
					}
				}
			}

		}


		/// <summary>
		/// Method to get suggested coords for a word 
		/// </summary>
		/// <returns>List of coords</returns>
		/// <param name="word">Given word</param>
		private Coord getCoord(string _word)
		{
			List<Coord> lSugestedCoords = new List<Coord>();		
			// Cycle each letter in the given word
			for (int iLetter= 0; iLetter < _word.Length; iLetter++) 
			{
				for (int iRow= 0; iRow<NRows; iRow++) 
				{
					for (int iCol= 0; iCol<NCols; iCol++) 
					{
						// Find a match
						char gridLetter = grid[iRow,iCol];
						char auxLetter = _word[iLetter];
						
						if (gridLetter == auxLetter)
						{
							// Filter this match according its size
							// Check down
							Coord auxC = new Coord(iRow,iCol,iLetter);
							auxC.Score = -1;
							
							// Check across
							if (((iCol + (_word.Length - iLetter))<= NCols) &&
							    ((iCol - iLetter) >= 0))
							{
								auxC.IsAcross = true;
							}
							
							// Check down
							if (((iRow + (_word.Length - iLetter))<= NRows) &&
							    ((iRow - iLetter) >= 0))
							{
								auxC.IsDown = true;
							}
							
							if (auxC.IsDown || auxC.IsAcross)
							{
								lSugestedCoords.Add(auxC);
							}
						}
					}
				}
			}
			lSugestedCoords = Utils.Shuffle (lSugestedCoords);
			Coord finalCoord = getFinalCoord(lSugestedCoords,_word);
			return finalCoord;
		}

		/// <summary>
		/// Checks if a left and right cell there is a correct letter (empty or intersect letter)
		/// </summary>
		/// <returns>True if is a correct letter (empty char or a crossing letter)</returns>
		/// <param name="_gridLetter">Letter in grid</param>
		/// <param name="_currentLetter">Current Letter</param>
		/// <param name="_currentRow">Row</param>
		/// <param name="_currentCol">Column</param>
		/// <param name="_score">Output scoreCross (a letter cross)</param>
		private bool checkLeftRightSide(char _gridLetter, char _currentLetter, int _currentRow, int _currentCol, out int _scoreCross)
		{
			_scoreCross = 0;
			// Not a empty cell check if is a cross letter
			if (_gridLetter != EMPTYCHARACTER)
			{
				if (_gridLetter != _currentLetter)
				{
					return false;
				}else
				{
					_scoreCross = 1;
					return true;
				}
			}else
			{		
				// Empty char, check if on left and righ col there is an empty letter
				// left
				int auxLeft = _currentCol -1;
				if (auxLeft >= 0)
				{
					if (grid[_currentRow,auxLeft] != EMPTYCHARACTER)
					{
						return false;
					}
				}
				
				// Right
				int auxRight = _currentCol +1;
				if (auxRight < NCols)
				{
					if (grid[_currentRow,auxRight] != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>
		/// Checks if a top and botton cell there is a correct letter (empty or intersect letter)
		/// </summary>
		/// <returns>True if is a correct letter (empty char or a crossing letter)</returns>
		/// <param name="_gridLetter">Letter in grid</param>
		/// <param name="_currentLetter">Current Letter</param>
		/// <param name="_currentRow">Row</param>
		/// <param name="_currentCol">Column</param>
		/// <param name="_score">Output scoreCross (a letter cross)</param>
		private bool checkTopBottonSide(char _gridLetter, char _currentLetter, int _currentRow, int _currentCol, out int _scoreCross)
		{
			_scoreCross = 0;
			// Not a empty cell check if is a cross letter
			if (_gridLetter != EMPTYCHARACTER)
			{
				if (_gridLetter != _currentLetter)
				{
					return false;
				}else
				{
					_scoreCross = 1;
					return true;
				}
			}else
			{
				// Empty char, check if on top and bottom col there is an empty letter
				// top
				int auxTop = _currentRow -1;
				if (auxTop >= 0)
				{
					if (grid[auxTop,_currentCol] != EMPTYCHARACTER)
					{
						return false;
					}
				}
				
				// bottom
				int auxDown = _currentRow +1;
				if (auxDown < NRows)
				{
					if (grid[auxDown,_currentCol] != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}
			return true;
		}

		#region EMPTY_LAST_FIRST_LETTER

		/// <summary>
		/// Check if there is an empty letter first letter in down direction
		/// </summary>
		/// <returns><c>true</c>If empty letter <c>false</c> otherwise.
		/// Besides, it returns true if is not the first word or if is out of the rows
		/// </returns>
		/// <param name="_iLetter">Index letter</param>
		/// <param name="_wordLength">Word letter</param>
		/// <param name="_currentRow">Current Row</param>
		/// <param name="_currentCol">Current Col</param>
		private bool isEmptyFirstDownLetter(int _iLetter, int _wordLength, int _currentRow, int _currentCol)
		{
			if (_iLetter ==  0) 
			{
				int auxRow = _currentRow - 1;
				if (auxRow >= 0)
				{
					char gridLetter = grid[auxRow,_currentCol];
					if (gridLetter != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}
			
			return true;
			
		}

		/// <summary>
		/// Check if there is an empty letter last letter in down direction
		/// </summary>
		/// <returns><c>true</c>If empty letter <c>false</c> otherwise.
		/// Besides, it returns true if is not the last word or if is out of the rows
		/// </returns>
		/// <param name="_iLetter">Index letter</param>
		/// <param name="_wordLength">Word letter</param>
		/// <param name="_currentRow">Current Row</param>
		/// <param name="_currentCol">Current Col</param>
		private bool isEmptyLastDownLetter(int _iLetter, int _wordLength, int _currentRow, int _currentCol)
		{
			if (_iLetter == (_wordLength - 1)) 
			{
				int auxRow = _currentRow + 1;
				if (auxRow < NRows)
				{
					char gridLetter = grid[auxRow,_currentCol];
					if (gridLetter != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}

			return true;

		}

		/// <summary>
		/// Check if there is an empty letter first letter in across direction
		/// </summary>
		/// <returns><c>true</c>If empty letter <c>false</c> otherwise.
		/// Besides, it returns true if is not the first word or if is out of the rows
		/// </returns>
		/// <param name="_iLetter">Index letter</param>
		/// <param name="_wordLength">Word letter</param>
		/// <param name="_currentRow">Current Row</param>
		/// <param name="_currentCol">Current Col</param>
		private bool isEmptyFirstAcrossLetter(int _iLetter, int _wordLength, int _currentRow, int _currentCol)
		{
			if (_iLetter ==  0) 
			{
				int auxCol = _currentCol - 1;
				if (auxCol >= 0)
				{
					char gridLetter = grid[_currentRow,auxCol];
					if (gridLetter != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}
			
			return true;
			
		}

		/// <summary>
		/// Check if there is an empty letter last letter in across direction
		/// </summary>
		/// <returns><c>true</c>If empty letter <c>false</c> otherwise.
		/// Besides, it returns true if is not the first word or if is out of the rows
		/// </returns>
		/// <param name="_iLetter">Index letter</param>
		/// <param name="_wordLength">Word letter</param>
		/// <param name="_currentRow">Current Row</param>
		/// <param name="_currentCol">Current Col</param>
		private bool isEmptyLastAcrossLetter(int _iLetter, int _wordLength, int _currentRow, int _currentCol)
		{
			if (_iLetter ==  (_wordLength-1)) 
			{
				int auxCol = _currentCol + 1;
				if (auxCol < NCols)
				{
					char gridLetter = grid[_currentRow,auxCol];
					if (gridLetter != EMPTYCHARACTER)
					{
						return false;
					}
				}
			}			
			return true;		
		}

		#endregion EMPTY_LAST_FIRST_LETTER

		/// <summary>
		/// Checks a given in down direction
		/// </summary>
		/// <returns>Score for the given coord</returns>
		/// <param name="_currentCoord">Current Coordinate</param>
		/// <param name="_currentWord">Word refers to coord</param>
		private int checkDownCoord(Coord _currentCoord,string _currentWord)
		{
			int currentRow = _currentCoord.IRow;
			int currentCol = _currentCoord.ICol;
			int scoreCoord = -1;

			bool validCoord = true;
			int auxRow = currentRow;
			// Previous filter
			// Check if over or under me, there is an empty cell
			bool isFirstDownEmpty = isEmptyFirstDownLetter(_currentCoord.ILetter,_currentWord.Length,currentRow,currentCol);
			bool isLastDownEmpty = isEmptyLastDownLetter(_currentCoord.ILetter,_currentWord.Length,currentRow,currentCol);
			
			if (!isFirstDownEmpty || !isLastDownEmpty) validCoord = false;
			
			if (validCoord)
			{
				// Go down from this position until  reach last row or las index
				int auxIndexLetter = _currentCoord.ILetter +1;
				auxRow = currentRow + 1;
				
				scoreCoord = 0;
				while ((auxIndexLetter < _currentWord.Length) && (auxRow < NRows) && validCoord)
				{
					
					char gridLetter = grid[auxRow,currentCol];
					char currentLetter = _currentWord[auxIndexLetter];
					int auxScore = 0;
					
					// Check if is the last letter, if an empty letter
					bool isEmpty = isEmptyLastDownLetter(auxIndexLetter,_currentWord.Length,auxRow,currentCol);
					if (isEmpty)
					{
						validCoord = checkLeftRightSide(gridLetter,currentLetter,auxRow,currentCol, out auxScore);
						if (validCoord)
						{
							scoreCoord += auxScore;
							auxIndexLetter++;
							auxRow++;
						}
					}else
					{
						validCoord = false;
					}
				}
			}
			
			// Check go up if is a valid word
			if (validCoord)
			{
				int auxIndexLetter =  _currentCoord.ILetter -1;
				auxRow = currentRow - 1;
				
				while ((auxIndexLetter >= 0) && (auxRow >= 0) && validCoord)
				{
					char gridLetter = grid[auxRow,currentCol];
					char currentLetter = _currentWord[auxIndexLetter];
					int auxScore = 0;
					
					bool isEmpty = isEmptyFirstDownLetter(auxIndexLetter,_currentWord.Length,auxRow,currentCol);
					if (isEmpty)
					{
						
						validCoord = checkLeftRightSide(gridLetter,currentLetter,auxRow,currentCol, out auxScore);
						if (validCoord)
						{
							scoreCoord += auxScore;
							auxIndexLetter--;
							auxRow--;
						}
					}else
					{
						validCoord = false;
					}
					
				}
			}
			if (validCoord) 
			{
				return scoreCoord;
			}
			
			return -1;

		}


		/// <summary>
		/// Checks a given in across direction
		/// </summary>
		/// <returns>Score for the given coord</returns>
		/// <param name="_currentCoord">Current Coordinate</param>
		/// <param name="_currentWord">Word refers to coord</param>
		private int checkAcrossCoord(Coord _currentCoord,string _currentWord)
		{
			int currentRow = _currentCoord.IRow;
			int currentCol = _currentCoord.ICol;
			int scoreCoord = -1;

			bool validCoord = true;
			int auxCol = currentCol;

			// Check if over or under me, there is an empty cell
			bool isFirstAcrossEmpty = isEmptyFirstAcrossLetter(_currentCoord.ILetter,_currentWord.Length,currentRow,currentCol);
			bool isLastAcrossEmpty = isEmptyLastAcrossLetter(_currentCoord.ILetter,_currentWord.Length,currentRow,currentCol);
			if (!isFirstAcrossEmpty || !isLastAcrossEmpty) validCoord = false;

			// Go right from this position until reach the last column or the last index
			if (validCoord) 
			{
				int auxIndexLetter = _currentCoord.ILetter + 1;
				auxCol = currentCol + 1;

				scoreCoord = 0;
				while ((auxIndexLetter < _currentWord.Length) && (auxCol < NCols) && validCoord) 
				{
					char gridLetter = grid [currentRow, auxCol];
					char currentLetter = _currentWord [auxIndexLetter];
					int auxScore = 0;

					// Check if is the last letter, if an empty letter
					bool isEmpty = isEmptyLastAcrossLetter (auxIndexLetter, _currentWord.Length, currentRow, auxCol);
					if (isEmpty) 
					{
						validCoord = checkTopBottonSide (gridLetter, currentLetter, currentRow, auxCol, out auxScore);
						if (validCoord)
						{
							scoreCoord += auxScore;
							auxIndexLetter++;
							auxCol++;
						}
					} else {
						validCoord = false;
					}
				}
			}


			// Check go left if is a valid word
			if (validCoord)
			{
				int auxIndexLetter =  _currentCoord.ILetter -1;
				auxCol = currentCol - 1;
				
				while ((auxIndexLetter >= 0) && (auxCol >= 0) && validCoord)
				{
					char gridLetter = grid[currentRow,auxCol];
					char currentLetter = _currentWord[auxIndexLetter];
					int auxScore = 0;
					
					bool isEmpty = isEmptyFirstAcrossLetter(auxIndexLetter,_currentWord.Length,currentRow,auxCol);
					if (isEmpty)
					{						
						validCoord = checkTopBottonSide(gridLetter,currentLetter,currentRow,auxCol, out auxScore);
						if (validCoord)
						{
							scoreCoord += auxScore;
							auxIndexLetter--;
							auxCol--;
						}
					}else
					{
						validCoord = false;
					}
					
				}
			}
			if (validCoord) 
			{
				return scoreCoord;
			}

			return -1;

		}


		/// <summary>
		/// Gets the final coordinate for the given word
		/// </summary>
		/// <param name="_lCoords">List of coordinates</param>
		/// <param name="_word">Current Word</param>
		private Coord getFinalCoord(List<Coord> _lCoords, string _word)
		{
			List<Coord> lFinalCoords = new List<Coord> ();

			if (_lCoords != null) 
			{
				for (int i= 0; i<_lCoords.Count; i++)
				{
					// Word could go down, check if is a valid position and set a score 
					// acording with the number of matches in grid
					if (_lCoords[i].IsDown)
					{
						int score = checkDownCoord(_lCoords[i],_word);

						if (score >= 0)
						{
							Coord auxC = new Coord(_lCoords[i].IRow,_lCoords[i].ICol,_lCoords[i].ILetter);
							auxC.IsDown = true;
							auxC.ILetter = _lCoords[i].ILetter;
							auxC.Score = score;
							lFinalCoords.Add(auxC);
						}


					}
					if (_lCoords[i].IsAcross)
					{
						int score = checkAcrossCoord(_lCoords[i], _word);
						if (score >= 0)
						{
							Coord auxC = new Coord(_lCoords[i].IRow,_lCoords[i].ICol,_lCoords[i].ILetter);
							auxC.IsAcross = true;
							auxC.ILetter = _lCoords[i].ILetter;
							auxC.Score = score;
							lFinalCoords.Add(auxC);
						}
					}
				}				
			}

			if (lFinalCoords.Count > 0) 
			{
				// Sort all cors by its score
				lFinalCoords.Sort (Utils.Compare);

				return lFinalCoords[0];
			}

			return null;
		}

		#region HANDLE_CROSS_WORD

		/// <summary>
		/// Tries to get the question in across coord if there is an actual word according to the given coord
		/// </summary>
		/// <returns>Returns wheter it was succes or not</returns>
		/// <param name="_iRow">Row to check</param>
		/// <param name="_iCol">Col to check</param>
		/// <param name="_iRowStart">Output row for the word</param>
		/// <param name="_iColStart">Output col for the word</param>
		/// <param name="_question">Question for that word</param>
		/// <param name="_word">Word for that question</param>
		public bool TryGetAcrossQuestion(int _iRow, int _iCol, out  int _iRowStart, out int _iColStart, out string _question,out string _word)
		{
			// Piece
			_iRowStart = _iRow;
			_question = "";
			_word = "";

			// Find first across position
			int auxICol =_iCol;		
			for (int i = auxICol; i>=0; i--) 
			{
				if (grid[_iRow,i] == CrossWordGenerator.EMPTYCHARACTER)
				{
					auxICol = i+1;
					break;
				}
				
				if ((i == 0) && (grid[_iRow,i] != CrossWordGenerator.EMPTYCHARACTER))
				{
					auxICol = i;
				}
			}
			_iColStart = auxICol;
			
			// Gets word to check in list
			string word = "";
			for (int i = auxICol; i< CrossWordGenerator.NCols; i++) 
			{
				if (grid[_iRow,i] != CrossWordGenerator.EMPTYCHARACTER)
				{
					word += grid[_iRow,i];
				}else
				{
					break;
				}
			}
			
			if (word != "")
			{
				// Find word in list words
				for (int i= 0; i<this.listSingleWords.Count; i++)
				{
					// Get the original word without index word
					string auxWord = this.listSingleWords[i].Answer;
					if (auxWord == word)
					{
						_question = this.listSingleWords[i].Questions;
						_word = this.listSingleWords[i].Answer;
						return true;
					}
				}
			}		
			return false;	
		}

		/// <summary>
		/// Tries to get the question in down direction if there is an actual word according to the given coord
		/// </summary>
		/// <returns>Returns wheter it was succes or not</returns>
		/// <param name="_iRow">Row to check</param>
		/// <param name="_iCol">Col to check</param>
		/// <param name="_iRowStart">Output row for the word</param>
		/// <param name="_iColStart">Output col for the word</param>
		/// <param name="_question">Question for that word</param>
		/// <param name="_word">Word for that question</param>
		public bool TryGetDownQuestion(int _iRow, int _iCol, out  int _iRowStart, out int _iColStart, out string _question,out string _word)
		{
			// Piece
			_iColStart = _iCol;
			_question = "";
			_word = "";
			
			// Find first down position
			int auxIRow =_iRow;
			
			for (int i = auxIRow; i>=0; i--) 
			{
				if (grid[i,_iCol] == CrossWordGenerator.EMPTYCHARACTER)
				{
					auxIRow = i+1;
					break;
				}
				
				if ((i == 0) && (grid[i,_iCol] != CrossWordGenerator.EMPTYCHARACTER))
				{
					auxIRow = i;
				}
			}
			
			_iRowStart = auxIRow;
			// Gets word to check in list
			string word = "";
			for (int i = auxIRow; i< CrossWordGenerator.NRows; i++) 
			{
				if (grid[i,_iCol] != CrossWordGenerator.EMPTYCHARACTER)
				{
					word += grid[i,_iCol];
				}else
				{
					break;
				}
			}
			
			if (word != "")
			{
				// Find word in list words
				for (int i= 0; i<this.listSingleWords.Count; i++)
				{
					// Get the original word without index word
					string auxWord = this.listSingleWords[i].Answer;
					if (auxWord == word)
					{
						_question = this.listSingleWords[i].Questions;
						_word = this.listSingleWords[i].Answer;
						return true;
					}
				}
			}		
			return false;	
		}


		#endregion HANDLE_CROSS_WORD
	}
}
