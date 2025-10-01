﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public record BookDtoForUpdate
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
    }

    //public record BookDtoForUpdate(int Id, string Title, decimal Price);
}
