using Dnx.Identity.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApplication.Data
{
    public interface IUserDataSource
    {
        Task<IEnumerable<Dnx.Identity.MongoDB.MongoIdentityUser>> FindAll();
    }
}