namespace Model.DB.Code
{
    public class CodeResult
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string Result { get; set; }

        public virtual UserCode Code { get; set; }
    }
}
