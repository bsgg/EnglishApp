using System.Collections.Generic;

public class AdjectivesDictionary
{
	private List<Word> adjectives;
	public List<Word> Adjectives
	{
		get { return this.adjectives; }
		set { this.adjectives = value;}
	}

	public AdjectivesDictionary()
	{
		adjectives = new List<Word>();
	}
}
