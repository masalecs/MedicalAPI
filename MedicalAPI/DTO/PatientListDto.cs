using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

public class PatientListDto
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string Address { get; set; }
    public DateTime BirthDate { get; set; }
    public string Gender { get; set; }
    public string DistrictNumber { get; set; }
}