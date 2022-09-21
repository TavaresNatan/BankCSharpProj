using Newtonsoft.Json;

#region Testing Area
Console.ReadLine();
#endregion

//===============================================================================
/*
 To do:
-withdraw
-transfer
-getdata
 */
string account_path = "AccountIds.txt";

Client CreateClient(string name, string password)
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

Client LoadClient(string id)
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
    catch(Exception ex)
    {
        Console.WriteLine(ex);
        return null;
    }
}

void Deposit(string client_id, decimal money)
{
    var client = LoadClient(client_id);
    if(money > 0)
    {
        client.Money += money;
    }
}

void SaveClient(Client client)
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

/// <summary>
/// Client class
/// </summary>
class Client
{

    public Client(string name, string password)
    {
        if(name != null && name != "" && password != null && password != "")
        {
            if(password.Length < 6)
            {
                Console.WriteLine("Password is too short.");
            }
            else
            {
                Name = name;
                Password = password;
            }
        }
    }

    public decimal Money { get; set; }
    public string Name { get; set; }
    public string AccountID { get; set; }
    public string Password { get; set; }

    

    
}