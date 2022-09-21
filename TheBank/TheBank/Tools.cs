using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheBank
{
    internal class Tools
    {
        string account_path = "AccountIds.txt";
        public Client CreateClient(string name, string password)
        {
            string id = CreateID();
            if (id != "Error")
            {
                Console.Clear();
                var client = new Client(name, password);
                client.AccountID = id;
                Console.WriteLine($"Your id is: {id}");
                SaveClient(client);
                return client;
            }
            else
            {
                Console.WriteLine("A problem got out of control");
                return null;
            }

            /// <summary>
            /// Creates an id for a new account
            /// </summary>
            /// <returns></returns>
            string CreateID()
            {
                string[] accounts = File.ReadAllLines(account_path);

                int id = accounts.Length + 1;

                if (id < 10)
                {
                    return "000" + id;
                }
                else if (id < 100)
                {
                    return "00" + id;
                }
                else if (id < 1000)
                {
                    return "0" + id;
                }
                else if (id >= 1000 && id <= 9999)
                {
                    return $"{id}";
                }
                else
                {
                    return "Error";
                }
            }
        }

        public Client LoadClient(string id)
        {
            try
            {
                string[] accounts = File.ReadAllLines(account_path);

                foreach (var account in accounts)
                {
                    if (account.Contains(id))
                    {
                        return JsonConvert.DeserializeObject<Client>(account);
                    }
                }
                Console.WriteLine("Account not found.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public void SaveClient(Client client)
        {
            try
            {
                using (var writer = new StreamWriter(account_path))
                {
                    writer.WriteLine(JsonConvert.SerializeObject(client));
                    Console.WriteLine("Account saved successfuly.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
