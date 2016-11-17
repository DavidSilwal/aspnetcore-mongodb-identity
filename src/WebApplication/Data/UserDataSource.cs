using Dnx.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.UsersAdminViewModels;

namespace WebApplication.Data
{
    public class UserDataSource : IUserDataSource
    {

        private MongoClient _mongoClient;
        private IMongoDatabase _mongoDatabase;
        private IMongoCollection<MongoIdentityUser> _userCollection;

        public UserDataSource()
        {
            _mongoClient = new MongoClient();
            _mongoDatabase = _mongoClient.GetDatabase("WebApplication");
            _userCollection = _mongoDatabase.GetCollection<MongoIdentityUser>("users");
        }


      

        public async Task<IEnumerable<MongoIdentityUser>> FindAll()
        {
            var users = await _userCollection.Find("{}").ToListAsync();
            return users;
        }

    }
}
