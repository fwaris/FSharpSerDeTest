namespace FSharpSerDeTest 
open Orleans
open System

type SenderGrain() =
    inherit Grain()

    interface ITestSender with
        member this.Send() = 
            let fac = this.GrainFactory
            task {
                let e1 =
                    {
                        F1 = 0
                        F2 = "F2 non DU"
                        F3 = DateTime.Now

                    }
                printfn $"sending non DU {e1}"
                let g = fac.GetGrain<ITestReceiver>("receiver")
                do! g.Receive(e1)
            }

        member this.SendDU() = 
            let fac = this.GrainFactory
            task {
                let t1 = 
                    {
                        F1 = 0
                        F2 = "F2 DU"
                        F3 = DateTime.Now

                    }
                    |> T1
                let t2 =
                    {
                        G1 = 1
                        G2 = "G2"
                        G3 = DateTime.Now
                    }
                    |> T2
                let g = fac.GetGrain<ITestReceiver>("receiver")
                printfn $"sending t1 {t1}"
                do! g.ReceiveDU(t1)
                printfn $"sending t2 {t2}"
                do! g.ReceiveDU(t2)
            }


type ReceiverGrain() =
    inherit Grain()

    interface ITestReceiver with
        member this.Receive(e1) =
            task {
                printfn $"received non DU {e1}"
            }
        
        member this.ReceiveDU(arg1: D1) =
            task {
                match arg1 with
                | T1 e1 -> printfn "received %A" e1
                | T2 e2 -> printfn "received %A" e2
            }

        member this.Get() =
            task {
                let e1 =
                    {
                        F1 = 0
                        F2 = "F2 non DU - return to client"
                        F3 = DateTime.Now

                    }
                printfn $"returning non DU {e1}"
                return e1
            }
        
        member this.GetDU() =
            task {
                let e1 =
                    {
                        F1 = 0
                        F2 = "F2 non DU - return to client"
                        F3 = DateTime.Now

                    }
                let t1 = T1 e1
                printfn $"returning DU {t1}"
                return t1
            }
