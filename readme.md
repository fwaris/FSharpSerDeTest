# FSharpSerDeTest

Minimal project that showcases serialization of F# discriminated union data types with 
Microsoft Orleans.

## Projects:

- Silo: Hosts the Orleans Cluster 
- FSharpSerDeTest: Contains grain interfaces and implmentations for silo side
- FsSerDeCodeGen:  C# project for serialization code generation
- ExternalClient: External client - run as a separate process - that interacts with the Orleans cluster

