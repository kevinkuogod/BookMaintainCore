using System;
using System.Collections.Generic;

namespace bookMaintain.Model.Models
{
    public partial class Vocabulary
    {
        public long Id { get; set; }
        public string EnglishName { get; set; } = null!;
        public string ChineseName { get; set; } = null!;
        public int? Language { get; set; }
        public int? ImageId { get; set; }
        public string? PartOfSpeech { get; set; }
        public string? PartOfSpeechDetial { get; set; }
        public string? ExampleSentences { get; set; }
        public string? ExampleSentencesTranslation { get; set; }
        public int Chances { get; set; }
        public string? Provenance { get; set; }
        public string Editor { get; set; } = null!;
        public string? KenyonAndKnott { get; set; }
        public string? ProfessionalField { get; set; }
        public string? ExtraMatters { get; set; }
        public string? Tense { get; set; }
        public string? Remark { get; set; }
        public string? PerfectTense { get; set; }
        public string? Analyze { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
