using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace EnglishApp
{
    public class Table : MonoBehaviour
    {

        [Header("Prefab Cell to add")]
        public GameObject PrefabCell;

        public RectTransform RectTransformTable;

        public float OffsetX = 0.02f;

        //public float HeightTotalContent = 500.0f;
        public float OffsetY = 0.02f;

        public Color32 ColorHeaderRow = new Color32(255, 255, 255, 255);
        public Color32 ColorTextHeaderRow = new Color32(0, 0, 0, 255);
        public Color32 ColorRows = new Color32(255, 255, 255, 255);
        public Color32 ColorTextRows = new Color32(0, 0, 0, 255);

        /// <summary>
        /// List of objects in content panel
        /// </summary>
        private List<GameObject> listElements;

        // TODO: TYPE CELL
        public void InitTable(string[,] _data, int _numberRows, int _numberCols, float _heightTable, bool _hasHeaderRow)
        {
            // Remove current list of elements
            if (listElements != null)
            {
                for (int i = 0; i < listElements.Count; i++)
                {
                    GameObject.Destroy(listElements[i]);
                }
            }
            listElements = new List<GameObject>();

            // Instanciate all elements and positione
            float xPos = 0.0f;
            float auxWidthOffset = 1.0f - (OffsetX * (float)(_numberCols - 1));
            float width = (float)(auxWidthOffset / (float)_numberCols);
            RectTransformTable.sizeDelta = new Vector2(0.0f, _heightTable);
            //int numberRows = 10;
            float auxHeight = 1.0f - (OffsetY * (float)(_numberRows + 1));
            float heightCell = (float)(auxHeight / (float)_numberRows);
            float yPos = 1.0f - OffsetY;

            for (int iRow = 0; iRow < _numberRows; iRow++)
            {
                for (int iCol = 0; iCol < _numberCols; iCol++)
                {
                    GameObject element = Instantiate(PrefabCell) as GameObject;
                    listElements.Add(element);

                    RectTransform rectElement = element.GetComponent<RectTransform>();
                    element.transform.SetParent(RectTransformTable.transform);

                    rectElement.localScale = Vector3.one;
                    rectElement.sizeDelta = Vector2.zero;
                    rectElement.anchoredPosition = Vector2.zero;

                    // Get the widht of the transform
                    rectElement.anchorMin = new Vector2(xPos, yPos - heightCell);
                    rectElement.anchorMax = new Vector2(xPos + width, yPos);
                    xPos += (width + OffsetX);

                    // Setup Cell component
                    // TODO: CELL IMAGE
                    CellTable cell = rectElement.GetComponent<CellTable>();
                    if (_hasHeaderRow)
                    {
                        if (iRow == 0)
                        {
                            cell.LetterCellColor = ColorTextHeaderRow;
                            cell.BackgroundCellColor = ColorHeaderRow;
                        }
                        else
                        {
                            cell.LetterCellColor = ColorTextRows;
                            cell.BackgroundCellColor = ColorRows;
                        }

                    }
                    else
                    {
                        cell.LetterCellColor = ColorTextRows;
                        cell.BackgroundCellColor = ColorRows;
                    }
                    cell.LetterCell = _data[iRow, iCol];

                }
                xPos = 0.0f;
                yPos -= (heightCell + OffsetY);
            }
        }
    }
}
