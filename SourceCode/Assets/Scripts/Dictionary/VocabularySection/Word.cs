using System.Collections.Generic;

public class Word: Vocabulary
{
	private string category;
	public string Category
	{
		set { this.category = value; }
		get { return this.category; }
	}

	private string pronunciation;
	public string Pronunciation
	{
		set { this.pronunciation = value; }
		get { return this.pronunciation; }
	}
	
	public Word() {}

}
