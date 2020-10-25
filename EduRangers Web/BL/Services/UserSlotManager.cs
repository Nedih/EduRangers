using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services
{
    class UserSlotManager : IUserSlotManager
    {
        private readonly IRepository repository;

        public UserSlotManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateUserSlot(UserSlotModel model)
        {
            //UserSlot userSlot = new UserSlot();

            var mapper = MapHelper.Mapping<UserSlotModel, UserSlot>();
            UserSlot userSlot = mapper.Map<UserSlot>(model);

            this.repository.AddAndSave<UserSlot>(userSlot);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<UserSlotModel> GetUserSlot()
        {
            var mapper = MapHelper.Mapping<UserSlot, UserSlotModel>();
            return mapper.Map<List<UserSlotModel>>(this.repository.GetAll<UserSlot>());

        }

        public UserSlotModel GetUserSlot(Func<UserSlot, bool> predicate)
        {

            var mapper = MapHelper.Mapping<UserSlotModel, UserSlot>();
            return mapper.Map<UserSlotModel>(this.repository.FirstorDefault(predicate));
        }

        public UserSlotModel GetUserSlotById(int id)
        {

            var mapper = MapHelper.Mapping<UserSlotModel, UserSlot>();
            return mapper.Map<UserSlotModel>(this.repository.FirstorDefault<UserSlot>(x => x.Id == id));
        }

        public void RemoveUserSlot(int id)
        {
            var userSlot = this.repository.FirstorDefault<UserSlot>(x => x.Id == id);
            if (userSlot == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(userSlot);
        }

        public void UpdateUserSlot(int id, UserSlotModel model)
        {
            var userSlot = this.repository.FirstorDefault<UserSlot>(x => x.Id == id);
            if (userSlot == null)
                throw new NullReferenceException();
            var mapper = MapHelper.Mapping<UserSlot, UserSlotModel>();
            userSlot = mapper.Map<UserSlot>(model);

            this.repository.UpdateAndSave(userSlot);
        }
    }
}
