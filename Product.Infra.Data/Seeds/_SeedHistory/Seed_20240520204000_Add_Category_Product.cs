using ComandaPro.Domain.Entities;
using Product.Domain.Entities;
using Product.Infra.Data.Context;

namespace Product.Infra.Data.Seeds._SeedHistory;

public class Seed_20240520204000_Add_Category : Seed
{
    private readonly ApplicationDbContext _dbContext;
    public Seed_20240520204000_Add_Category(ApplicationDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Up()
    {
        _dbContext.Category.AddRange(new List<Category>
        {
            new() { Name = "Prato Principal", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 },
            new() { Name = "Entradas", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 },
            new() { Name = "Porções", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 },
            new() { Name = "Sobremesa", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 },
            new() { Name = "Bebidas", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 },
            new() { Name = "Bebidas Alcoólicas", IsActive = true, CreatedAt = DateTime.Now, UserId = 1 }
        });

        _dbContext.SaveChanges();

        _dbContext.Product.AddRange(new List<Domain.Entities.Product>
        {
            new() 
            { 
                Name = "Feijoada", 
                IsActive = true, 
                CreatedAt = DateTime.Now, 
                UserId = 1, 
                CategoryId = 1,
                Price = 65.00M, 
                Description = "Feijão preto cozido com carnes de porco, servido com arroz, couve refogada, farofa e laranja."
            },

            new() 
            { 
                Name = "Arroz com Feijão", 
                IsActive = true, 
                CreatedAt = DateTime.Now, 
                UserId = 1, 
                CategoryId = 1,
                Price = 30.00M, 
                Description = "Acompanhado de carne (bovina, suína ou de frango), salada e farofa."
            },
            new() 
            { 
                Name = "Bife à Cavalo", 
                IsActive = true, 
                CreatedAt = DateTime.Now, 
                UserId = 1, 
                CategoryId = 1,
                Price = 40.00M, 
                Description = "Bife grelhado com um ovo frito por cima, geralmente servido com arroz, feijão e batata frita."
            },

            new()
            {
                Name = "Salada Caprese",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 2,
                Price = 20.00M,
                Description = "Fatias de tomate e muçarela de búfala, decoradas com folhas de manjericão e temperadas com azeite e sal."
            },
            
            
            new()
            {
                Name = "Frango à Passarinho",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 3,
                Price = 50.00M,
                Description = "Pedaços de frango fritos, temperados com alho e acompanhados de mandioca frita ou batata frita."
            },
            new()
            {
                Name = "Linguiça na Chapa",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 3,
                Price = 50.00M,
                Description = "Linguiça grelhada, servida com pão, vinagrete e farofa."
            },
            

            new()
            {
                Name = "Brigadeiro",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 4,
                Price = 30.00M,
                Description = "Doce de chocolate feito com leite condensado, chocolate em pó e manteiga."
            },       
            new()
            {
                Name = "Pudim de Leite Condensado",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 4,
                Price = 30.00M,
                Description = "Pudim cremoso feito de leite condensado, ovos e açúcar."
            },

            new()
            {
                Name = "Suco Natural",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 5,
                Price = 10.00M,
                Description = "Sucos de frutas frescas como laranja, maracujá, acerola, abacaxi, entre outros."
            },   

            new()
            {
                Name = "Refrigerante",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 5,
                Price = 10.00M,
                Description = "Bebidas gasosas populares como Guaraná, Coca-Cola, Fanta, etc."
            },

            new()
            {
                Name = "Cerveja",
                IsActive = true,
                CreatedAt = DateTime.Now,
                UserId = 1,
                CategoryId = 6,
                Price = 10.00M,
                Description = "Pudim cremoso feito de leite condensado, ovos e açúcar."
            },

        });


        _dbContext.SaveChanges();
    }
}
