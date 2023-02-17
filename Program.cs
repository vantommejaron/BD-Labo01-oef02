// add validation to check if all fields are filled in nog toevoegen
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidatorsFromAssemblyContaining<BrandValidator>();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();


// ------------------------
// Make a list with brands
// ------------------------

var brands = new List<Brand>();
brands.Add(new Brand { BrandId = 1, Name = "BMW", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/BMW_Logo_%282017%29.svg/1200px-BMW_Logo_%282017%29.svg.png" });
brands.Add(new Brand { BrandId = 2, Name = "Audi", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/BMW_Logo_%282017%29.svg/1200px-BMW_Logo_%282017%29.svg.png" });
brands.Add(new Brand { BrandId = 3, Name = "Mercedes", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/BMW_Logo_%282017%29.svg/1200px-BMW_Logo_%282017%29.svg.png" });
brands.Add(new Brand { BrandId = 4, Name = "Volkswagen", Country = "Germany", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/BMW_Logo_%282017%29.svg/1200px-BMW_Logo_%282017%29.svg.png" });
brands.Add(new Brand { BrandId = 5, Name = "Porsche", Country = "Italy", Logo = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/04/BMW_Logo_%282017%29.svg/1200px-BMW_Logo_%282017%29.svg.png" });

var carmodels = new List<CarModel>();
carmodels.Add(new CarModel { CarModelId = 1, Name = "BMW 1", Brand = brands[0] });
carmodels.Add(new CarModel { CarModelId = 2, Name = "BMW 2", Brand = brands[0] });
carmodels.Add(new CarModel { CarModelId = 3, Name = "Audi 1", Brand = brands[1] });
carmodels.Add(new CarModel { CarModelId = 4, Name = "Audi 2", Brand = brands[1] });
carmodels.Add(new CarModel { CarModelId = 5, Name = "Audi 3", Brand = brands[1] });
carmodels.Add(new CarModel { CarModelId = 6, Name = "Mercedes 1", Brand = brands[2] });
carmodels.Add(new CarModel { CarModelId = 7, Name = "Mercedes 2", Brand = brands[2] });
carmodels.Add(new CarModel { CarModelId = 8, Name = "Volkswagen 1", Brand = brands[3] });
carmodels.Add(new CarModel { CarModelId = 9, Name = "Porsche", Brand = brands[4] });

app.MapGet("/", () => "Hello World!");


// ------------------------
// GET all the Brand
// ------------------------
app.MapGet("/brands", () =>
{
    return Results.Ok(brands);
});

// ------------------------
// GET all the Brand from a country
// ------------------------
app.MapGet("/brand/{country}", (string country) =>
{
    var brand = brands.FirstOrDefault(b => b.Country == country);
    if (brand == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(brand);
});

// ------------------------
// GET all the carModels
// ------------------------
app.MapGet("/models", () =>
{
    return Results.Ok(carmodels);
});

// ------------------------
// GET all the Brand from a ID
// ------------------------
app.MapGet("/brands/{brandId}", (int brandId) =>
{
    var brand = brands.FirstOrDefault(b => b.BrandId == brandId);
    if (brand == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(brand);
});

// ------------------------
// GET all the Carmodels with a brand
// ------------------------
app.MapGet("/models/{brandName}", (string brandName) =>
{
    var carModel = carmodels.Where(c => c.Brand.Name == brandName);
    if (carModel == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(carModel);
});


// ------------------------
// ADD a Brand
// ------------------------
app.MapPost("/brands", (IValidator<Brand> validator, Brand brand) =>
{
    var result = validator.Validate(brand);
    if (!result.IsValid)
    {
        return Results.BadRequest(result.Errors);
    }

    brand.BrandId = brands.Max(b => b.BrandId) + 1;

    brands.Add(brand);
    return Results.Created($"/brands/{brand.BrandId}", brand);
});

// ------------------------
// GET all the carModel from a ID
// ------------------------
app.MapGet("/model/{CarModelId}", (int carModelId) =>
{
    var carModel = carmodels.FirstOrDefault(c => c.CarModelId == carModelId);
    if (carModel == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(carModel);
});

// ------------------------
// GET all the carModel from a country
// ------------------------
app.MapGet("/model/country/{country}", (string country) =>
{
    var carModel = carmodels.Where(c => c.Brand.Country == country);
    if (carModel == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(carModel);
});





//Moet onderaan je programma staan
app.Run();