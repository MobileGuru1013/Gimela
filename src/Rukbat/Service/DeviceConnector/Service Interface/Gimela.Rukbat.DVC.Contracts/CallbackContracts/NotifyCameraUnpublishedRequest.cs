﻿using System.ServiceModel;
using Gimela.Rukbat.DVC.Contracts.DataContracts;

namespace Gimela.Rukbat.DVC.Contracts.CallbackContracts
{
  [MessageContract]
  public class NotifyCameraUnpublishedRequest
  {
    [MessageBodyMember]
    public string CameraId { get; set; }

    [MessageBodyMember]
    public PublishDestinationData Destination { get; set; }
  }
}
