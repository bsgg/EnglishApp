using System.Collections.Generic;

public class ActionsDictionary
{
	private List<Word> actions;
	public List<Word> Actions
	{
		get { return this.actions; }
		set { this.actions = value;}
	}

	public ActionsDictionary()
	{
		actions = new List<Word>();
	}
}
