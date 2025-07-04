using Application;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration["allowedOrigins"]!
                    .Split(',', StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("FrontPolicy", p =>
        p.WithOrigins(allowedOrigins)
         .AllowAnyHeader()
         .AllowAnyMethod()
         .AllowCredentials());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("FrontPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();


/*
 ⚠️ Nota: Por motivos de tiempo no se implementó el sistema de autenticación.
Sin embargo, la estrategia contemplada era utilizar JWT (Bearer Token) para la autenticación del lado del backend. En el frontend (Angular), se tenía previsto interceptar las solicitudes HTTP mediante un Interceptor para adjuntar el token en las cabeceras (Authorization: Bearer <token>), y proteger rutas sensibles utilizando Guards que validaran la existencia y vigencia del token.
Esta implementación quedó pendiente por razones de tiempo, pero la arquitectura del proyecto está preparada para su integración futura.
 
 */