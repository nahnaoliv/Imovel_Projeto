using System.ComponentModel;

namespace ImovelWEB.Models
{
    public class Imovel
    {
        public int Id { get; set; }
        [DisplayName("Titulo do Imóvel")]
        public string Nome { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Preço do Imóvel")]
        public decimal Preco { get; set; }
        [DisplayName("CEP")]
        public string Cep { get; set; }
        [DisplayName("Logradouro")]
        public string Rua { get; set; }
        [DisplayName("Número")]
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        [DisplayName("UF")]
        public string Estado { get; set; }
        [DisplayName("Referência")]
        public string Referencia { get; set; }
        public bool Alugado { get; set; }
    }
}
