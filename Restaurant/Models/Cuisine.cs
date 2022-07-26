using System.Collections.Generic;

namespace Restaurant.Models
{
  public class Cuisine
  {
    public Cuisine()
    {
      this.Eateries = new HashSet<Eatery>();
    }

    public int CuisineId { get; set; }
    public string CuisineType { get; set; }
    public virtual ICollection<Eatery> Eateries { get; set; }//Entity requires ICollection specifically. By declaring Items as an ICollection<Item> data type, we're ensuring Entity will be able to use all the ICollection methods it requires on the Item objects in order to act as our ORM and we wont have to manually interact with our database. Virtual allows Entity to use LazyLoading. 
  }
}