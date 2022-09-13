using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnesCentar.Models
{
    public class Comment
    {
        public string Username { get; set; }
        public string FitnessCenterName { get; set; }
        public string CommentText { get; set; }
        public int Review { get; set; }
        public string Visibility { get; set; } //odbijen/prihvacen
        public Comment()
        {

        }

        public Comment(string username, string fitnessCenterName, string commentText, int review, string visibility)
        {
            Username = username;
            FitnessCenterName = fitnessCenterName;
            CommentText = commentText;
            Review = review;
            Visibility = visibility;
        }
    }
}