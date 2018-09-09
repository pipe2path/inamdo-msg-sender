using Newtonsoft.Json;
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
                var users = task.Result;
                foreach (User u in users)
                    Console.WriteLine(u.userName.ToString());
                
            },
            TaskContinuationOptions.OnlyOnRanToCompletion);
            Console.ReadLine();
        }

        static async Task<IEnumerable<User>> getUsers() 
        {
            string path = "http://localhost:56362/api/users/couponlist";
            //await Task.Delay(3000);

            var response = await client.GetAsync(path);
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            IEnumerable<User> users = JsonConvert.DeserializeObject<IEnumerable<User>>(stringResult);
            return users;
        }
        
        
    }
}
