﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Gimela.Infrastructure.AsyncModel")]
[assembly: AssemblyDescription("Gimela.Infrastructure.AsyncModel")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Gimela Technology Co., Ltd.")]
[assembly: AssemblyProduct("Gimela® Infrastructure Library")]
[assembly: AssemblyCopyright("Copyright © 2011-2012 Gimela Technologies Co., Ltd. All rights reserved.")]
[assembly: AssemblyTrademark("Gimela®")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("63191a5a-c22a-4011-93b6-aef3a5e92ff2")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gimela")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gimela", Scope = "namespace", Target = "Gimela.Infrastructure.AsyncModel")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorkerHelper.#DoWork`1(System.ComponentModel.DoWorkEventHandler,System.ComponentModel.ProgressChangedEventHandler,System.EventHandler`1<Gimela.Infrastructure.AsyncModel.AsyncWorkerCallbackEventArgs`1<!!0>>)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorkerHelper.#DoWork`1(System.ComponentModel.DoWorkEventHandler,System.EventHandler`1<Gimela.Infrastructure.AsyncModel.AsyncWorkerCallbackEventArgs`1<!!0>>)")]
[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorkerHelper.#DoWork`1(System.ComponentModel.DoWorkEventHandler,System.Int32,System.ComponentModel.ProgressChangedEventHandler,System.EventHandler`1<Gimela.Infrastructure.AsyncModel.AsyncWorkerCallbackEventArgs`1<!!0>>)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorkerHelper.#DoWork`1(System.ComponentModel.DoWorkEventHandler,System.Int32,System.ComponentModel.ProgressChangedEventHandler,System.EventHandler`1<Gimela.Infrastructure.AsyncModel.AsyncWorkerCallbackEventArgs`1<!!0>>)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorker`1.#.ctor(System.ComponentModel.DoWorkEventHandler,System.ComponentModel.ProgressChangedEventHandler,System.EventHandler`1<Gimela.Infrastructure.AsyncModel.AsyncWorkerCallbackEventArgs`1<!0>>)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncWorker`1.#WorkerMethodCompletedCallback")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Scope = "member", Target = "Gimela.Infrastructure.AsyncModel.AsyncToken`1.#Equals(System.Object)")]
