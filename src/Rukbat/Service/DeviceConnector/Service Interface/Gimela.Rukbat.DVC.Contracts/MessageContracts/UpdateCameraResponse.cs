﻿using System.ServiceModel;
using Gimela.Rukbat.DVC.Contracts.DataContracts;

namespace Gimela.Rukbat.DVC.Contracts.MessageContracts
{
  [MessageContract]
  public class UpdateCameraResponse
  {
    [MessageBodyMember]
    public CameraData Camera { get; set; }
  }
}
