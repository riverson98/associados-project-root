using DocAssociados.Domain.Entities;
using DocAssociados.Domain.Interfaces;
using DocAssociados.Infra.Data.Context;
using DocAssociados.Service.Domain.EntitiesSummary;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DocAssociados.Infra.Data.Repository;

public class AssociadoRepositorio : Repositorio<Associado>, IAssociadoRepositorio
{
    public AssociadoRepositorio(AppDbContext context) : base(context)
    {
    }

    public Associado AdicionaAssociadoComEndereco(Associado associado)
    {
        if (associado == null || associado.Endereco == null)
            throw new ArgumentNullException("Associado ou Endereço não podem ser nulos.");

        _context.Associado.Add(associado);

        associado.Endereco.AssociadoId = associado.Id;

        _context.Endereco.Add(associado.Endereco);

        return associado;
    }

    public async Task<AssociadoResumido> AtualizaParcialmenteAsync(AssociadoResumido entidadeResumida)
    {
        var entidade = await _context.Associado.FirstOrDefaultAsync(it => it.Id.Equals(entidadeResumida.Id));

        if (entidade == null)
            throw new ArgumentNullException("Nenhum usuario encontrado");

        entidade.Atualiza(entidade.Id, entidadeResumida.Nome, entidade.Email, entidadeResumida.DataDeNascimento,
            entidadeResumida.Genero, entidade.Funcao, entidade.Status, entidade.Cpf, entidade.CpfUploadUrl,
            entidade.CodigoRepresentante, entidade.CodigoRepresentanteSuperior, entidade.TermoDeAdessaoUploadUrl,
            entidade.FichaAssociacaoUploadUrl, entidadeResumida.UrlFotoDePerfil, entidade.RequerimentoJudicialUrl);

        return entidadeResumida;
    }

    public async Task<Associado> BuscaAssociadoComEndereco(Expression<Func<Associado, bool>> predicate)
    {
        return await _context.Set<Associado>()
                             .Include(associado => associado.Endereco)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(predicate);
    }

    public async Task<AssociadoResumido> BuscaAssociadoResumidoAsync(Guid id)
    {
        return await _context.Set<Associado>()
                             .Where(it => it.Id.Equals(id))
                             .Select(it => new AssociadoResumido
                             {
                                 Id = it.Id,
                                 Nome = it.Nome,
                                 DataDeNascimento = it.DataDeNascimento,
                                 Email = it.Email,
                                 Genero = it.Genero,
                                 CodigoAssociado = it.CodigoAssociado,
                                 Funcao = it.Funcao,
                                 UrlFotoDePerfil = it.FotoDePerfilUrl
                             })
                             .FirstOrDefaultAsync();
    }

    public async Task<UrlsDocumentos> BuscaUrlsDoAssociadoAsync(Guid id)
    {
        return await _context.Set<Associado>()
        .Where(a => a.Id == id)
        .Select(a => new UrlsDocumentos(a.Endereco.ComprovanteDeResidenciaUpload, 
                                        a.RequerimentoJudicialUrl, a.FichaAssociacaoUploadUrl, 
                                        a.TermoDeAdessaoUploadUrl, a.CpfUploadUrl))
        .FirstOrDefaultAsync();
    }

    public async Task AtualizaUrlsDoAssociadoAsync(Guid id, UrlsDocumentos urls)
    {
        var associado = await _context.Associado
            .Where(a => a.Id == id)
            .Include(a => a.Endereco)
            .FirstOrDefaultAsync();

        ArgumentNullException.ThrowIfNull("Nenhum associado encontrado com o ID fornecido.");


        associado?.AtualizaUrls(urls.UrlDoRequerimento, urls.FichaAssociacaoUploadUrl,
            urls.TermoAdesaoUploadUrl, urls.CpfUploadUrl);

        associado?.Endereco.AtualizaUrl(urls.ComprovanteDeResidenciaUpload);
    }
}
