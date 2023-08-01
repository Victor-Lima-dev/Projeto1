using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ResolverQuestao.Models
{
    public class Firebase
    {
        public string ApiKey { get; set; }
        public string AuthDomain { get; set; }
        public string ProjectId { get; set; }
        public string StorageBucket { get; set; }
        public string MessagingSenderId { get; set; }
        public string AppId { get; set; }
        public string MeasurementId { get; set; }



        public static Firebase GetFirebaseConfig()
        {
            // Ler o arquivo JSON
            var json = File.ReadAllText("firebaseConfig.json");

            // Converter o JSON em um objeto FirebaseConfig
            var config = JsonConvert.DeserializeObject<Firebase>(json);

            // Retornar o objeto FirebaseConfig
            return config;
        }



    }



}