using DocAssociados.Domain.Validation;

namespace DocAssociados.Service.Domain.EntitiesSummary;

public class UrlsDocumentos
{
    public string ComprovanteDeResidenciaUpload { get; private set; }
    public string UrlDoRequerimento { get; private set; }
    public string FichaAssociacaoUploadUrl { get; private set; }
    public string TermoAdesaoUploadUrl { get; private set; }
    public string CpfUploadUrl { get; private set; }


    public UrlsDocumentos(string comprovanteDeResidenciaUpload, string urlDoRequerimento, string fichaAssociacaoUploadUrl, string termoAdesaoUploadUrl, string cpfUploadUrl)
    {
        ValidaDominio(comprovanteDeResidenciaUpload, urlDoRequerimento, fichaAssociacaoUploadUrl, termoAdesaoUploadUrl, cpfUploadUrl);

        ComprovanteDeResidenciaUpload = comprovanteDeResidenciaUpload;
        UrlDoRequerimento = urlDoRequerimento;
        FichaAssociacaoUploadUrl = fichaAssociacaoUploadUrl;
        TermoAdesaoUploadUrl = termoAdesaoUploadUrl;
        CpfUploadUrl = cpfUploadUrl;
    }

    public void Atualiza(string comprovanteDeResidenciaUpload, string urlDoRequerimento, string fichaAssociacaoUploadUrl, string termoAdesaoUploadUrl, string cpfUploadUrl)
    {
        ValidaDominio(comprovanteDeResidenciaUpload, urlDoRequerimento, fichaAssociacaoUploadUrl, termoAdesaoUploadUrl, cpfUploadUrl);

        ComprovanteDeResidenciaUpload = comprovanteDeResidenciaUpload;
        UrlDoRequerimento = urlDoRequerimento;
        FichaAssociacaoUploadUrl = fichaAssociacaoUploadUrl;
        TermoAdesaoUploadUrl = termoAdesaoUploadUrl;
        CpfUploadUrl = cpfUploadUrl;
    }

    private void ValidaDominio(string comprovanteDeResidenciaUpload, string urlDoRequerimento, string fichaAssociacaoUploadUrl,
        string termoAdesaoUploadUrl, string cpfUploadUrl)
    {
        ValidacaoDeDominioException.When(string.IsNullOrEmpty(comprovanteDeResidenciaUpload),
            "O link do comprovante de residencia é obrigatório");

        ValidacaoDeDominioException.When(string.IsNullOrEmpty(urlDoRequerimento),
            "O link do requerimento é obrigatório");

        ValidacaoDeDominioException.When(string.IsNullOrEmpty(fichaAssociacaoUploadUrl),
            "O link da ficha é obrigatório");

        ValidacaoDeDominioException.When(string.IsNullOrEmpty(termoAdesaoUploadUrl),
            "O link do termo é obrigatório");

        ValidacaoDeDominioException.When(string.IsNullOrEmpty(cpfUploadUrl),
            "O link do cpf é obrigatório");
    }
}
