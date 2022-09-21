using Newtonsoft.Json;
using TheBank;

#region Testing Area
Console.ReadLine();
#endregion

//===============================================================================
/*
 How ids work?:
        The id is just the index of the line of an account inside the .txt
  
 To do:
-getclientinfo
 */

var tools = new Tools();
//Work more on
void Deposit(string client_id, decimal money)
{
    var client = tools.LoadClient(client_id);
    if(money > 0)
    {
        client.Money += money;
    }
    else
    {
        throw new NotImplementedException();
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

    public void Transfer(string receiver_id, decimal money)
    {
        if(money >= Money)
        {
            //var client = tools.LoadClient(receiver_id);
            #region This shouldnt be here
            try
            {
                string[] accounts = File.ReadAllLines("AccountIds.txt");

                foreach (var account in accounts)
                {
                    if (receiver_id.Length == 4 && account.Contains(receiver_id))
                    {
                        var client = JsonConvert.DeserializeObject<Client>(account);
                        client.Money += money;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
            #endregion
        }
    }
    //Work more on
    public void WithDraw(decimal money)
    {
        if(money >= Money)
        {
            Money -= money;
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}