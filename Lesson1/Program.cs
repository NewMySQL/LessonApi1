using Lesson1.Data;
using Lesson1.Entities;

Console.WriteLine("Hello, World!");

using var db = new WorksTokContext();

Console.WriteLine(db.Clients.Any(x => x.Id > 59));

var clients = db.Clients
    //.Where(x => x.Id < 20)
    //.Where(x => x.Id > 10)
    //.OrderByDescending(x => x.Name)
    //.ThenByDescending(x => x.Id)
    .Skip(5)
    .Take(10)
    .ToList()
    .GroupBy(x => x.Name);

// CTRL(Pressed) + K + U - убрать коментарий
// CTRL(Pressed) + K + С - добавить коментарий

foreach (var clientGroup in clients)
{
    //Console.WriteLine(client);
    Console.WriteLine(@$"Group: {clientGroup.Key}");

    foreach (var client in clientGroup)
    {
        Console.WriteLine(@$"   [{client.Id}] {client.Name}");
    }
}

Console.WriteLine();

var firstClient = db.Clients.FirstOrDefault(x => x.Id > 60);
var firstClientId = db.Clients.Select(x => (int?)x.Id).FirstOrDefault(x => x > 60);

Console.WriteLine("{0} - {1}", firstClient?.Id, firstClient?.Name);
Console.WriteLine($"{firstClient?.Id} - {firstClient?.Name}");
Console.WriteLine(firstClient?.Id + " - " + firstClient?.Name);

var line = string.Format("{0} - {1}", firstClient?.Id, firstClient?.Name);
Console.WriteLine(line);

Console.WriteLine($@"

    {firstClient?.Id}
        {firstClient?.Name}

");

Console.WriteLine(@$"Work Type
    Avg price: {db.Worktypes.Average(x => x.Price)}
    Min price: {db.Worktypes.Min(x => x.Price)}
    Max price: {db.Worktypes.Max(x => x.Price)}
    Sum price: {db.Worktypes.Sum(x => x.Price)}
    Count types: {db.Worktypes.Count()}
");


// Scaffold-DbContext "server=localhost;port=3306;user=root;password=0000;database=workstokv1" Pomelo.EntityFrameworkCore.MySql -ContextDir Data -OutputDir Entities –Context WorksTokContext -Force