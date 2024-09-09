using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/patients")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>>
    GetPatients(
    [FromQuery] string sortField = "LastName", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Patients
        .Include(p => p.District) 
        .Select(p => new
        {
            p.Id,
            p.LastName,
            p.FirstName,
            p.MiddleName,
            p.Address,
            p.BirthDate,
            p.Gender,
            DistrictNumber = p.District.Number 
        });
        query = sortField switch
        {
            "FirstName" => query.OrderBy(p => p.FirstName),
            "BirthDate" => query.OrderBy(p => p.BirthDate),
            _ => query.OrderBy(p => p.LastName),
        };
        var totalRecords = await query.CountAsync();
        var patients = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(new { TotalRecords = totalRecords, Data = patients });
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();
        return Ok(patient);
}

// Добавление пациента
    [HttpPost]
    public async Task<ActionResult<Patient>>CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
}

// Редактирование пациента
[HttpPut("{id}")]
public async Task<IActionResult> UpdatePatient(int id, Patient patient)
    { 
        if (id != patient.Id) return BadRequest();
        _context.Entry(patient).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
}
}