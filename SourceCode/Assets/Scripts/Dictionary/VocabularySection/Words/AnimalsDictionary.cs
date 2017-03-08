using System.Collections.Generic;

public class AnimalsDictionary
{
	private List<Word> animals;
	public List<Word> Animals
	{
		get { return this.animals; }
		set { this.animals = value;}
	}

	public AnimalsDictionary()
	{
		animals = new List<Word>();
	}
}
