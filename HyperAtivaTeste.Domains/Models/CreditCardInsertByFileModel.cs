namespace HyperAtivaTeste.Domains.Models
{
    public class CreditCardInsertByFileModel
    {
        public CreditCardInsertByFileModel()
            => this.CreditCards = new List<CreditCardFileModel>();

        public Guid CreatedUserId { get; set; }
        public string Name { get; set; }
        public DateTime? DateFile { get; set; }
        public string Lote { get; set; }
        public int? TotalRegisters { get; set; }
        public List<CreditCardFileModel> CreditCards { get; set; }
    }
}
