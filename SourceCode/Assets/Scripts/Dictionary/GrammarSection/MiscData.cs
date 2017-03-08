using System.Collections.Generic;

public class MiscData
{
	private List<Grammar> miscGrammar;
	public List<Grammar> MiscGrammar
	{
		get { return this.miscGrammar; }
		set { this.miscGrammar = value;}
	}

	public MiscData()
	{
		miscGrammar = new List<Grammar>();
	}
}
