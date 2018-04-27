using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO
{
    class CommentsDTO
    {
        public int Id { get; set; }

        public string CommentText { get; set; }

        public DateTime CreationDateTime { get; set; }

        public int ExerciseId { get; set; }

        public int Rating { get; set; }
    }
}
