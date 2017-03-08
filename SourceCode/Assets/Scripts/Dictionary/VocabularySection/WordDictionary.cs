using System.Collections.Generic;

public class WordDictionary
{
	private List<Word> words;
	public List<Word> Words
	{
		get { return this.words; }
		set { this.words = value;}
	}

	public WordDictionary()
	{
		words = new List<Word>();
	}
}
