using AsoftyBackend.Infrastructure.Data.DatabaseHandler;
using AsoftyBackend.Infrastructure.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Infrastructure
{
    public static class ManualUnitTestComponent
    {
        public static async void Main()
        {

            var cmd = new QueryGenericHandler<User>();


            //  Query Example
            var user1 = await cmd.Where(u => u.Username == "UnitTest1" && u.Password == "12345678" && u.Enabled == true).QueryAsync();

            var usr = new User { CompanyCode = 0, Username = "George", Password = "Macaco12" };

            //  Insert single
            //  Note: preferably insert objects of anonymous type, because if the type is used, you must declare all the necessary fields
            await cmd.InsertAsync(usr);

            

            ;
            




        }



        

    }
}
