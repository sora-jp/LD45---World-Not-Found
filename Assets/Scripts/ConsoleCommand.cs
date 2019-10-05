using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.Utilities;
using UnityEngine;

public abstract class ConsoleCommand
{
    public abstract IEnumerator Execute(Terminal term, string[] args);
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class CommandGroupAttribute : Attribute
{
    public string Group { get; }

    public CommandGroupAttribute(string group)
    {
        Group = group;
    }
}

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class CommandAliasAttribute : Attribute
{
    public string[] Aliases { get; }

    public CommandAliasAttribute(params string[] aliases)
    {
        Aliases = aliases;
    }
}

public static class ConsoleCommandLoader
{
    private static Dictionary<string, Dictionary<string, ConsoleCommand>> _groupToCommands;
    private static Dictionary<string, ConsoleCommand> _allCommands;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void LoadCommands()
    {
        _groupToCommands = new Dictionary<string, Dictionary<string, ConsoleCommand>>();
        _allCommands = new Dictionary<string, ConsoleCommand>();

        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes())
            .Where(t => t.IsSubclassOf(typeof(ConsoleCommand)) && !t.IsAbstract).ToList();

        foreach (var type in types)
        {
            var group = type.GetCustomAttribute<CommandGroupAttribute>()?.Group;
            if (string.IsNullOrEmpty(group))
            {
                continue;
            }

            var aliases = type.GetCustomAttribute<CommandAliasAttribute>()?.Aliases;
            if (aliases == null)
            {
                continue;
            }

            if (!_groupToCommands.ContainsKey(group)) _groupToCommands[group] = new Dictionary<string, ConsoleCommand>();
            var dict = GetCommandsWithGroup(group.ToLower());
            var cmd = Activator.CreateInstance(type) as ConsoleCommand;

            foreach (var alias in aliases)
            {
                _allCommands[alias.ToLower()] = cmd;
                dict[alias.ToLower()] = cmd;
            }

            _groupToCommands[group.ToLower()] = dict;
        }
    }

    public static Dictionary<string, ConsoleCommand> GetCommandsWithGroup(string group)
    {
        if (group == "*") return _allCommands;
        return _groupToCommands[group];
    }
}