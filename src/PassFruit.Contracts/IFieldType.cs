﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassFruit.Contracts {

    public interface IFieldType {

        string Type { get; }

        bool IsDefault { get; }

        bool IsPassword { get; }

    }

}
