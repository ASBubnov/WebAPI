using Microsoft.Extensions.DependencyInjection;
using Services;
using DAL.DBConnector;
using DAL.Interfaces;
using DAL.Models;

public class Program
{
    public async void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton<IPipelineService, PipelineService>();
        services.AddSingleton<DBMyContext>();
        services.AddSingleton<IRepository<Pipeline>, PipelineRepository>();
        services.AddSingleton<ITaskService, TaskService>();
        services.AddSingleton<IRepository<TaskApp>, TaskAppRepository>();
        var provider = services.BuildServiceProvider();
        var pipelineService = provider.GetService<IPipelineService>();
        var pipeline = await RunPipeline(Convert.ToInt16(args.FirstOrDefault()), pipelineService);
        Console.WriteLine(pipeline.AverageTime);
    }
    
    private async Task<Pipeline> RunPipeline(int pipeLineId, IPipelineService service)
    {
        var pipeline = service.Run(pipeLineId);
        await Task.Run(()=> Pause());
        return pipeline;
    }

    private void Pause()
    {
        var random = new Random();
        var pause = random.Next(100, 10000);
        Thread.Sleep(pause);
    }
}

