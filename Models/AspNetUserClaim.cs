﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class AspNetUserClaim
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public virtual AspNetUser User { get; set; }
    }
}