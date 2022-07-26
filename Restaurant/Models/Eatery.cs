namespace Restaurant.Models
{
  public class Eatery
  {
    public int EateryId { get; set; }
    public string EateryName { get; set; }
    public int CuisineId { get; set; }
    public virtual Cuisine Cuisine { get; set; }
  }
}