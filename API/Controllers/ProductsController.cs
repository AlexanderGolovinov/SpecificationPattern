using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await repository.ListAsync(spec);
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null) return NotFound();

        return product;
    }

    //TODO: Implement methods
    // [HttpGet("brands")]
    // public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    // {
    //     return Ok(await productRepository.GetBrandsAsync());
    // }
    //
    // [HttpGet("types")]
    // public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
    // {
    //     return Ok(await productRepository.GetTypesAsync());
    // }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        repository.Add(product);

        if (await repository.SaveAllAsync())
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);

        return BadRequest("Problem Creating Product");
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
    {
        if (id != product.Id || !ProductExists(id))
            return BadRequest("Product ID mismatch.");

        repository.Update(product);
        if (await repository.SaveAllAsync()) return NoContent();

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> DeleteProduct(int id)
    {
        var product = await repository.GetByIdAsync(id);
        if (product == null) return NotFound();

        repository.Remove(product);
        if (await repository.SaveAllAsync()) return product.Id;
        return BadRequest("Problem deleting the product");
    }

    // Helper method to check if a product exists
    private bool ProductExists(int id)
    {
        return repository.Exists(id);
    }
}