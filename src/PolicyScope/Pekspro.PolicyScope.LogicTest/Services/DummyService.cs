using System;
using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest.Services
{
    public class DummyService1 : IDummyService1
    {
        public void DoWork(bool throwException)
        {
            if (throwException)
            {
                throw new Exception("Ups");
            }
        }

        public Task DoWorkAsync(bool throwException)
        {
            if (throwException)
            {
                throw new Exception("Ups");
            }

            return Task.CompletedTask;
        }

        public virtual int Calculate()
        {
            return 1 << 0;
        }

        public Task<int> CalculateAsync()
        {
            return Task.FromResult(Calculate());
        }
    }

    public class DummyService2 : DummyService1, IDummyService2
    {
        public override int Calculate()
        {
            return 1 << 1;
        }
    }

    public class DummyService3 : DummyService1, IDummyService3
    {
        public override int Calculate()
        {
            return 1 << 2;
        }
    }

    public class DummyService4 : DummyService1, IDummyService4
    {
        public override int Calculate()
        {
            return 1 << 3;
        }
    }
}