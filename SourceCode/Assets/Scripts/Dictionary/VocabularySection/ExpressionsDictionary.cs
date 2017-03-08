using System.Collections.Generic;

public class ExpressionsDictionary
{
	private List<Vocabulary> expressions;
	public List<Vocabulary> Expressions
	{
		get { return this.expressions; }
		set { this.expressions = value;}
	}

	public ExpressionsDictionary()
	{
		expressions = new List<Vocabulary>();
	}
}
