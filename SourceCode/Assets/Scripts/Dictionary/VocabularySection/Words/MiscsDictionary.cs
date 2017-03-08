using System.Collections.Generic;

public class MiscsDictionary
{
	private List<Word> miscs;
	public List<Word> Miscs
	{
		get { return this.miscs; }
		set { this.miscs = value;}
	}

	public MiscsDictionary()
	{
		miscs = new List<Word>();
	}
}
