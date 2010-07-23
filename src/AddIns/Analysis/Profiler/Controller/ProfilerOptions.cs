// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Siegfried Pammer" email="sie_pam@gmx.at"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ICSharpCode.Profiler.Controller.Data;

namespace ICSharpCode.Profiler.Controller
{
	/// <summary>
	/// Holds all settings for the profiler chosen by the user.
	/// </summary>
	public class ProfilerOptions
	{
		/// <summary>
		/// Defines the default size of the shared memory.
		/// </summary>
		public const int DefaultSharedMemorySize = 64 * 1024 * 1024; // 64 mb
		
		/// <summary>
		/// Defines a list of default performance counters.
		/// </summary>
		public static readonly PerformanceCounterDescriptor[] DefaultCounters = new[] {
			new PerformanceCounterDescriptor("Process", "% Processor Time", "_Total", ".", 0, 0, null, "%", "0.00"),
			new PerformanceCounterDescriptor("Process", "IO Data Bytes/sec", "_Total", ".", 0, null, null, "bytes/sec", "#,##0")
		};
		
		bool enableDC;
		bool enableDCAtStart;
		bool dotNotProfileDotNetInternals;
		bool combineRecursiveFunction;
		int sharedMemorySize;
		
		PerformanceCounterDescriptor[] counters;
		
		/// <summary>
		/// Gets the performance counters selected for monitoring.
		/// </summary>
		public PerformanceCounterDescriptor[] Counters {
			get { return this.counters; }
		}
		
		/// <summary>
		/// Gets whether .NET internal calls are profiled or not.
		/// </summary>
		public bool DoNotProfileDotNetInternals {
			get { return dotNotProfileDotNetInternals; }
		}
		
		/// <summary>
		/// Gets whether recursive functions calls are combined or not.
		/// </summary>
		public bool CombineRecursiveFunction {
			get { return combineRecursiveFunction; }
		}
		
		/// <summary>
		/// Gets whether data collection is enabled during profiling sessions.
		/// </summary>
		public bool EnableDC {
			get { return enableDC; }
		}
		
		/// <summary>
		/// Gets whether data collection is enabled at the start of the profiling session.
		/// </summary>
		public bool EnableDCAtStart {
			get { return enableDCAtStart; }
		}
		
		/// <summary>
		/// Gets the size of the shared memory.
		/// </summary>
		public int SharedMemorySize {
			get { return sharedMemorySize; }
		}
		
		/// <summary>
		/// Creates new ProfilerOptions using the selected settings.
		/// </summary>
		public ProfilerOptions(bool enableDC, int sharedMemorySize, bool profileDotNetInternals,
		                       bool combineRecursiveFunction, bool enableDCAtStart, IEnumerable<PerformanceCounterDescriptor> counters)
		{
			this.enableDC = enableDC;
			this.sharedMemorySize = sharedMemorySize;
			this.dotNotProfileDotNetInternals = profileDotNetInternals;
			this.combineRecursiveFunction = combineRecursiveFunction;
			this.enableDCAtStart = enableDCAtStart;
			this.counters = counters.ToArray();
		}
		
		/// <summary>
		/// Creates default ProfilerOptions.
		/// </summary>
		public ProfilerOptions()
			: this(true, DefaultSharedMemorySize, false, false, true, DefaultCounters)
		{
		}
	}
}
