﻿﻿/*
 * [The "BSD Licence"]
 * Copyright (c) 2011-2012 Chundong Gao
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 * 3. The name of the author may not be used to endorse or promote products
 *    derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
 * IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using Gimela.Knifer.CommandLines.Foundation;

namespace Gimela.Knifer.CommandLines.KillProcess
{
  public class KillProcessCommandLine : CommandLine
  {
    #region Fields

    private KillProcessCommandLineOptions options;

    #endregion

    #region Constructors

    public KillProcessCommandLine(string[] args)
      : base(args)
    {
    }

    #endregion

    #region ICommandLine Members

    public override void Execute()
    {
      base.Execute();

      List<string> singleOptionList = KillProcessOptions.GetSingleOptions();
      CommandLineOptions cloptions = CommandLineParser.Parse(Arguments.ToArray<string>(), singleOptionList.ToArray());
      options = ParseOptions(cloptions);
      CheckOptions(options);

      if (options.IsSetHelp)
      {
        RaiseCommandLineUsage(this, KillProcessOptions.Usage);
      }
      else if (options.IsSetVersion)
      {
        RaiseCommandLineUsage(this, Version);
      }
      else
      {
        StartKillProcess();
      }

      Terminate();
    }

    #endregion

    #region Private Methods

    private void StartKillProcess()
    {
      try
      {
        KillProcess();
      }
      catch (CommandLineException ex)
      {
        RaiseCommandLineException(this, ex);
      }
    }

    private void KillProcess()
    {
      try
      {
        if (options.IsSetPid)
        {
          Process p = Process.GetProcessById(int.Parse(options.Pid));
          p.Kill();
        }
        else if(options.IsSetName)
        {
          Process[] processes = Process.GetProcesses();
          foreach (Process p in processes)
          {
            if (p.ProcessName == options.Name)
            {
              p.Kill();
            }
          }
        }
      }
      catch (PlatformNotSupportedException ex)
      {
        throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
          "Operation exception -- {0}", ex.Message));
      }
      catch (InvalidOperationException ex)
      {
        throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
          "Operation exception -- {0}", ex.Message));
      }
      catch (NotSupportedException ex)
      {
        throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
          "Operation exception -- {0}", ex.Message));
      }
      catch (Win32Exception ex)
      {
        throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
          "Operation exception -- {0}", ex.Message));
      }
    }

    #endregion

    #region Parse Options

    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    private static KillProcessCommandLineOptions ParseOptions(CommandLineOptions commandLineOptions)
    {
      if (commandLineOptions == null)
        throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
          "Option used in invalid context -- {0}", "must specify a option."));

      KillProcessCommandLineOptions targetOptions = new KillProcessCommandLineOptions();

      if (commandLineOptions.Arguments.Count >= 0)
      {
        foreach (var arg in commandLineOptions.Arguments.Keys)
        {
          KillProcessOptionType optionType = KillProcessOptions.GetOptionType(arg);
          if (optionType == KillProcessOptionType.None)
            throw new CommandLineException(
              string.Format(CultureInfo.CurrentCulture, "Option used in invalid context -- {0}",
              string.Format(CultureInfo.CurrentCulture, "cannot parse the command line argument : [{0}].", arg)));

          switch (optionType)
          {
            case KillProcessOptionType.Pid:
              targetOptions.IsSetPid = true;
              targetOptions.Pid = commandLineOptions.Arguments[arg];
              break;
            case KillProcessOptionType.Name:
              targetOptions.IsSetName = true;
              targetOptions.Name = commandLineOptions.Arguments[arg];
              break;
            case KillProcessOptionType.Help:
              targetOptions.IsSetHelp = true;
              break;
            case KillProcessOptionType.Version:
              targetOptions.IsSetVersion = true;
              break;
          }
        }
      }

      if (commandLineOptions.Parameters.Count > 0)
      {
        if (string.IsNullOrEmpty(targetOptions.Pid))
        {
          targetOptions.IsSetPid = true;
          targetOptions.Pid = commandLineOptions.Parameters.First();
        }
      }

      return targetOptions;
    }

    private static void CheckOptions(KillProcessCommandLineOptions checkedOptions)
    {
      if (!checkedOptions.IsSetHelp && !checkedOptions.IsSetVersion)
      {
        if (!checkedOptions.IsSetPid && !checkedOptions.IsSetName)
        {
          throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
            "Option used in invalid context -- {0}", "must specify a option."));
        }

        Process p = Process.GetCurrentProcess();

        if (checkedOptions.IsSetPid && checkedOptions.IsSetName)
        {
          throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
            "Option used in invalid context -- {0}", "can only set pid or name."));
        }
        if (checkedOptions.IsSetPid && string.IsNullOrEmpty(checkedOptions.Pid))
        {
          throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
            "Option used in invalid context -- {0}", "invalid process pid format."));
        }
        if (checkedOptions.IsSetPid)
        {
          int pid = 0;
          if (!int.TryParse(checkedOptions.Pid, out pid))
          {
            throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
              "Option used in invalid context -- {0}", "invalid process pid number."));
          }
          else
          {
            if (pid == p.Id)
            {
              throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
                "Option used in invalid context -- {0}", "cannot kill current process by pid."));
            }
          }
        }
        if (checkedOptions.IsSetName && string.IsNullOrEmpty(checkedOptions.Name))
        {
          throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
            "Option used in invalid context -- {0}", "invalid process name."));
        }
        if (checkedOptions.IsSetName)
        {
          if (checkedOptions.Name == p.ProcessName)
          {
            throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
              "Option used in invalid context -- {0}", "cannot kill current process by name."));
          }
        }
      }
    }

    #endregion
  }
}
