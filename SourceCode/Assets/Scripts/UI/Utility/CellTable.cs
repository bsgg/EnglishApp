using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace EnglishApp
{
    public class CellTable : MonoBehaviour
    {
        public RectTransform RectTransfromCell;
        public Text LetterCellLabel;
        public Image BackgroundCell;

        public Color32 BackgroundCellColor
        {
            set { this.BackgroundCell.color = value; }
            get { return this.BackgroundCell.color; }
        }

        public Color32 LetterCellColor
        {
            set { this.LetterCellLabel.color = value; }
            get { return this.LetterCellLabel.color; }
        }

        public string LetterCell
        {
            set { this.LetterCellLabel.text = value; }
            get { return this.LetterCellLabel.text; }
        }

    }
}
