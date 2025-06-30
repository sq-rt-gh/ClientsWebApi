using ClientsWebApi.Data;
using ClientsWebApi.DTO;
using ClientsWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FoundersController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var founders = await _context.Founders.ToListAsync();
        return Ok(founders);
    }


    [HttpGet("{inn}")]
    public async Task<IActionResult> Get(string inn)
    {
        var founder = await _context.Founders.FirstOrDefaultAsync(f => f.Inn == inn);

        if (founder == null)
            return NotFound();

        return Ok(founder);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] FounderDto dto)
    {
        if (await _context.Founders.AnyAsync(f => f.Inn == dto.Inn))
            return Conflict("Учредитель с таким ИНН уже существует.");

        var founder = new Founder
        {
            Inn = dto.Inn,
            FullName = dto.FullName
        };

        _context.Founders.Add(founder);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { inn = founder.Inn }, founder);
    }


    [HttpPut("{inn}")]
    public async Task<IActionResult> Update(string inn, [FromBody] FounderDto dto)
    {
        var founder = await _context.Founders.FirstOrDefaultAsync(f => f.Inn == inn);

        if (founder == null)
            return NotFound();

        founder.FullName = dto.FullName;

        await _context.SaveChangesAsync();
        return Ok(founder);
    }


    [HttpDelete("{inn}")]
    public async Task<IActionResult> Delete(string inn)
    {
        var founder = await _context.Founders.FirstOrDefaultAsync(f => f.Inn == inn);

        if (founder == null)
            return NotFound();

        _context.Founders.Remove(founder);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
