using System.Collections.Generic;

public class ObjectsDictionary
{
	private List<Word> objects;
	public List<Word> Objects
	{
		get { return this.objects; }
		set { this.objects = value;}
	}

	public ObjectsDictionary()
	{
		objects = new List<Word>();
	}
}
