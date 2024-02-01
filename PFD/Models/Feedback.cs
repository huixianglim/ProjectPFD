using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PFD.Models
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "FeedbackID")]

        public int FeedbackID { get; set; }

        [Display(Name = "AIFeedback")]
        [StringLength(500, ErrorMessage = "Feedback cannot be longer than 500 characters!")]

        public string ChatBotFeedback{ get; set; }

        [Display(Name = "GestureFeedback")]
        [StringLength(500, ErrorMessage = "Feedback cannot be longer than 500 characters!")]

        public string GestureFeedback { get; set; }

        [Display(Name = "VoiceFeedback")]
        [StringLength(500, ErrorMessage = "Feedback cannot be longer than 500 characters!")]

        public string VoiceFeedback { get; set; }

        [Display(Name = "GestureScore")]
        public int GestureScore { get; set; }


        [Display(Name = "VoiceRecogScore")]
        public int VoiceScore { get; set; }


        [Display(Name = "ChatBOTScore")]
        public int ChatBotScore { get; set; }


    }
}
