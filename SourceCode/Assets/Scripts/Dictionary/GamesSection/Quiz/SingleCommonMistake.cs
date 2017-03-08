using System.Collections.Generic;

public class SingleCommonMistake
{
    private string m_Question;
    public string Question
    {
        set { this.m_Question = value; }
        get { return this.m_Question; }
    }


    private List<string> m_Answers;
	public List<string> Answers
    {
		set { this.m_Answers = value; }
		get { return this.m_Answers; }
	}
	public SingleCommonMistake() {}

}
