using System.Collections.Generic;

public class Vocabulary
{
	private string vocabularyWord;
	public string VocabularyWord
	{
		set { this.vocabularyWord = value; }
		get { return this.vocabularyWord; }
	}

	private List<string> meanings;
	public List<string> Meanings
	{
		set { this.meanings = value; }
		get { return this.meanings; }
	}

	private List<string> englishExamples;
	public List<string> EnglishExamples
	{
		set { this.englishExamples = value; }
		get { return this.englishExamples; }
	}

	private List<string> spanishExamples;
	public List<string> SpanishExamples
	{
		set { this.spanishExamples = value; }
		get { return this.spanishExamples; }
	}
	
	public Vocabulary() {}

}
