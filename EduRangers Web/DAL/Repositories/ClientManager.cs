using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ClientManager : IClientManager
    {
        public UserContext Database { get; set; }
        public ClientManager(UserContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Update(ClientProfile item)
        {
            var profile = Database.ClientProfiles.FirstOrDefault(x => x.Id == item.Id);
            Database.ClientProfiles.Remove(profile);
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
