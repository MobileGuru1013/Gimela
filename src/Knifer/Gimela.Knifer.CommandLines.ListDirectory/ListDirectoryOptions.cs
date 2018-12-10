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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Gimela.Knifer.CommandLines.ListDirectory
{
	internal static class ListDirectoryOptions
	{
		public static readonly ReadOnlyCollection<string> InputDirectoryOptions;
		public static readonly ReadOnlyCollection<string> DirectoryOptions;
		public static readonly ReadOnlyCollection<string> FileOptions;
		public static readonly ReadOnlyCollection<string> ListOptions;
		public static readonly ReadOnlyCollection<string> HelpOptions;
		public static readonly ReadOnlyCollection<string> VersionOptions;

		public static readonly IDictionary<ListDirectoryOptionType, ICollection<string>> Options;

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
		static ListDirectoryOptions()
		{
			InputDirectoryOptions = new ReadOnlyCollection<string>(new string[] { "i", "input" });
			DirectoryOptions = new ReadOnlyCollection<string>(new string[] { "d", "directory" });
			FileOptions = new ReadOnlyCollection<string>(new string[] { "f", "file" });
			ListOptions = new ReadOnlyCollection<string>(new string[] { "l", "list" });
			HelpOptions = new ReadOnlyCollection<string>(new string[] { "h", "help" });
			VersionOptions = new ReadOnlyCollection<string>(new string[] { "v", "version" });

			Options = new Dictionary<ListDirectoryOptionType, ICollection<string>>();
			Options.Add(ListDirectoryOptionType.InputDirectory, InputDirectoryOptions);
			Options.Add(ListDirectoryOptionType.Directory, DirectoryOptions);
			Options.Add(ListDirectoryOptionType.File, FileOptions);
			Options.Add(ListDirectoryOptionType.List, ListOptions);
			Options.Add(ListDirectoryOptionType.Help, HelpOptions);
			Options.Add(ListDirectoryOptionType.Version, VersionOptions);
		}

		public static List<string> GetSingleOptions()
		{
			List<string> singleOptionList = new List<string>();

			singleOptionList.AddRange(ListDirectoryOptions.DirectoryOptions);
			singleOptionList.AddRange(ListDirectoryOptions.FileOptions);
			singleOptionList.AddRange(ListDirectoryOptions.HelpOptions);
			singleOptionList.AddRange(ListDirectoryOptions.VersionOptions);

			return singleOptionList;
		}

		#region Usage

		public static readonly string Usage = string.Format(CultureInfo.CurrentCulture, @"
NAME

	ls - list directory contents

SYNOPSIS

	ls [OPTION]... [FILE]...

DESCRIPTION

	List information about the FILEs (the current directory by default). 

OPTIONS

	-i, --input=INPUT
	{0}{0}The input directory.
	-f, --file
	{0}{0}List file entries instead of contents.
	-d, --directory
	{0}{0}List directory entries instead of contents.
	-l, --list
	{0}{0}Shows you huge amounts of information.
	{0}{0}(permissions, owners, size, and when last modified.)
	-h, --help 
	{0}{0}Display this help and exit.
	-v, --version
	{0}{0}Output version information and exit.

EXAMPLES

	ls -f
	List file entries instead of contents in the current directory.

AUTHOR

	Written by Chundong Gao.

REPORTING BUGS

	Report bugs to <gaochundong@gmail.com>.

COPYRIGHT

	Copyright (C) 2011-2012 Chundong Gao. All Rights Reserved.
", @" ");

		#endregion

		public static ListDirectoryOptionType GetOptionType(string option)
		{
			ListDirectoryOptionType optionType = ListDirectoryOptionType.None;

			foreach (var pair in Options)
			{
				foreach (var item in pair.Value)
				{
					if (item == option)
					{
						optionType = pair.Key;
						break;
					}
				}
			}

			return optionType;
		}
	}
}
