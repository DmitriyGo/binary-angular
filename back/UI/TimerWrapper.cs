using System.Collections.Concurrent;
using System.Timers;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace binary_linq;

public class TimerWrapper
{
    private readonly Timer _timer;
    private readonly Task<List<Core.Entities.Task>> _tasks;
    private readonly HttpClient _httpClient = new();
    private Task<int> result;


    public TimerWrapper()
    {
        _timer = new Timer();

        //_timer.Start();
    }

    public async Task<int> MarkRandomTaskWithDelay(double delay)
    {
        _timer.Interval = delay;
        _timer.Elapsed += CallBackMethod;
        
        _timer.Start();

        return await result;
    }


    private async void CallBackMethod(object? sender, ElapsedEventArgs e)
    {
        var content = await (await _httpClient.GetAsync(
                "https://localhost:7252/api/Task/"))
            .Content
            .ReadAsStringAsync();

        var tasks = JsonConvert
            .DeserializeObject<List<Core.Entities.Task>>(content)!
            .Where(t => t.State != 2)
            .ToList();

        var task = tasks[new Random().Next(0, tasks.Count)];

        task.State = 2;
        result = Task.Run(() => task.Id);
    }
}