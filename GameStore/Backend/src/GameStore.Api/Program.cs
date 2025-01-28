var builder = WebApplication.CreateBuilder(args);
// these lines are all about configuring this build
var app = builder.Build();

// the request pipeline
app.MapGet("/", () => "Hello World!");
// in this case we say any requests that arrive, 
// we will reply with Hello World!
app.Run();
