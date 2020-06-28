using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;

namespace consoletest
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            
            //登録
            services.AddSingleton<IDataService, DataServiceMock>();//Singleton
            services.AddTransient<Printer>();//毎回インスタンス化

            var serviceProvider = services.BuildServiceProvider();

            //インスタンス取得
            var printer = serviceProvider.GetService<Printer>();

            printer.PrintOut();
        }
    }


    public class Printer
    {
        private readonly IDataService _dataService;

        //登録済みのオブジェクトがDIされる
        public Printer(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public void PrintOut()
        {
            var names = _dataService.MembersName();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }

    public interface IDataService
    {
        IEnumerable<string> MembersName();
    }
    public class DataServiceMock : IDataService
    {
        public IEnumerable<string> MembersName()
        {
            return new List<string> { "Taro Yamada", "Ichiro Suzuki", "Jotaro Higashikata" };
        }
    }
}
