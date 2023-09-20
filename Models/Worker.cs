using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;

namespace ResolverQuestao.Models
{
    public class Worker
    {
        //construtor da classe
        public Worker()
        {
        }
        public async Task<string> GerarGPTAsync(string entrada)

        {


            string key = "5ab8e3af2fd14070af933c3bc360b07a";

            Azure.AI.OpenAI.OpenAIClient client = new(new Uri("https://codexteste1.openai.azure.com/"), new AzureKeyCredential(key));

            var pergunta = entrada;

            var resposta = " ";


            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
    {
        new ChatMessage(ChatRole.System, @"you want me to help you study for the college exam, providing a question based on a text that you send me. You want the question to be multiple choice, with four alternatives, and have an explanation of the correct answer. You also want me to put my answer in JSON format, following the model that you indicate. Model The answer should follow the following JSON model
#TEXT IN BRAZILIAN PORTUGUESE#
{
  ""ExercicioId"": {0},
  ""Enunciado"": {eu crio uma pergunta},
  ""Titulo"": {eu crio um título},
  ""Tipo"": {eu crio um tipo},
  ""Resposta"": {eu crio a resposta completa},
  ""Alternativas"": [
    {
      ""AlternativaId"": {eu crio um número},
      ""Texto"": {eu crio uma alternativa},
      ""ExercicioId"": {eu uso o mesmo número do ExercicioId}
    },
    {
      ""AlternativaId"": {eu crio outro número},
      ""Texto"": {eu crio outra alternativa},
      ""ExercicioId"": {eu uso o mesmo número do ExercicioId}
    },
    {
      ""AlternativaId"": {eu crio outro número},
      ""Texto"": {eu crio outra alternativa},
      ""ExercicioId"": {eu uso o mesmo número do ExercicioId}
    },
    {
      ""AlternativaId"": {eu crio outro número},
      ""Texto"": {eu crio outra alternativa},
      ""ExercicioId"": {eu uso o mesmo número do ExercicioId}
    }
  ],
  ""Explicacao"": {eu crio uma explicação},
  ""MaterialSuporte"": null,
  ""ListaExercicios"": [],
  ""UsuarioId"": null,
  ""Materia"": null
}
Orientações
The question must be related to the base text that you send me. The explanation should be brief and clear, using facts from the base text. The alternatives must be plausible, but only one must be correct. I must put everything in JSON format to facilitate copying and inserting into your system
.Exemplo: ####USUARIO###
Texto base:
Uma base de dados sem redundância é aquela que não armazena mais informação do que o necessário. Toda vez que colocamos na base de dados algum dado que se retirado não geraria perda de informação, estamos adicionando redundância à base. Um exemplo de redundância é armazenar o mesmo dado em duas tabelas. Outro exemplo é quando o valor de certa coluna pode ser derivado a partir de cálculos envolvendo outras colunas.
###FIM DO DO TEXTO BASE####
##ASSISTENTE##:
{
  ""ExercicioId"": 1,
  ""Enunciado"": ""Qual é a vantagem de se ter uma base de dados sem redundância?"",
  ""Titulo"": ""Base de dados sem redundância"",
  ""Tipo"": ""Múltipla escolha"",
  ""Resposta"": ""Evitar anomalias nos dados e economizar espaço de armazenamento."",
  ""Alternativas"": [
    {
      ""AlternativaId"": 1,
      ""Texto"": ""Evitar anomalias nos dados e economizar espaço de armazenamento."",
      ""ExercicioId"": 1
    },
    {
      ""AlternativaId"": 2,
      ""Texto"": ""Facilitar a inserção e a atualização de dados."",
      ""ExercicioId"": 1
    },
    {
      ""AlternativaId"": 3,
      ""Texto"": ""Aumentar a velocidade de consulta e de processamento."",
      ""ExercicioId"": 1
    },
    {
      ""AlternativaId"": 4,
      ""Texto"": ""Melhorar a segurança e a integridade dos dados."",
      ""ExercicioId"": 1
    }
  ],
  ""Explicacao"": ""Uma base de dados sem redundância é aquela que não armazena mais informação do que o necessário. Isso evita que haja erros ou inconsistências nos dados, chamados de anomalias, que podem ocorrer quando há duplicidade de informação. Além disso, uma base de dados sem redundância também economiza espaço de armazenamento, pois não guarda dados desnecessários."",
  ""MaterialSuporte"": null,
  ""ListaExercicios"": [],
  ""UsuarioId"": null,
  ""Materia"": null
}
###FIM EXEMPLO###
####TEXTO BASE:



####FIM DO TEXTO BASE

##UMA POSSIVEL QUESTAO PARA ESSE TEXTO BASE É:###
###
The question must be related to the base text that you send me. The explanation should be brief and clear, using facts from the base text. The alternatives must be plausible, but only one must be correct. I must put everything in JSON format to facilitate copying and inserting into your system.
you want me to help you study for the college exam, providing a question based on a text that you send me. You want the question to be multiple choice, with four alternatives, and have an explanation of the correct answer. You also want me to put my answer in JSON format, following the model that you indicate. Model The answer should follow the following JSON model:
###
##TEXT IN BRAZILIAN PORTUGUESE##
###
####YOU REMEMBER THE MODEL? SINCE YOU UNDERSTOOD, MAKE THE QUESTION BASED ON THE TEXT BASE, I TRUST THAT YOU UNDERSTOOD, YOU CAN MAKE THE QUESTION FOLLOWING THE MODEL, NOT TEXT BASE IN ANWSER####
"),
        new ChatMessage(ChatRole.Assistant, "Sim, irei enviar a resposta em formato JSON, Seguindo o modelo que você indicar. Modelo A resposta deve seguir o seguinte modelo JSON:"),
        new ChatMessage(ChatRole.User, pergunta),
    },
                MaxTokens = 1000
            };

            Response<StreamingChatCompletions> response = await client.GetChatCompletionsStreamingAsync(
                deploymentOrModelName: "TesteCodex",
                chatCompletionsOptions);
            using StreamingChatCompletions streamingChatCompletions = response.Value;



            await foreach (StreamingChatChoice choice in streamingChatCompletions.GetChoicesStreaming())
            {
                await foreach (ChatMessage message in choice.GetMessageStreaming())
                {
                    //Console.Write(message.Content);

                    //concatenar a resposta do assistente com a pergunta do usuário
                    resposta = resposta + message.Content;

                  
                }
            }
            return resposta;
        }
    }
}