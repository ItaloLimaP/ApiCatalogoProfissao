using System;

namespace ApiCatalogoProfissao.Entities
{
    public class Profissao
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Empresa { get; set; }
        public double Preco { get; set; }
    }
}
