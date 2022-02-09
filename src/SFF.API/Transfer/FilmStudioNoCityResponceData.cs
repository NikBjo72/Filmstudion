using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SFF.API.Domain.Entities;
using SFF.API.Transfer.Interfaces;

namespace SFF.API.Transfer
{
    public class FilmStudioNoCityResponceData
    {
        public string FilmStudioId { get; set; }
        public string Name { get; set; }
    }
}