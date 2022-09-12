using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=master; Integrated Security=True"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
}
);

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoExcluir != null) 
    contexto.Produto.Remove(produtoExcluir);
    await contexto.SaveChangesAsync();
}
);

app.MapPost("ListarProduto", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
}
);

app.MapPost("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
     return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
     
}
);

app.UseSwaggerUI();
app.Run();
