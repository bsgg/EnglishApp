﻿using System.Collections.Generic;

public class ComparativeGrammarData
{
	private List<Grammar> comparativesGrammar;
	public List<Grammar> ComparativesGrammar
	{
		get { return this.comparativesGrammar; }
		set { this.comparativesGrammar = value;}
	}

	public ComparativeGrammarData()
	{
		ComparativesGrammar = new List<Grammar>();
	}
}
