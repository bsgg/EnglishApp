using System.Collections.Generic;

public class AgilityDictionary
{
	private List<Vocabulary> agilityExpressions;
	public List<Vocabulary> AgilityExpressions
	{
		get { return this.agilityExpressions; }
		set { this.agilityExpressions = value;}
	}

	public AgilityDictionary()
	{
		agilityExpressions = new List<Vocabulary>();
	}
}
