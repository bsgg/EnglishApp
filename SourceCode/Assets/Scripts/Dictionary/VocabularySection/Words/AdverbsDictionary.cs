using System.Collections.Generic;

public class AdverbsDictionary
{
	private List<Word> adverbs;
	public List<Word> Adverbs
	{
		get { return this.adverbs; }
		set { this.adverbs = value;}
	}

	public AdverbsDictionary()
	{
		adverbs = new List<Word>();
	}
}
