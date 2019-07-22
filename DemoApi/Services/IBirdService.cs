using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApi.Services
{
    public interface IBirdService
    {
        Task Say();
    }

    public class Duck : IBirdService
    {
        public Task Say()
        {
            Console.WriteLine("GaGaGa");
            return Task.CompletedTask;
        }
    }
}
