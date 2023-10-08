using System.ComponentModel.DataAnnotations;

namespace Messaging.Intraestructure.MySql
{
    public class EmailAddressDTO
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string EmailAddress { get; set; }

    }
}
