﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CompleteTestModel
    {
        public CompleteTestModel(){}

        public string IntakeID { get; set; }

        public string ClientID { get; set; }

        public bool ShowOQChart { get; set; }
    }
}