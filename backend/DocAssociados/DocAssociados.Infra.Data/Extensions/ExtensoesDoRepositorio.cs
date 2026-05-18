using DocAssociados.Domain.Entities;
using System.Linq.Expressions;

namespace DocAssociados.Service.Infra.Data.Extensions;

public static class ExtensoesDoRepositorio
{
    public static Expression<Func<Associado, bool>> FiltroDinamico(string filtro)
    {
        return x =>
            x.Nome.Contains(filtro) ||
            x.Email.Contains(filtro);
    }
}
