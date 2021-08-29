// using System;
using System.ComponentModel.DataAnnotations;
// using System.Collections.Generic;

namespace Mydatas
{
    public class User  
    {  
        [Required]  
        [StringLength(50)]  
        public string FirstName { get; set; }  
        [Required]  
        [StringLength(50)]  
        public string LastName { get; set; }  
        // [Required]  
        [EmailAddress]  
        public string EmailAddress { get; set; }  
        [Required]  
        [Phone]  
        public string PhoneNumber { get; set; }  
        [Required]  
        [CreditCard]  
        public string CreditCardNumber { get; set; }  
        [Required]
        public string Address;
    }  
}