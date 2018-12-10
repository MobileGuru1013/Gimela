﻿using System;
using System.Runtime.Serialization;

namespace Gimela.Data.DataStructures
{
  /// <summary>
  /// 链式文件异常
  /// </summary>
  [Serializable]
  public class LinkedFileException : Exception
  {
    /// <summary>
    /// 链式文件异常
    /// </summary>
    public LinkedFileException()
    {
    }

    /// <summary>
    /// 链式文件异常
    /// </summary>
    /// <param name="message">异常信息</param>
    public LinkedFileException(string message)
      : base(message)
    {
    }

    /// <summary>
    /// 链式文件异常
    /// </summary>
    /// <param name="message">异常信息</param>
    /// <param name="innerException">内部异常</param>
    public LinkedFileException(string message, Exception innerException) :
      base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LinkedFileException"/> class.
    /// </summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="info"/> parameter is null. </exception>
    ///   
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0). </exception>
    protected LinkedFileException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
