namespace Model.DTO
{
    public class TestCaseDTO
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public string UserId { get; set; }
        public string InputData { get; set; }
        public string OutputData { get; set; }

        public virtual ExerciseDTO Exercise { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
