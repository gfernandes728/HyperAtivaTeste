namespace HyperAtivaTeste.Domains.Models
{
    public class LogModel
    {
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string Result { get; set; }
    }
}
