using System.Collections.Generic;

public class FoodDictionary
{
	private List<Word> food;
	public List<Word> Food
	{
		get { return this.food; }
		set { this.food = value;}
	}

	public FoodDictionary()
	{
		food = new List<Word>();
	}
}
