using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CommandGroup("test")]
[CommandAlias("test", "test2", "t")]
public class TestCommand : ConsoleCommand
{
    public override IEnumerator Execute(Terminal term, string[] args)
    {
        for (int i = 0; i < 10; i++) term.WriteLine("Test");
        term.WriteLine(args.FirstOrDefault());
        yield return null;
    }
}
