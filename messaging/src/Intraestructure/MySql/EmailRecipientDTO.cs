using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Messaging.Intraestructure.MySql
{
    public class EmailRecipientDTO
    {
        [Key, Column(Order = 0)]
        public Guid MessageId { get; set; }

        [Key, Column(Order = 1)]
        public Guid RecipientId { get; set; }

        public virtual MessageDTO Message { get; set; }

        public virtual EmailAddressDTO Recipient { get; set; }
    }
}