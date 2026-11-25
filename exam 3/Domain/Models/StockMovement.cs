namespace Domain.Models;

public class StockMovement
{

    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ChangeQty { get; set; }
    public DateTime MovedAt { get; set; }
}
