﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashOS.Interpreter.AST
{
    public class MPStmt : Iast
    {
        public int ParameterID, MethodID;
        public string Value;

    }
}