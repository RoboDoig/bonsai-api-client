﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.Models
{
    public interface ICanvasObject
    {
        int Layer { get; }
        int LayerIndex { get; }
    }
}