using System.Collections.Generic;

public class BodyPartsDictionary
{
	private List<Word> bodyParts;
	public List<Word> BodyParts
	{
		get { return this.bodyParts; }
		set { this.bodyParts = value;}
	}

	public BodyPartsDictionary()
	{
		bodyParts = new List<Word>();
	}
}
