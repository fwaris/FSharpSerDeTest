namespace FSharpSerDeTest
open Orleans
open System
open System.Threading.Tasks
open Orleans.Services

//[<CLIMutable>]
[<GenerateSerializer>]
type E1 =
    {
        [<Id(0u)>] F1 : int
        [<Id(1u)>] F2 : string
        [<Id(2u)>] F3 : DateTime
    }

//[<CLIMutable>]
[<GenerateSerializer>]
type E2 =
    {
        [<Id(0u)>] G1 : int
        [<Id(1u)>] G2 : string
        [<Id(2u)>] G3 : DateTime
    }

[<GenerateSerializer>]
type D1 = T1 of E1 | T2 of E2

type ITestReceiver =
    inherit IGrainWithStringKey
    abstract member Receive : E1 -> Task
    abstract member ReceiveDU : D1 -> Task
    //invoke from client
    abstract member Get : unit -> Task<E1>
    abstract member GetDU : unit -> Task<D1>

type ITestSender =
    inherit IGrainWithStringKey
    abstract member Send : unit -> Task
    abstract member SendDU : unit -> Task

type ITestService =
    inherit IGrainService
