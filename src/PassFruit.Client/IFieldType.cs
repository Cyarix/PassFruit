﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PassFruit.Contracts {

    public interface IFieldType {

        FieldTypeKey Key { get; }

        string Description { get; }

    }

}
