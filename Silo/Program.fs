namespace FsSerDeSilo
open System
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Orleans
open Orleans.Hosting
open Orleans.Configuration

module Load =

    [<assembly: Orleans.ApplicationPartAttribute("FsSerDeCodeGen")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Core.Abstractions")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Core")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Persistence.Memory")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Runtime")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Reminders")>]
    [<assembly: Orleans.ApplicationPartAttribute("OrleansDashboard.Core")>]
    [<assembly: Orleans.ApplicationPartAttribute("OrleansDashboard")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Clustering.Redis")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Persistence.Redis")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Streaming")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization.Abstractions")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization")>]
    ()        

module Program =

    [<EntryPoint>]
    let main args =
        Host
            .CreateDefaultBuilder(args)
            .UseOrleans(fun ctx sb -> 
                sb
                    .UseInMemoryReminderService()
                    .UseDashboard()
                    .UseLocalhostClustering()
                    .AddMemoryGrainStorage("PubSubStore")
                    .AddGrainService<FSharpSerDeTest.TestService>()
                    |> ignore)
            .RunConsoleAsync()
            |> Async.AwaitTask
            |> Async.RunSynchronously
        0
