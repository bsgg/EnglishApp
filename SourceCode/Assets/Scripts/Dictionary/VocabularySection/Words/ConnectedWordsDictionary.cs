using System.Collections.Generic;

public class ConnectedWordsDictionary
{
	private List<Word> connectedWords;
	public List<Word> ConnectedWords
	{
		get { return this.connectedWords; }
		set { this.connectedWords = value;}
	}

	public ConnectedWordsDictionary()
	{
		connectedWords = new List<Word>();
	}
}
