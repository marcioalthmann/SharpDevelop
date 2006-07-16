﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald" email="daniel@danielgrunwald.de"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.IO;
using System.Collections;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.SharpDevelop.Gui;

namespace ICSharpCode.Core
{
	public enum TaskType {
		Error,
		Warning,
		Message,
		
		Comment,
	}
	
	public class Task
	{
		string   description;
		string   fileName;
		TaskType type;
		int      line;
		int      column;
		object contextMenuOwner;
		string contextMenuAddInTreeEntry;

		public override string ToString()
		{
			return String.Format("[Task:File={0}, Line={1}, Column={2}, Type={3}, Description={4}",
			                     fileName,
			                     line,
			                     column,
			                     type,
			                     description);
		}
			
		/// <summary>
		/// The line number of the task. Zero-based (text editor coordinate)
		/// </summary>
		public int Line {
			get {
				return line;
			}
		}
		
		/// <summary>
		/// The column number of the task. Zero-based (text editor coordinate)
		/// </summary>
		public int Column {
			get {
				return column;
			}
		}
		
		public string Description {
			get {
				return description;
			}
		}
		
		public string FileName {
			get {
				return fileName;
			}
			set {
				fileName = value;
			}
		}
		
		public TaskType TaskType {
			get {
				return type;
			}
		}
		
		public object ContextMenuOwner {
			get {
				return contextMenuOwner;
			}
			set {
				contextMenuOwner = value;
			}
		}
		
		public string ContextMenuAddInTreeEntry {
			get {
				return contextMenuAddInTreeEntry;
			}
			set {
				contextMenuAddInTreeEntry = value;
			}
		}
		
		public Task(string fileName, string description, int column, int line, TaskType type)
		{
			this.type        = type;
			this.fileName    = fileName;
			this.description = description.Trim();
			this.column      = column;
			this.line        = line;
		}
		
		public Task(BuildError error)
		{
			type         = error.IsWarning ? TaskType.Warning : TaskType.Error;
			column       = Math.Max(error.Column - 1, 0);
			line         = Math.Max(error.Line - 1, 0);
			fileName     = error.FileName;
			if (string.IsNullOrEmpty(error.ErrorCode)) {
				description = error.ErrorText;
			} else {
				description = error.ErrorText + "(" + error.ErrorCode + ")";
			}
			contextMenuAddInTreeEntry = error.ContextMenuAddInTreeEntry;
			contextMenuOwner = error;
		}
		
		public void JumpToPosition()
		{
			FileService.JumpToFilePosition(fileName, line, column);
		}
	}
}
