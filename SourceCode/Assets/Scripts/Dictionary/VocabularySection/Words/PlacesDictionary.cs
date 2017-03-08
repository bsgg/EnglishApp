using System.Collections.Generic;

public class PlacesDictionary
{
	private List<Word> places;
	public List<Word> Places
	{
		get { return this.places; }
		set { this.places = value;}
	}

	public PlacesDictionary()
	{
		places = new List<Word>();
	}
}
