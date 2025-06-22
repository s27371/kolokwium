namespace szablon.DTOs;

public class BatchRequestDto
{
    public int Quantity { get; set; }
    public string Species { get; set; }
    public string Nursery { get; set; }
    public List<ResponsibleRequestDto> Responsible { get; set; }
}

public class ResponsibleRequestDto
{
    public int EmployeeId { get; set; }
    public string Role { get; set; }
}
