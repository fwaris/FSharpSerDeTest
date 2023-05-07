namespace FsSerDeSilo.Client
open System
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Orleans
open Orleans.Hosting
open Orleans.Configuration

module Load =

    [<assembly: Orleans.ApplicationPartAttribute("FsSerDeCodeGen")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Core.Abstractions")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Core")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Runtime")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Streaming")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization.Abstractions")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization")>]
    [<assembly: Orleans.ApplicationPartAttribute("Orleans.Serialization.FSharp")>]
    ()        

module Program =

    [<EntryPoint>]
    let main args =
        Host
            .CreateDefaultBuilder(args)
            .UseOrleansClient(fun oc -> 
                oc
                    .UseLocalhostClustering()
                |> ignore
                )
            .ConfigureServices(fun svcs -> svcs.AddHostedService<ClientService>() |> ignore)
            .RunConsoleAsync()
            |> Async.AwaitTask
            |> Async.RunSynchronously
        0
