namespace FsSerDeSilo.Client
open System
open System.Threading.Tasks
open Orleans
open Microsoft.Extensions.Hosting
open FSharpSerDeTest

type ClientService(client:IClusterClient) =
   
    interface IHostedService with
        member this.StartAsync(cancellationToken) = 
            task {                
                let client = client.GetGrain<ITestReceiver>("g1")                

                let e1 =
                    {
                        F1 = 0
                        F2 = "F2 non DU external client"
                        F3 = DateTime.Now

                    }
                printfn $"sending non DU {e1} to silo"
                do! client.Receive(e1)
                
                let t1 = 
                    {
                        F1 = 0
                        F2 = "F2 DU external client"
                        F3 = DateTime.Now

                    }
                    |> T1
                let t2 =
                    {
                        G1 = 1
                        G2 = "G2 external client"
                        G3 = DateTime.Now
                    }
                    |> T2

                printfn $"sending DU {e1} to silo"
                do! client.ReceiveDU(t1)
                do! client.ReceiveDU(t2)

                //query grain

                printfn "Get non DU from silo"
                let! e1c = client.Get()
                printfn $"Got {e1c}"

                printfn "Get DU from silo"
                let! t1c = client.GetDU()
                printfn $"Got DU {t1c}"

            }

        member this.StopAsync(cancellationToken) = 
            Task.CompletedTask    

