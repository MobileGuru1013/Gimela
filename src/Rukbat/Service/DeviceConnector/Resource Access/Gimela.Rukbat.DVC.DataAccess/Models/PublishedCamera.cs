﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gimela.Data.Magpie;

namespace Gimela.Rukbat.DVC.DataAccess.Models
{
  [Serializable]
  public class PublishedCamera : IMagpieDocumentId
  {
    public string Id { get; set; }

    public List<Destination> Destinations { get; set; }
  }
}
