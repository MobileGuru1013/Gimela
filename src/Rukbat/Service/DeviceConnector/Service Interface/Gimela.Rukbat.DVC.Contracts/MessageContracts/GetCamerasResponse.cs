﻿using System.ServiceModel;
using Gimela.Rukbat.DVC.Contracts.DataContracts;

namespace Gimela.Rukbat.DVC.Contracts.MessageContracts
{
  [MessageContract]
  public class GetCamerasResponse
  {
    [MessageBodyMember]
    public CameraDataCollection Cameras { get; set; }
  }
}
