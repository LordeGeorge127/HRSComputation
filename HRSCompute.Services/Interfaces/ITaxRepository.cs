﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Interfaces
{
    public interface ITaxRepository
    {
        decimal TaxAmount(decimal totalAmount);
    }
}
