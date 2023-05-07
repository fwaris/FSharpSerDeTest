namespace FSharpSerDeTest
open Orleans
open Orleans.Runtime
open Microsoft.Extensions.Logging

type TestService(id:GrainId,silo:Silo,loggerFactory:ILoggerFactory,clnt:IClusterClient) =
    inherit GrainService(id,silo,loggerFactory)

    override this.StartInBackground() =       
        task {
            let g = clnt.GetGrain<ITestSender>("sender")
            do! g.Send()
            do! g.SendDU()
        }

    interface ITestService

