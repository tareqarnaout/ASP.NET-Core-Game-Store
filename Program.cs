using GameStore.Dtos;
using GameStore.GameEndPoints;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
const string EndPoint = "GetGame";

// this is like calling GameEndPoints.MapGameEndPoints(app);
app.MapGameEndPoints();


app.Run();