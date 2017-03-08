using System.Collections.Generic;

public class ModalsData
{
	private List<Grammar> modalsGrammar;
	public List<Grammar> ModalsGrammar
	{
		get { return this.modalsGrammar; }
		set { this.modalsGrammar = value;}
	}

	public ModalsData()
	{
		modalsGrammar = new List<Grammar>();
	}
}
