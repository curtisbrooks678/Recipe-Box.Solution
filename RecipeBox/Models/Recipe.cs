using System;
using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public Recipe()
    {
      this.JoinEntities = new HashSet<CategoryRecipe>(); 
    }
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public string Ingredients { get; set; }
    public int Rating { get; set; }
    public int[] RatingOptions = new[] { 1, 2, 3, 4, 5 };
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<CategoryRecipe> JoinEntities { get; }
  }
}
