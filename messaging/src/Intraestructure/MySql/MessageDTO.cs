using System;
using System.ComponentModel.DataAnnotations;

namespace Messaging.Intraestructure.MySql
{
    public class MessageDTO
    {
        [Key]
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }

        [MaxLength(255)]
        public string Subject { get; set; }

        public string Body { get; set; }
        public EmailAddressDTO Sender { get; set; }  

    }
}
