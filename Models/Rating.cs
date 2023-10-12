using System;
namespace QA_Feedback.Models
{
	public class Rating
	{
        public int Id { set; get; }
        public int Question { set; get; }
        public string Description { set; get; }
        public int Difficulty_Stars { set; get; }
        public int Pyramidality_Stars { set; get; }
        public int Accuracy_Stars { set; get; }
    }
}

