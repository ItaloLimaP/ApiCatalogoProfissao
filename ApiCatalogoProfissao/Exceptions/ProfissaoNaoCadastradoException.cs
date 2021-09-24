using System;

namespace ApiCatalogoProfissao.Exceptions
{
    public class ProfissaoNaoCadastradoException : Exception
    {
        public ProfissaoNaoCadastradoException()
            : base("Esta profissão não está cadastrada")
        { }
    }
}
