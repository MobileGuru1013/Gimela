﻿using System.ServiceModel;
using Gimela.Rukbat.DVC.Contracts.DataContracts;

namespace Gimela.Rukbat.DVC.Contracts.MessageContracts
{
  [MessageContract]
  public class PingCameraRequest
  {
    [MessageBodyMember]
    public string CameraId { get; set; }
  }
}
