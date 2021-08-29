using System.ComponentModel.DataAnnotations;


namespace Mydatas
{
    public class product 
    {
        [Required]  
        [StringLength(50)]  
        public string name{get ; set ;}
        [Required]
        public int price{get ; set ;}
        [Required] 
        public string photo{get ; set ;}
        public int number = 0;
        public int Id {get ; set ;}
        public string Color {get ;set ;}
        [Required] 
        public int LeftInStock {get ;set ;}
        // public string isAvailable = "Available";
    }
}