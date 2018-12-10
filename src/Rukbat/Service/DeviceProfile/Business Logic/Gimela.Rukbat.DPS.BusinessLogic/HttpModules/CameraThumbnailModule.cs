﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Gimela.Data.Json;
using Gimela.Infrastructure.ResourceLocation;
using Gimela.Net.Http;
using Gimela.Net.Http.Headers;
using Gimela.Net.Http.Messages;
using Gimela.Net.Http.Modules;
using Gimela.Rukbat.DPS.BusinessEntities;

namespace Gimela.Rukbat.DPS.BusinessLogic.HttpModules
{
  public class CameraThumbnailModule : IModule
  {
    public ProcessingResult Process(RequestContext context)
    {
      // URL format : ~/camera/{id}/thumbnail
      Regex r = new Regex(@"^/camera/(\w+)/thumbnail$", RegexOptions.IgnoreCase);
      Match m = r.Match(context.Request.Uri.AbsolutePath.ToLowerInvariant());

      if (!m.Success) return ProcessingResult.Continue;

      string cameraId = m.Groups[1].ToString().ToLowerInvariant();
      byte[] thumbnail = Locator.Get<ICameraManager>().GetCameraThumbnail(cameraId);

      IRequest request = context.Request;
      IResponse response = context.Response;

      response.ContentType = new ContentTypeHeader("image/bmp");
      response.ContentLength.Value = thumbnail.Length;
      response.Add(new DateHeader("Last-Modified", DateTime.Now.ToUniversalTime()));

      var generator = HttpFactory.Current.Get<ResponseWriter>();
      generator.SendHeaders(context.HttpContext, response);
      generator.SendBody(context.HttpContext, new MemoryStream(thumbnail));

      return ProcessingResult.Abort;
    }
  }
}
