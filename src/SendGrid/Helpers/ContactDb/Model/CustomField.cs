// <copyright file="ASM.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using System.Collections.Generic;

namespace SendGrid.Helpers.CustomDb
{
    public class CustomField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}