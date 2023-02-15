namespace HyperAtivaTeste.API.ViewModels
{
    public class CreditCardInsertByFileViewModel
    {
        public CreditCardInsertByFileViewModel()
            => this.CreditCards = new List<CreditCardFileViewModel>();

        public Guid CreatedUserId { get; set; }
        public string Name { get; set; }
        public DateTime? DateFile { get; set; }
        public string Lote { get; set; }
        public int? TotalRegisters { get; set; }
        public List<CreditCardFileViewModel> CreditCards { get; set; }
    }
}
