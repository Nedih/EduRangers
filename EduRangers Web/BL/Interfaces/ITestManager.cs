using BinderLayer.Models;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface ITestManager
    {
        IEnumerable<TestModel> GetTest();

        TestModel GetTestById(int id);

        TestModel GetTest(Func<Test, bool> predicate);

        void CreateTest(TestModel model);

        void UpdateTest(int id, TestModel model);

        void RemoveTest(int id);
    }
}
