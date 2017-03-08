using System.Collections.Generic;

public class Grammar
{
	private string title;
	public string Title
	{
		set { this.title = value; }
		get { return this.title; }
	}

	private string description;
	public string Description
	{
		set { this.description = value; }
		get { return this.description; }
	}

	private List<string> rules;
	public List<string> Rules
	{
		set { this.rules = value; }
		get { return this.rules; }
	}

	private string extraInfo;
	public string ExtraInfo
	{
		set { this.extraInfo = value; }
		get { return this.extraInfo; }
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
	
	public Grammar() {}

}
