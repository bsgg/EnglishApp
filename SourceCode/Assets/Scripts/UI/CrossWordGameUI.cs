using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using CrossWordUtil;

namespace EnglishApp
{
    public class CrossWordGameUI : BaseUI
    {
        public delegate void OnPieceButtonPress(int iRow, int iCol);

        [Header("CrossWordGameUI")]
        [SerializeField] private RectTransform      m_ContentCrossWord;
        [SerializeField] private List<TextButton>   m_Letters;
        public int NumberOfLetters
        {
            get { return m_Letters.Count; }
           
        }
        [SerializeField] private Text               m_Question;
        public string Question
        {
            get { return m_Question.text; }
            set { m_Question.text = value; }
        }
        [SerializeField] private TextButton         m_HintButton;
        public TextButton HintButton
        {
            get { return m_HintButton; }
            set { m_HintButton = value; }
        }

        private CellCrossWord[,]                    m_GridBoard;


        public CellCrossWord GetCell(int _iRow, int _iCol)
        {
            if ((_iRow <= -1) || (_iRow >= CrossWordGenerator.NRows) ||
                (_iCol <= -1) || (_iCol >= CrossWordGenerator.NCols))
            {
                return null;
            }
            return m_GridBoard[_iRow, _iCol];
        }

        public void InitTableboard()
        {
            // Fill table board with pieces
            m_GridBoard = new CellCrossWord[CrossWordGenerator.NRows, CrossWordGenerator.NCols];
            int indexPieces = 0;
            for (int iRow = 0; iRow < CrossWordGenerator.NRows; iRow++)
            {
                for (int iCol = 0; iCol < CrossWordGenerator.NCols; iCol++)
                {
                    CellCrossWord piece = m_ContentCrossWord.GetChild(indexPieces).GetComponent<CellCrossWord>();

                    m_GridBoard[iRow, iCol] = piece;
                    indexPieces++;
                }
            }
        }
        /// <summary>
        /// Sets the callback for each piece in table
        /// </summary>
        /// <param name="iRow">I row.</param>
        /// <param name="iCol">I col.</param>
        /// <param name="callbackPiece">Callback piece.</param>
        public void SetCallbackPiece(int iRow, int iCol, OnPieceButtonPress callbackPiece)
        {
            m_GridBoard[iRow, iCol].ButtonComponent.onClick.AddListener(() => { callbackPiece(iRow, iCol); });
        }

        /// <summary>
        /// Sets each letter in buttons by a list given
        /// </summary>
        /// <param name="_letters">list of letters.</param>
        public void SetLetterButtons(List<char> _letters)
        {
            for (int i = 0; i < _letters.Count; i++)
            {
                if (i < m_Letters.Count)
                {
                    m_Letters[i].Text = _letters[i].ToString();
                }
            }
        }
        public string GetLetter(int index)
        {
            if (index < m_Letters.Count)
            {
                return m_Letters[index].Text;
            }
            return "";
        }

        /// <summary>
        /// Sets the color of a letter in tableboard
        /// </summary>
        /// <param name="_iRow">_i row.</param>
        /// <param name="_iCol">_i col.</param>
        /// <param name="_color">_color.</param>
        public void SetLetterColor(int _iRow, int _iCol, Color32 _color)
        {
            if ((_iRow > -1) && (_iRow < CrossWordGenerator.NRows) &&
                (_iCol > -1) && (_iCol < CrossWordGenerator.NCols))
            {
                m_GridBoard[_iRow, _iCol].ColorBackground = _color;
            }
        }

        public void SetLetter(int iRow, int iCol, string letter)
        {
            if ((iRow > -1) && (iRow < CrossWordGenerator.NRows) &&
                (iCol > -1) && (iCol < CrossWordGenerator.NCols))
            {
                m_GridBoard[iRow, iCol].Letter = letter;
            }
        }
    }
}
