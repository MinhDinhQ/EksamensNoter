using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// 🔧 Konfigurer builder (typisk for ASP.NET Core 6+)
var builder = WebApplication.CreateBuilder(args);

// 🛡️ HEMMELIG nøgle til at signere token (burde komme fra config i produktion)
var jwtKey = "min-hemmelige-nøgle-123456789"; // mindst 16 karakterer for sikkerhed
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// 🔐 Konfigurer JWT-autentificering
builder.Services.AddAuthentication(options =>
{
    // Brug JWT som standard autentificeringsmetode
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Her specificerer vi hvordan JWT-token skal valideres
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false, // Du kan sætte til true og tilføje din egen issuer
        ValidateAudience = false, // Ligeledes for audience
        ValidateIssuerSigningKey = true, // VIGTIG: vi vil tjekke at token er signeret korrekt
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes), // brug nøglen ovenfor
        ClockSkew = TimeSpan.Zero // ingen tidsbuffer (for test)
    };
});

// 🧾 Tilføj autorisation (roller, policies osv. - ikke brugt her, men krævet af pipeline)
builder.Services.AddAuthorization();

// 🚀 Build appen
var app = builder.Build();

// 📦 Middleware til autentificering og autorisation (rækkefølgen er vigtig!)
app.UseAuthentication();
app.UseAuthorization();


// 🟢 ENDPOINT 1: LOGIN
// - Brugeren sender brugernavn og adgangskode
// - Hvis det matcher "admin"/"1234", returnerer vi et JWT-token
app.MapPost("/login", (User user) =>
{
    // ⛔ Dummy-brugercheck – brug database i rigtig app!
    if (user.Username == "admin" && user.Password == "1234")
    {
        // Opret en tokenhandler (hjælper til at generere JWT)
        var tokenHandler = new JwtSecurityTokenHandler();

        // Specificer claims – fx hvem brugeren er
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            // Du kan tilføje flere claims, fx roller: new Claim(ClaimTypes.Role, "Admin")
        };

        // Definer token-egenskaber
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30), // token udløber om 30 minutter
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes), // brug samme nøgle som ved validering
                SecurityAlgorithms.HmacSha256Signature) // signatur-algoritme
        };

        // Opret og skriv token
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        // ✅ Returnér token til klienten
        return Results.Ok(new { token = jwt });
    }

    // ⛔ Forkert brugernavn eller kodeord
    return Results.Unauthorized();
});


// 🔐 ENDPOINT 2: /secret
// - Beskyttet endpoint, som kræver gyldigt JWT-token
app.MapGet("/secret", () => "🎉 Du har adgang til den hemmelige side!")
   .RequireAuthorization(); // <- kræver autorisation = JWT-token

// 🟨 Kør applikationen
app.Run();

// 🧑 Brugermodel – vi modtager JSON med username og password
record User(string Username, string Password);
