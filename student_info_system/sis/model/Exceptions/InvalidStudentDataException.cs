﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sis.model.Exceptions
{
    internal class InvalidStudentDataException : Exception
    {
        public InvalidStudentDataException(string message)
        : base(message) { }
    }
}
