using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;

namespace Source;



// Third party paket (Nuget: Author: Isa)
public class LegacyDataService
{
    public IEnumerable<string> GetNames()
    {
        using var sr = new StreamReader("file.txt");

        // 3s

        var text = sr.ReadToEnd();
        var names = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return names;
    }
}




static class LegacyDataServiceExtensions
{
    public static Task<IEnumerable<string>> GetNamesAsync(this LegacyDataService service)
    {
        var tcs = new TaskCompletionSource<IEnumerable<string>>();

        Task.Run(() =>
        {
            var names = service.GetNames();
            tcs.SetResult(names);
        });

        return tcs.Task;
    }


    public static Task<IEnumerable<string>> GetNamesAsync2(this LegacyDataService service)
    {
        return Task.Run(() => service.GetNames());
    }
}