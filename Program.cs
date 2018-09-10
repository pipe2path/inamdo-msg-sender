using MongoDB.Bson;
using Newtonsoft.Json;
using Sinch.ServerSdk;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;

namespace InamdoMsgSender
{
    class Program
    {
        private static Timer timer;
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            var userTask = getUsers();

            userTask.ContinueWith(task => {
                //var users = task.Result;
                //foreach (User u in users)
                //Console.WriteLine(u.userName.ToString());
                Console.WriteLine("Processing done ...");
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);
            Console.ReadLine();
        }

        static async Task<IEnumerable<User>> getUsers() 
        {
            string path = "http://localhost:56362/api/users/couponlist";
            ObjectId userId = new ObjectId();
            string phoneNum = "";
            string smsMessage = "";
            string code = "";
            
            //await Task.Delay(3000);

            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResult);
            
            // process messages
            foreach (User u in users)
            {
                userId = u.userId;
                phoneNum = u.userPhone;
                code = u.code.ToString();
                smsMessage = u.message + " Please use code: " + code + " when you order.";
                var smsApi = SinchFactory.CreateApiFactory("86be6998-e82f-49eb-9d8d-cdd2427ad4a9", "5MnvbXXhe0iMuzXjl02WWQ==").CreateSmsApi();
                var sendSmsResponse = await smsApi.Sms("+19094524127", smsMessage).Send();
                await Task.Delay(TimeSpan.FromSeconds(10));
                var smsMessageStatusResponse = await smsApi.GetSmsStatus(sendSmsResponse.MessageId);

                if (smsMessageStatusResponse.Status == "successful")
                {
                    // update db
                }


            }




            return users;
        }
    }
}
