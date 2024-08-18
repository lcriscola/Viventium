using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Security.Cryptography;

using Viventium.Benchmark;



var summary = BenchmarkRunner.Run<CompanyImportTests>();


    