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
using System.Globalization;
using System.Linq;

namespace Gimela.Knifer.CommandLines.Foundation
{
  /// <summary>
  /// Command line parsing
  /// </summary>
  public static class CommandLineParser
  {
    private const char OptionEqualChar = '=';
    private static readonly char[] OptionStartWithChars = new char[] { '-' };
    private const string MagicOptionValue = @"!#%&$&*^)&_+*^&&*^$#@*$^%)^^&#$@$#+&(^^$#!";

    /// <summary>
    /// Parses the passed command line arguments and returns the result
    /// in a CommandLineOptions object.
    /// </summary>
    /// <param name="args">Array of command line arguments</param>
    /// <param name="list">Array of command line arguments</param>
    /// <returns>Object containing the parsed command line</returns>
    public static CommandLineOptions Parse(string[] args, params string[] singleOptionList)
    {
      if (args == null)
        throw new ArgumentNullException("args");

      CommandLineOptions cmdOptions = new CommandLineOptions();
      int index = 0;

      if (index < args.Length)
      {
        string token = NextToken(args, ref index);
        while (!string.IsNullOrEmpty(token))
        {
          if (IsArgument(token))
          {
            string arg = token.TrimStart(OptionStartWithChars).TrimEnd(OptionEqualChar);
            string value = string.Empty;

            if (arg.Contains(OptionEqualChar))
            {
              // arg was specified with an '=' sign, so we need
              // to split the string into the arg and value, but only
              // if there is no space between the '=' and the arg and value.
              string[] r = arg.Split(new char[] { OptionEqualChar }, 2);
              if (r.Length == 2)
              {
                arg = r[0];
                value = r[1];
              }
            }

            // single option do not need a following parameter
            bool isSingleOption = false;
            if (singleOptionList != null)
            {
              for (int i = 0; i < singleOptionList.Length; i++)
              {
                if (arg == singleOptionList[i])
                {
                  isSingleOption = true;
                  break;
                }
              }
            }

            // find following parameter
            while (!isSingleOption && string.IsNullOrEmpty(value))
            {
              index++;
              if (index < args.Length)
              {
                string next = NextToken(args, ref index);
                if (!string.IsNullOrEmpty(next))
                {
                  if (IsArgument(next))
                  {
                    // push the token back onto the stack so
                    // it gets picked up on next pass as an arg
                    index--;
                    value = MagicOptionValue;
                    break;
                  }
                  else if (next != OptionEqualChar.ToString())
                  {
                    // save the value (trimming any '=' from the start)
                    value = next.TrimStart(OptionEqualChar);
                  }
                }
              }
              else
              {
                index--;
                value = MagicOptionValue;
                break;
              }
            }

            // save the pair
            if (cmdOptions.Arguments.ContainsKey(arg))
              throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
                "Option used in invalid context -- {0}", "option with the same argument."));

            if (value == MagicOptionValue)
            {
              cmdOptions.Arguments.Add(arg, string.Empty);
            }
            else
            {
              cmdOptions.Arguments.Add(arg, value.TrimStart('\'').TrimEnd('\''));
            }
          }
          else
          {
            // save stand-alone parameter
            if (cmdOptions.Parameters.Contains(token))
              throw new CommandLineException(string.Format(CultureInfo.CurrentCulture,
                "Option used in invalid context -- {0}", "option with the same argument."));

            cmdOptions.Parameters.Add(token.TrimStart('\'').TrimEnd('\''));
          }

          index++;
          if (index < args.Length)
          {
            token = NextToken(args, ref index);
          }
          else
          {
            break;
          }
        }
      }

      return cmdOptions;
    }

    /// <summary>
    /// Returns True if the passed string is an argument (starts with '-', '--', or '\'.)
    /// </summary>
    /// <param name="token">the string token to test</param>
    /// <returns>true if the passed string is an argument, else false if a parameter</returns>
    private static bool IsArgument(string token)
    {
      bool isArgument = false;

      if (!string.IsNullOrEmpty(token))
      {
        foreach (char item in OptionStartWithChars)
        {
          isArgument = token.StartsWith(item.ToString(CultureInfo.CurrentCulture), false, CultureInfo.CurrentCulture);
          if (isArgument) break;
        }
      }

      return isArgument;
    }

    /// <summary>
    /// Returns the next string token in the argument list.
    /// </summary>
    /// <param name="args">list of string tokens</param>
    /// <param name="index">index of the current token in the array</param>
    /// <returns>the next string token, or null if no more tokens in array</returns>
    private static string NextToken(string[] args, ref int index)
    {
      if (index < 0)
        throw new ArgumentOutOfRangeException("index");
      if (index >= args.Length)
        throw new ArgumentOutOfRangeException("index");

      string token = string.Empty;

      while (index < args.Length)
      {
        token = args[index].Trim();
        if (!string.IsNullOrEmpty(token))
        {
          break;
        }
        else
        {
          index++;
        }
      }

      return token;
    }
  }
}
