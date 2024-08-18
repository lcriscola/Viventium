using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Viventium.Business;
using Viventium.Repositores;
using Viventium.Repositores.Infrastructure;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Viventium.Benchmark
{

    /*
        | Method                          | Mean     | Error    | StdDev   | Gen0      | Gen1     | Allocated |
        |-------------------------------- |---------:|---------:|---------:|----------:|---------:|----------:|
        | ImportWithCompoundDictionaryKey | 62.81 ms | 7.009 ms | 10.27 ms | 1000.0000 | 857.1429 |  15.98 MB |
        | ImportWithChainDictionary       | 73.67 ms | 8.328 ms | 12.21 ms | 1000.0000 | 833.3333 |  15.87 MB |
     * */
    //[SimpleJob(RuntimeMoniker.HostProcess)]
    [RPlotExporter]
    [Config(typeof(AntiVirusFriendlyConfig))]
    [MemoryDiagnoser]
    public class CompanyImportTests
    {

        public CompanyImportTests()
        {

            

        }

        private CompanyService GetService()
        {
            DbContextOptionsBuilder<ViventiumDataContext> builder = new DbContextOptionsBuilder<ViventiumDataContext>();
            builder.UseSqlServer("Server=(local); Database=Viventium; Integrated Security=true; Encrypt=false;Application NAme=Viventium v1.0");

            Microsoft.EntityFrameworkCore.DbContextOptions<ViventiumDataContext> options = builder.Options;
            var db = new ViventiumDataContext(options);
            var repo = new GenericRepository(db);
            var service = new CompanyService(repo);
            return service;
        }
        [Benchmark]
        public void ImportWithCompoundDictionaryKey()
        {
            var service = GetService();
            using var st = GetStream();
            service.ImportCSV(st).Wait();

        }

        [Benchmark]
        public void ImportWithChainDictionary()
        {
            var service = GetService();
            using var st = GetStream();
            service.ImportCSV2(st).Wait();
        }


        private Stream GetStream()
        {
            return File.Open(@"..\..\..\..\Files\Data.csv", FileMode.Open);
        }

    

    }


    public class AntiVirusFriendlyConfig : ManualConfig
    {
        public AntiVirusFriendlyConfig()
        {
            AddJob(Job.MediumRun
                .WithToolchain(InProcessNoEmitToolchain.Instance));
        }
    }
}
