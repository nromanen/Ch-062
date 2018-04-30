namespace Model.DTO
{
    public class TestCaseDTO
    {
        public int Id { get; set; }
        public int ExerciseDTOId { get; set; }
        public string UserDTOId { get; set; }
        public string InputData { get; set; }
        public string OutputData { get; set; }

        public virtual ExerciseDTO Exercise { get; set; }
        public virtual UserDTO User { get; set; }
    }
}
