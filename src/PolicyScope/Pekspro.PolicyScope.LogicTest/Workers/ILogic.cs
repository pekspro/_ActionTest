﻿// This file has been autogenerated via
// Pekspro.PolicyScope.CodeGenerator.LogicTest.Workers.LogicGenerator

using System.Threading.Tasks;

namespace Pekspro.PolicyScope.LogicTest.Workers
{
    public interface ILogic
    {
        public Task RunWithNoResultNoServiceAsync();

        public Task RunWithNoResult1ServiceAsync();

        public Task RunWithNoResult2ServiceAsync();

        public Task RunWithNoResult3ServiceAsync();

        public Task RunWithNoResult4ServiceAsync();

        public Task<int> RunWithResultNoServiceAsync();

        public Task<int> RunWithResult1ServiceAsync();

        public Task<int> RunWithResult2ServiceAsync();

        public Task<int> RunWithResult3ServiceAsync();

        public Task<int> RunWithResult4ServiceAsync();

        public void RunWithNoResultNoService();

        public void RunWithNoResult1Service();

        public void RunWithNoResult2Service();

        public void RunWithNoResult3Service();

        public void RunWithNoResult4Service();

        public int RunWithResultNoService();

        public int RunWithResult1Service();

        public int RunWithResult2Service();

        public int RunWithResult3Service();

        public int RunWithResult4Service();

    }
}
