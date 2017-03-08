using System.Collections.Generic;

public class PhrasalVerbDictionary
{
	private List<Vocabulary> phrasalVerbs;
	public List<Vocabulary> PhrasalVerbs
	{
		get { return this.phrasalVerbs; }
		set { this.phrasalVerbs = value;}
	}

	public PhrasalVerbDictionary()
	{
		phrasalVerbs = new List<Vocabulary>();
	}
}
