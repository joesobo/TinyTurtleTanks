using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase : MonoBehaviour {
    private string _commandId;
    private string _commandDescription;
    private string _commandFormat;
    private string _extraDescription;

    public string commandId { get { return _commandId; } }
    public string commandDescription { get { return _commandDescription; } }
    public string commandFormat { get { return _commandFormat; } }
    public string extraDescription { get { return _extraDescription; } }

    public DebugCommandBase(string id, string description, string format, string extraDescription) {
        _commandId = id;
        _commandDescription = description;
        _commandFormat = format;
        _extraDescription = extraDescription;
    }
}

public class DebugCommand : DebugCommandBase {
    private Action command;

    public DebugCommand(string id, string description, string format, string extraDescription, Action command) : base(id, description, format, extraDescription) {
        this.command = command;
    }

    public void Invoke() {
        command.Invoke();
    }
}

public class DebugCommand<T1> : DebugCommandBase {
    private Action<T1> command;

    public DebugCommand(string id, string description, string format, string extraDescription, Action<T1> command) : base(id, description, format, extraDescription) {
        this.command = command;
    }

    public void Invoke(T1 value) {
        command.Invoke(value);
    }
}

public class DebugCommand<T1, T2> : DebugCommandBase {
    private Action<T1, T2> command;

    public DebugCommand(string id, string description, string format, string extraDescription, Action<T1, T2> command) : base(id, description, format, extraDescription) {
        this.command = command;
    }

    public void Invoke(T1 textCommand, T2 numberModifier) {
        command.Invoke(textCommand, numberModifier);
    }
}