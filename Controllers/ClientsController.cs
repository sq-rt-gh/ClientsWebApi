using ClientsWebApi.Data;
using ClientsWebApi.DTO;
using ClientsWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClientsWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(AppDbContext context) : ControllerBase
{
    private readonly AppDbContext _context = context;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var clients = await _context.Clients
            .Include(c => c.Founders)
            .ToListAsync();

        return Ok(clients);
    }


    [HttpGet("{inn}")]
    public async Task<IActionResult> Get(string inn)
    {
        var client = await _context.Clients
            .Include(c => c.Founders)
            .FirstOrDefaultAsync(c => c.Inn == inn);

        if (client == null)
            return NotFound();

        return Ok(client);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientDto dto)
    {
        if (await _context.Clients.AnyAsync(c => c.Inn == dto.Inn))
            return Conflict("Клиент с таким ИНН уже существует.");

        var client = new Client
        {
            Inn = dto.Inn,
            Name = dto.Name,
            Type = dto.Type
        };

        if (client.Type == ClientType.UL && dto.FounderInns is not null)
        {
            var founders = await _context.Founders
                .Where(f => dto.FounderInns.Contains(f.Inn))
                .ToListAsync();

            client.Founders = founders;
        }

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { inn = client.Inn }, client);
    }


    [HttpPut("{inn}")]
    public async Task<IActionResult> Update(string inn, [FromBody] ClientDto dto)
    {
        var existing = await _context.Clients
            .Include(c => c.Founders)
            .FirstOrDefaultAsync(c => c.Inn == inn);

        if (existing == null)
            return NotFound();

        existing.Name = dto.Name;
        existing.Type = dto.Type;

        if (dto.Type == ClientType.IP)
        {
            existing.Founders = null;
        }
        else if (dto.FounderInns is not null)
        {
            var founders = await _context.Founders
                .Where(f => dto.FounderInns.Contains(f.Inn))
                .ToListAsync();

            existing.Founders = founders;
        }

        await _context.SaveChangesAsync();

        return Ok(existing);
    }


    [HttpDelete("{inn}")]
    public async Task<IActionResult> Delete(string inn)
    {
        var client = await _context.Clients
            .Include(c => c.Founders)
            .FirstOrDefaultAsync(c => c.Inn == inn);

        if (client == null)
            return NotFound();

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return Ok();
    }
}