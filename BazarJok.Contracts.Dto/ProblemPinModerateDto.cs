using System;

namespace UrbanDream.Contracts.Dtos
{
    public class ProblemPinModerateDto
    {
        public bool State { get; set; }
        public string AnswerType { get; set; }
        public string Answer { get; set; }
        public Guid ProblemPin { get; set; }
    }
}