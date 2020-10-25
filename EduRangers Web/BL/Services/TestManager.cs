using BinderLayer.Interfaces;
using BinderLayer.Models;
using BL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    class TestManager : ITestManager
    {
        private readonly IRepository repository;

        public TestManager(IRepository repo)
        {
            this.repository = repo;
        }
        public void CreateTest(TestModel model)
        {
            //Test test = new Test();

            var mapper = MapHelper.Mapping<TestModel, Test>();
            Test test = mapper.Map<Test>(model);

            this.repository.AddAndSave<Test>(test);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<TestModel> GetTest()
        {
            var mapper = MapHelper.Mapping<Test, TestModel>();
            return mapper.Map<List<TestModel>>(this.repository.GetAll<Test>());

        }

        public TestModel GetTest(Func<Test, bool> predicate)
        {

            var mapper = MapHelper.Mapping<TestModel, Test>();
            return mapper.Map<TestModel>(this.repository.FirstorDefault(predicate));
        }

        public TestModel GetTestById(int id)
        {

            var mapper = MapHelper.Mapping<TestModel, Test>();
            return mapper.Map<TestModel>(this.repository.FirstorDefault<Test>(x => x.Id == id));
        }

        public void RemoveTest(int id)
        {
            var test = this.repository.FirstorDefault<Test>(x => x.Id == id);
            if (test == null)
                throw new NullReferenceException();
            this.repository.RemoveAndSave(test);
        }

        public void UpdateTest(int id, TestModel model)
        {
            var test = this.repository.FirstorDefault<Test>(x => x.Id == id);
            if (test == null)
                throw new NullReferenceException();
            var mapper = MapHelper.Mapping<Test, TestModel>();
            test = mapper.Map<Test>(model);

            this.repository.UpdateAndSave(test);
        }
    }
}
