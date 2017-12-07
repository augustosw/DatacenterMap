using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace AutDemo.WebApi
{
    public class ErrosGlobaisFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string mensagem = "Ocorreu um erro ao processar sua requisição.";

            if (context.Exception is System.Data.SqlClient.SqlException)
            {
                var sqlException = context.Exception as System.Data.SqlClient.SqlException;
                switch (sqlException.Errors[0].Number)
                {
                    case 547:
                        mensagem = "Registro em uso por outra tabela.";
                        break;
                    case 2601:
                        mensagem = "Já existe registro com este nome.";
                        break;
                    case 8115:
                        mensagem = "Valor da coluna excedeu o máximo permitido.";
                        break;
                    case 8152:
                        mensagem = "Texto da coluna excedeu o máximo permitido.";
                        break;
                    default:
                        mensagem = "Erro ao executar operação no banco de dados.";
                        break;
                }
            }

            var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        mensagens = new string[] { mensagem }
                    });

            context.Response = response;
        }
    }
}