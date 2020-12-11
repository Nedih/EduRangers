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
            test.Course = this.repository.FirstorDefault<Course>(x => x.Id == model.CourseId);
            this.repository.AddAndSave<Test>(test);
        }

        public void Dispose()
        {
            this.repository.Dispose();
        }
        public IEnumerable<TestModel> GetTest()
        {
            var mapper = MapHelper.Mapping<Test, TestModel>();
            var list = mapper.Map<List<TestModel>>(this.repository.GetAll<Test>());
            foreach (var i in list)
                i.AvgMark = AvgMark(i.Id);
            return list;
        }

        public double AvgMark(int id)
        {
            var temp = this.repository.GetAttemptsWhere<Attempt>(x => x.Test.Id == id);
            List<int> marks = new List<int>();
            foreach(var i in temp)
            {
                marks.Add((int)i.Mark);
            }
            if (marks.Count != 0)
               return marks.Average();
            return 0;
        }

        public IEnumerable<TestModel> GetTests(int id) {
            var mapper = MapHelper.Mapping<Test, TestModelMap>();
            var mapper2 = MapHelper.Mapping<TestModelMap, TestModel>();
            var list = mapper2.Map<List<TestModel>>(mapper.Map<List<TestModelMap>>(this.repository.GetTestWhere<Test>(x => x.Course.Id == id)));
            foreach (var i in list)
                i.AvgMark = AvgMark(i.Id);
            return list;
        }

        public TestModel GetTest(Func<Test, bool> predicate)
        {
            var mapper = MapHelper.Mapping<Test, TestModel>();
            var test = mapper.Map<TestModel>(this.repository.FirstorDefault(predicate));
            test.AvgMark = AvgMark(test.Id);
            return test;
        }

        public TestModel GetTestById(int id)
        {

            var mapper = MapHelper.Mapping<Test, TestModel>();
            var test = mapper.Map<TestModel>(this.repository.FirstorDefault<Test>(x => x.Id == id));
            test.AvgMark = AvgMark(test.Id);
            return test;
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
