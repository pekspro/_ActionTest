using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest.Services
{
    public interface IDummyService1
    {
        void DoWork(bool throwException);
        Task DoWorkAsync(bool throwException);

        int Calculate();

        Task<int> CalculateAsync();
    }

    public interface IDummyService2 : IDummyService1

    {

    }

    public interface IDummyService3 : IDummyService1

    {

    }

    public interface IDummyService4 : IDummyService1

    {

    }

}