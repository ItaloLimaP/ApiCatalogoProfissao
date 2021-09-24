using System;

namespace ApiCatalogoProfissao.ViewModel
{
    public class ProfissaoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public double Preco { get; set; }
    }
}
