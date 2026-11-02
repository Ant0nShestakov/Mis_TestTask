var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var misdb = postgres.AddDatabase("testtaskdb");

builder.AddProject<Projects.Api>("api")
    .WithReference(misdb)
    .WaitFor(misdb);

builder.Build().Run();