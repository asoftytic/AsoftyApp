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

            var db = new QueryGenericHandler<User>();


            //  Query Example
            var user1 = await db.Where(u => u.Username == "UnitTest1" && u.Password == "12345678" && u.Enabled == true).QueryAsync();

            //  Create random users
            var usr = new User { CompanyCode = 0, Username = "George5", Password = "Macaco12" };
            var usr1 = new User { CompanyCode = 0, Username = "George6", Password = "Macaco12" };
            var usr2 = new User { CompanyCode = 0, Username = "George7", Password = "Macaco12" };

            //  Insert single
            //  Note: preferably insert objects of anonymous type, because if the type is used, you must declare all the necessary fields
            var t0 = db.InsertAsync(usr);
            var t1 = db.InsertAsync(usr1);
            var t2 = db.InsertAsync(usr2);

            //  Delete example
            var result = await db.DeleteWhere(u => u.Password == "Macaco12");   // Result needs to be 3

            //  Delete one by PrimaryKey
            result = await db.DeleteWhere(u => u.UserId == user1.First().UserId);

            ;
            




        }



        

    }
}
