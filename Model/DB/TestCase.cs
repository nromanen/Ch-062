namespace Model.DB
{
    public class TestCase
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string UserId { get; set; }
        public string InputData { get; set; }
        public string OutputData { get; set; }

        public virtual Exercise Exercise { get; set; }
        public virtual User User { get; set; }
    }
}
