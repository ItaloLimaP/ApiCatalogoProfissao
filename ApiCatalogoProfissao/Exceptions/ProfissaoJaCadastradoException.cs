using System;

namespace ApiCatalogoProfissao.Exceptions
{
    public class ProfissaoJaCadastradoException : Exception
    {
        public ProfissaoJaCadastradoException()
            : base("Esta profissão já está cadastrada")
        { }
    }
}
