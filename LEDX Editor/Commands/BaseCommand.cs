using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace LEDX.Commands {
	public class BaseCommand : ICommand {
		#region CanExecute Automatic Updating
		static readonly List<BaseCommand> AutomaticCanExecuteUpdatingCommand = new List<BaseCommand>();

		static void RegisterForCanExecuteUpdating(BaseCommand command) {
			AutomaticCanExecuteUpdatingCommand.Add(command);
		}

		#endregion

		#region Events

		/// <summary>
		/// Occurs when CanExecute property changed
		/// </summary>
		public event EventHandler CanExecuteChanged;

		#endregion

		#region Fields

		readonly Action<object> _execute;
		readonly Predicate<object> _canExecute;

		#endregion // Fields

		#region Constructors

		/// <summary>
		/// Construcotor
		/// </summary>
		/// <param name="execute">Action to execute</param>
		public BaseCommand(Action<object> execute)
			: this(execute, null, false) {
		}

		/// <summary>
		/// Construcotor
		/// </summary>
		/// <param name="execute">Action to execute</param>
		/// <param name="canExecute">Predicate to check whether command can be executed</param>
		public BaseCommand(Action<object> execute, Predicate<object> canExecute)
			: this(execute, canExecute, false) {
		}

		/// <summary>
		/// Construcotor
		/// </summary>
		/// <param name="execute">Action to execute</param>
		/// <param name="canExecute">Predicate to check whether command can be executed</param>
		/// <param name="autoCanExecuteUpdating">Use this flag only if you can not invoke RaiseCanExecuteChanged</param>
		public BaseCommand(Action<object> execute, Predicate<object> canExecute, bool autoCanExecuteUpdating) {
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
			if (autoCanExecuteUpdating)
				RegisterForCanExecuteUpdating(this);
		}

		#endregion

		#region ICommand Members

		/// <summary>
		/// Gets whether the command can be executed
		/// </summary>
		/// <param name="parameter">Parameter</param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public bool CanExecute(object parameter) {
			return _canExecute == null || _canExecute(parameter);
		}

		/// <summary>
		/// Raises CanExecuteChanged event 
		/// </summary>
		[SuppressMessage("Microsoft.Design", "CA1030")]
		public void RaiseCanExecuteChanged() {
			if (CanExecuteChanged != null)
				CanExecuteChanged(this, EventArgs.Empty);
		}

		/// <summary>
		/// Executes the command
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter) {
			_execute(parameter);
		}

		#endregion
	}
}
